--creating the database for Train Reservation-------------

create Database TrainReservation
use TrainReservation

-- creating the Table for Train Details------------
create table TrainDetails (TrainNo numeric (8) Primary Key NOT NULL,
TrainName varchar (50), Source_Station varchar (50), Final_Station varchar (50))

select * from TrainDetails
Alter table TrainDetails Add TrainStatus varchar(10) not null default 'Active'

--drop table TrainDetails

--Inserting the value in TrainDetails------------
insert into TrainDetails values
(100520, 'Satabadi Express','New Delhi','Mumbai'),
(123456, 'Vande Bharat Express', 'Jammu Katra', 'Kanyakumari'),
(567890, 'Chennai Express', 'Chennai', 'Bhopal')

select * from TrainDetails

--Creating the table for Train Class------------
create table Class_Type ([S_No] int identity,TrainNo numeric (8) Foreign key references TrainDetails(TrainNo) NOT NULL,
[1-AC] int,[2-AC] int,[SL] int)
--drop table Class_Type

--Inserting the values in Train Class------------
insert into Class_Type values
(100520,200,300,500),(123456,400,600,200),(567890,200,300,500)

select * from CancelTicket

--Creating the Table for Fare-----------
create table TicketPrice(
S_No int identity,
TrainNo numeric(8) foreign key references TrainDetails(TrainNo),
[1-AC-Price] int,
[2-AC-Price] int,
SL_Price int,)

--Inserting the fare of the Trains  Ticket----------
insert into TicketPrice values
(100520,4000,2500,1250),
(123456,8000,5000,3000),
(567890,6000,4500,1000)

select * from TicketPrice

--Creating the table for User(Passenger)--------
create table UserDetails (UserID numeric (5) Primary Key NOT NULL,
UserName varchar (20), Age int, Password varchar (10) NOT NULL)

--Creating the table for Admin--------
create table AdminDetails (AdminID numeric (5) Primary Key NOT NULL,
AdminName varchar (20), Admin_Password varchar (10) NOT NULL)

--Inserting the Value of Admin-------
insert into AdminDetails values(12345,'Nityanshu','Nitya@123')

select * from AdminDetails

--Creating Table for Ticket Booking-------
create table Booking_Details (PNR_NO numeric (8) Primary Key NOT NULL,
UserID numeric (5) Foreign Key references UserDetails(UserID),
TrainNo numeric(8) foreign key references TrainDetails(TrainNo),
Class_Type varchar (10) NOT NULL,
Total_Price float,
NumberofTickets int not null,
Booking_Date_Time date)

drop table Booking_Details

--Adding Column into Ticket Booking---------
Alter Table Booking_Details add Booking_Status varchar(10) NOT NULL Default 'Booked'

select * from Booking_Details

--Creating table for Ticket Cancelling-------
create table CancelTicket(
Cancel_ID numeric (10) Primary Key,
PNR_No numeric(8) foreign key references Booking_Details(PNR_No),
UserId numeric (5) foreign key references UserDetails(UserID),
TrainNo numeric(8) foreign key references TrainDetails(TrainNo),
Cancel_Date_Time  date,
Refund_Amount float,)

Drop Table CancelTicket 
select * from CancelTicket

--Stored Procedure---------

--For Update Ticket-------
Create or Alter Procedure UpdateBooking ( @TrainNo numeric (8),
@Class Nvarchar(20), @SeatsBooked int)

as Begin

if @Class = '1AC'
   Update Class_Type
   Set [1-AC]=[1-AC]-@SeatsBooked
   where TrainNo=@TrainNo;

Else if @Class = '2AC'
   Update Class_Type
   Set [2-AC]=[2-AC]-@SeatsBooked
   where TrainNo=@TrainNo;

Else if @Class = 'SL'
   Update Class_Type
   Set [SL]=[SL]-@SeatsBooked
   where TrainNo=@TrainNo;

   END

--For Cancel Ticket---------
Create or alter Procedure UpdateCancelTicket( @TrainNo numeric(8),
    @Class NVARCHAR(20),
    @SeatsBooked INT)
as
Begin
 
    IF @Class = '1AC'
        update Class_Type
        set [1-AC] = [1-AC] + @SeatsBooked
        where @TrainNo = @TrainNo;
    Else if  @Class = '2AC'
        Update class_Type
        set [2-AC] = [2-AC] + @SeatsBooked
        where @TrainNo = @TrainNo;
    else if @Class = 'SL'
        update class_Type
        set [SL] = [SL] + @SeatsBooked
        where @TrainNo = @TrainNo;
end
   
   -- Stored Procedure to Add seats and of New Train
CREATE or alter PROCEDURE AddclassSeats( 
@TrainNo NUMERIC(8),
@firstAcSeats int,
@SecAcSeats int,
@SLSeats int
)
AS
insert into Class_Type(TrainNo,[1-AC],[2-AC],[SL]) values
(@TrainNo,@firstAcSeats,@SecAcSeats,@SLSeats)
 
 
-- Stored Proceudure to add Ticket Price of 1AC,2AC and SL class
create or alter PROC AddclassPrice( 
@TrainNo NUMERIC(8),
@firstAcTicketPrice int,
@SecAcTicketPrice int,
@SLTicketPrice int
)
AS
insert into TicketPrice(TrainNo,[1-AC-Price],[2-AC-Price],SL_Price) values
(@TrainNo,@firstAcTicketPrice,@SecAcTicketPrice,@SLTicketPrice)
