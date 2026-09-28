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
