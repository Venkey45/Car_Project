CREATE TABLE car_users (
    id INT IDENTITY(1,1),
    Name VARCHAR(50),
    password VARCHAR(50),
    Email VARCHAR(50) NOT NULL,
    phone VARCHAR(10),
    CONSTRAINT PK_car_users_Email PRIMARY KEY (Email),
    CONSTRAINT UQ_car_users_id UNIQUE (id)
);
drop table car_users
select * from Car_users
insert into car_users values('venkey','venkey@456','vadalavenkatesh11@gmail.com',9346132403)
select * from car_users

create procedure proc_userdata_Register(@name varchar(100),@pass varchar(50),@email varchar(100),@phone bigint)
as begin
insert into car_users values(@name,@pass,@email,@phone)
end;

/* Car_Booking Data Table */
CREATE TABLE Car_Bookings (
    Booking_id INT IDENTITY(1523,1) PRIMARY KEY,
    Name VARCHAR(100),
    Phone VARCHAR(10),  -- Changed to match the data type in car_users
    Email VARCHAR(50),  -- Corrected spelling and data type
    Model VARCHAR(500),
    price MONEY,
    book_date DATETIME DEFAULT SYSDATETIME(),
    CONSTRAINT FK_Car_Bookings_Email FOREIGN KEY (Email) REFERENCES car_users(Email)
);
insert into Car_Bookings values('venkey',9346132403,'vadalavenkatesh456@gmail.com','Deloren-alpha-5',200000000,SYSDATETIME());
select * from Car_Bookings

/* car Booking Details User Store Procedure */
create procedure proc_car_Booking_deatils1(@email varchar(100))
as begin
select b.Booking_id,b.Model,b.price,b.book_date from car_users u inner join Car_Bookings b on b.Email=u.Email where u.Email=@email
end;

/* car Booking */
create procedure proc_car_Booking_(@name varchar(100),@phone varchar(10),@email varchar(100),@model varchar(50),@price money,@data  varchar(50))
as begin
insert into Car_Bookings values (@name,@phone,@email,@model,@price,@data)
end;

/* Login */
create procedure proc_car_Login(@email varchar(50),@password varchar(50))
as begin
select COUNT(*) from car_users where Email=@email and password=@password
end;
/* data  Booking di  return */ 
create procedure proc_user_booking_id_return(@email varchar(50))
as begin
select * from (select *,DENSE_RANK () over(order by Booking_id desc)as rnk from Car_Bookings)as Car_Bookings where rnk=1 and Email=@email
end;