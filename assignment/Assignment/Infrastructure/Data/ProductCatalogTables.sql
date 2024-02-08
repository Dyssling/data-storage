CREATE TABLE Products (
	ArticleNumber nvarchar(max) not null primary key,
	[Name] nvarchar(max) not null,
	[Description] nvarchar(max) null
)

CREATE TABLE Categories (
	Id int identity not null primary key,
	[Name] nvarchar(max) not null unique
)

CREATE TABLE Images (
	Id int identity not null primary key,
	ImageURL nvarchar(max) not null
)

CREATE TABLE Reviews (
	Id int identity not null primary key,
	CustomerId int not null,
	Rating tinyint not null,
	Title nvarchar(max) null,
	Content nvarchar(max) null
)

CREATE TABLE ProductCategories (
	ArticleNumber nvarchar(max) not null references Products(ArticleNumber),
	CategoryId int not null references Categories(Id)
)