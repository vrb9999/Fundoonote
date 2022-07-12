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

create procedure spGetAllUser
As
Begin try
select * from Users
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

exec spGetAllUser

--executing the spLoginUser stored procedure
Create procedure spLoginUser(
@Email varchar(50),
@Password varchar(50)
)
As
Begin try
select * from Users where Email=@Email and password = @Password
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

exec spLoginUser 'vinay@gmail.com' ,'Vinay@123'


--executing the spForgetPasswordUser stored procedure
Create procedure spForgetPasswordUser(
@Email varchar(50)
)
As
Begin try
select * from Users where Email=@Email 
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

exec spForgetPasswordUser 'vinay@gmail.com'


--executing the spForgetPasswordUser stored procedure
Create procedure spResetPassword(
@Email varchar(50),
@Password varchar(50)
)
As
Begin try
--select * from Users where Email=@Email
update Users set password=@Password where Email=@Email 
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH
