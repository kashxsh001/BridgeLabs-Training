create schema banking;
create schema transaction_schema;

create table banking.customers(
	customer_id int PRIMARY KEY,
	first_name varchar(20) NOT NULL,
	last_name varchar(20) NOT NULL,
	email text UNIQUE NOT NULL,
	phone varchar(15) NOT NULL UNIQUE ,
	birthdate DATE NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
)
create table banking.branches(
	branch_id int PRIMARY KEY,
	branch_code varchar(10) UNIQUE NOT NULL,
	branch_name text NOT NULL,
	city text NOT NULL,
	state text NOT NULL
)
	
create table banking.accounts(
	account_id int PRIMARY KEY,
	account_number varchar(20) UNIQUE NOT NULL,
	customer_id int NOT NULL REFERENCES  banking.customers(customer_id),
	branch_id int NOT NULL REFERENCES banking.branches(branch_id),
	account_type text NOT NULL,
	balance NUMERIC(15,2) NOT NULL Check (balance>=0) DEFAULT 0,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
)

create table transaction_schema.bank_transactions(
	transaction_id int PRIMARY KEY,
	account_id int Not NUll REFERENCES banking.accounts(account_id),
	transaction_type text NOT NULL,
	transaction_timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	description text,
	amount NUMERIC(15,2) NOT NULL CHECK(amount>=0)
)
INSERT INTO banking.customers
VALUES
(1,'Kashish', 'Gupta', 'kashish.gupta@gmail.com', '9876543210', '2004-05-15'),
(2,'Rahul', 'Sharma', 'rahul.sharma@gmail.com', '9876543211', '2002-08-20'),
(3,'Priya', 'Verma', 'priya.verma@gmail.com', '9876543212', '2003-01-10')

INSERT INTO banking.branches
VALUES
(001,'BR001', 'Main Branch', 'Ludhiana', 'Punjab'),
(002,'BR002', 'Model Town Branch', 'Ludhiana', 'Punjab'),
(003,'BR003', 'Sector 17 Branch', 'Chandigarh', 'Chandigarh'),
(004,'BR004', 'Industrial Area Branch', 'Mohali', 'Punjab')

INSERT INTO banking.accounts
VALUES
(1,'ACC10001', 1, 1, 'SAVINGS', 45000.00),
(2,'ACC10002', 2, 1, 'CURRENT', 75000.00),
(3,'ACC10003', 3, 2, 'SAVINGS', 28000.00)

INSERT INTO transaction_schema.bank_transactions
VALUES

(001,1, 'DEPOSIT', '2026-09-20 09:15:00',
 'Initial deposit',50000.00),

(002,1, 'WITHDRAWAL',  '2026-09-21 11:30:00',
 'ATM withdrawl',5000.00),

(003,1, 'DEPOSIT',  '2026-09-22 14:20:00',
 'Salary credit',10000.00),

(004,2, 'DEPOSIT',  '2026-09-20 10:00:00',
 'Business deposit',100000.00),

(005,2, 'WITHDRAWAL', '2026-09-21 12:15:00',
 'Business expense',25000.00),

(006,3, 'DEPOSIT', '2026-09-20 10:30:00',
 'Cash deposit',40000.00)