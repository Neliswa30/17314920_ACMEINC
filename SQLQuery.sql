create database AcmeIncdb

create table product(
	Pro_id int IDENTITY(1,1) PRIMARY KEY,
	prdName varchar(255),
	prdPrice smallmoney,
	pDescription varchar(255), 
);

create table customerUser(
	username varchar(255) PRIMARY KEY not null,
	userFirstname varchar(255) , 
	userLastname varchar(255),
	userPassword varchar(255) not null
);

create table administrator(
	userSurname varchar(255) PRIMARY KEY not null,
	userPassword varchar(255) not null
);

create table shoppingCart(
	id int IDENTITY(1,1) PRIMARY KEY,
	username varchar(255) FOREIGN KEY REFERENCES customerUser(username),
	Pro_id int FOREIGN KEY REFERENCES product(Pro_id)
);

insert into product values ('Eggs', 40.99, 'Large Jumbo eggs');
insert into product values ('Nivea',	12.99,	'This is just body lotion');
insert into product values ('Mashrooms',	20.00,	'Just pure mashrooms');
insert into product values ('Chicken Breast',	30.00, 	'This is just chicken meat R30/kq');
insert into product values ('Oros', 26.99, 'This is concentrated juice');
insert into product values ('bread',	16.99,	'brown bread');
insert into product values ('Luqi Fruit',	25.00,	'Pure 100% Juice');

Select * From product