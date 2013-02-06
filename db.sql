Create Database TrulDB
Go

Use TrulDb
Go

Create Table Country (
CountryID Int Identity Primary Key,
Name NVarChar(50) Not Null,
IsDeleted Bit Default(0) Not Null
)

Create Table Person (
PersonID Int Identity Primary Key,
FirstName NVarChar(50) Not Null,
LastName NVarChar(50) Not Null,
CountryID Int Not Null,
IsDeleted Bit Default(0) Not Null,
Foreign Key(CountryID) References Country (CountryID)
)

Create Table [User] (
UserID Int Identity Primary Key,
UserName NVarChar(250) Not Null,
Password NVarChar(250) Not Null,
IsDeleted Bit Default(0) Not Null
)

Create Table [Role] (
RoleID Int Identity Primary Key,
RoleName NVarChar(250) Not Null,
IsDeleted Bit Default(0) Not Null
)

Create Table UserRole (
UserRoleID Int Identity Primary Key,
UserID Int Not Null,
RoleID Int Not Null,
Foreign Key(UserID) References [User] (UserID),
Foreign Key(RoleID) References [Role] (RoleID)
)


Insert Into Country(Name) Values('Turkey')
Insert Into Country(Name) Values('USA')
Insert Into Country(Name) Values('United Kingdom')
Insert Into Country(Name) Values('Belgium')
Insert Into Country(Name) Values('Germany')
Insert Into Country(Name) Values('Mexico')
Insert Into Country(Name) Values('Brazil')
Insert Into Country(Name) Values('Rohan')
Insert Into Country(Name) Values('Mordor')
Insert Into Country(Name) Values('Gondor')
Insert Into Country(Name) Values('Isengard')
Insert Into Country(Name) Values('Stormwind')
Insert Into Country(Name) Values('Redridge')
Insert Into Country(Name) Values('Goldshire')
Insert Into Country(Name) Values('Northshire')
Insert Into Country(Name) Values('Duskwood')
Insert Into Country(Name) Values('Elwynn Forest')
Insert Into Country(Name) Values('Westfall')


Insert Into Person(FirstName, LastName, CountryID) 
Select 'Tuðrul', 'Elmas', CountryID From Country Where Name = 'Turkey'

Insert Into Person(FirstName, LastName, CountryID) 
Select 'James', 'Bond', CountryID From Country Where Name = 'United Kingdom'

CREATE TABLE [dbo].[Menu](
	[MenuID] [int] IDENTITY(1,1) NOT NULL,
	[LinkName] [nvarchar](100) NULL,
	[ApplicationCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
