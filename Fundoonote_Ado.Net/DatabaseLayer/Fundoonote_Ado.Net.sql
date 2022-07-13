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


------------------------------------------NOTE------------------------------------------

create table Note(
NoteId int identity(1,1) primary key,
Title varchar(20) not null,
Description varchar(max) not null,
Bgcolor varchar(50) not null,
IsPin bit,
IsArchive bit,
IsRemainder bit,
IsTrash bit,
UserId int not null foreign key references Users(UserId),
RegisteredDate datetime default GETDATE(),
Remainder datetime,
ModifiedDate datetime null
)

select * from Note

Alter procedure spAddNote(
@Title varchar(20), 
@Description varchar(max),
@BgColor varchar(50),
@UserId int
)
As
Begin try
insert into Note(Title,Description,Bgcolor,UserId,IsPin,IsArchive,IsRemainder,IsTrash,Remainder,ModifiedDate) values(@Title,@Description,@BgColor,@UserId,0,0,0,0,0,GetDate())
Select * from Note where UserId = @UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH


Create procedure spGetAllNotes(@UserId int)
As
Begin try
select * from Note where UserId = @UserId and IsTrash=0
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH



create procedure spUpdateNote(
@Title varchar(20), 
@Description varchar(max),
@BgColor varchar(50),
@UserId int,
@NoteId int,
@IsPin bit,
@IsArchive bit,
@IsTrash bit
)
As
Begin try
Update Note set Title=@Title, Description=@Description,BgColor=@BgColor,UserId=@UserId,IsPin=@IsPin,IsArchive=@IsArchive,IsTrash=@IsTrash,ModifiedDate=GetDate() where UserId=@UserId and NoteId=@NoteId
Select * from Note where UserId = @UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH


create procedure spDeleteNote(@NoteId int,@UserId int)
As
Begin try
Update Note set IsTrash=1 where NoteId=@NoteId and UserId=@UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH