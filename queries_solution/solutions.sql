--Indexes on account number and transaction timestamp.
create index idx_accounts_account_number
on banking.accounts(account_number);

create index idx_transactions_timestamp
on transaction_schema.bank_transactions(transaction_timestamp);

--Joins to display customer-account-branch information.
select
    c.customer_id,
    c.first_name,
    c.last_name,
    a.account_number,
    a.account_type,
    a.balance,
    b.branch_code,
    b.branch_name,
    b.city
from banking.customers c
join banking.accounts a
    on c.customer_id = a.customer_id
join banking.branches b
    on a.branch_id = b.branch_id;

--Subquery to find accounts having balance greater than the average account balance.
select account_id,account_number,balance from
banking.accounts where balance>(
	select AVG(balance) from banking.accounts
)

--View showing account transaction history.
create view transaction_schema.account_history as
select
    a.account_number,
    t.transaction_type,
    t.amount,
    t.transaction_timestamp,
    t.description
from banking.accounts a
join transaction_schema.bank_transactions t
    on a.account_id = t.account_id;

--CTE to calculate each customer's total deposits and withdrawals.
with customer_transactions as (
    select
        a.customer_id,
        t.transaction_type,
        t.amount
    from banking.accounts a
    join transaction_schema.bank_transactions t
        on a.account_id = t.account_id
)
select
    customer_id,
sum(
     case
            when transaction_type ='DEPOSIT'
            then amount
            else 0
        end
    ) as total_deposits,

    sum(
        case
            when transaction_type ='WITHDRAWAL'
            then amount
            else 0
        end
    ) as total_withdrawals

from customer_transactions
group by customer_id;

--Create a separate transaction schema and grant required permissions.
create role banking_app login password 'kashish123';

grant usage on schema banking
to banking_app;

grant usage on schema transaction_schema
to banking_app;

grant select, insert, update
on all tables in schema banking
TO banking_app;

grant select, insert, update
on all tables in schema transaction_schema
TO banking_app;

--UDF to calculate account balance from transactions.
Create or replace function transaction_schema.account_balance(p_account_id int)
returns numeric(15,2)
language sql
as $$
	select coalesce(
		sum(
		case 
			when transaction_type = 'DEPOSIT'
				then amount
		 	when transaction_type = 'WITHDRAWAL'
				then -amount
		end
	),0
)
from transaction_schema.bank_transactions
where account_id = p_account_id;
$$;

--Trigger to prevent invalid transactions.
create or replace function transaction_schema.check_withdrawal()
returns trigger
language plpgsql
as $$
declare current_balance numeric(15,2);
begin
	select balance into current_balance
from banking.accounts
where account_id = new.account_id;

if new.transaction_type = 'WITHDRAWAL'
and new.amount>current_balance then
raise exception 'Insufficient balance';

end if;

return new;
end;
$$;

create trigger prevent_invalid_withdrawal
before insert
on transaction_schema.bank_transactions
for each row
execute function transaction_schema.check_withdrawal();

--Stored procedure for fund transfer between two accounts.
create or replace procedure transaction_schema.transafer_money(
	sender_id int,
	receiver_id int,
	transfer_amount numeric(15,2)
)
language plpgsql
	as $$
declare sender_balance numeric(15,2);
begin
 select balance into sender_balance
from banking.accounts where account_id = sender_id
for update;

if sender_balance<transfer_amount then
raise exception 'Insuficient balance';
end if;

update banking.accounts set balance = balance - transfer_amount
where account_id = sender_id;

update banking.accounts set balance = balance + transfer_amount
where account_id = receiver_id;

 insert into transaction_schema.bank_transactions(account_id,transaction_type,amount,description)
values(sender_id,'WITHDRAWAL',transfer_amount,'Money transferred');

  insert into transaction_schema.bank_transactions(account_id,transaction_type,amount,description)
values(receiver_id,'DEPOSIT',transfer_amount,'Money recieved');

end;
$$;

CALL transaction_schema.transafer_money(
    1,
    2,
    5000
);

-- Cursor to generate a transaction statement.
Create or replace procedure transaction_schema.generate_transactions(
	p_account_id int
)
language plpgsql
as $$
declare
  record_data record;

transaction_cursor cursor for
	select
		transaction_id,
		transaction_type,
		amount,
		transaction_timestamp
	from transaction_schema.bank_transactions
	where account_id = p_account_id
	order by transaction_timestamp;

begin
for record_data in transaction_cursor
loop
 RAISE NOTICE
            'Transaction ID: %, Type: %, Amount: %, Date: %',
            record_data.transaction_id,
            record_data.transaction_type,
            record_data.amount,
            record_data.transaction_timestamp;
end loop;
end $$;

call transaction_schema.generate_transactions(2);

--Temporary table for today's transactions
Create temp table today_transactions as 
select * from transaction_schema.bank_transactions
where transaction_timestamp::DATE = current_date

SELECT * FROM today_transactions;
