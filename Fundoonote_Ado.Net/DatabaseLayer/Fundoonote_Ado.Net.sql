-- Creating table Users in Fundonotes Database
Create table Users(
UserId int identity(1,1) primary key,
Firstname varchar(50),
Lastname varchar(50),
Email varchar(50) unique,
password varchar(100),
CreatedDate datetime default GetDate(),
modifiedDate datetime default GetDate(),
)
insert into Users(Firstname,Lastname,Email,password) values('Vinay','Biradar','vinay@gmail.com','Vinay@123')

select* from Users

--Created Stored Procedure
create procedure spAddUser(
@Firstname varchar(50), 
@Lastname varchar(50),
@Email varchar(50),
@password varchar(100)
)
As
Begin try
insert into Users(Firstname,Lastname,Email,password) values(@Firstname,@Lastname,@Email,@password)
--select * from Users where Email=@Email
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

--executing the spAddUser stored procedure
exec spAddUser 'Suresh','Kumar','suresh@gmail.com','Suresh@123'