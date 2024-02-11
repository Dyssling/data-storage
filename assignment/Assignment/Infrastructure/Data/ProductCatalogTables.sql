DROP TABLE ProductReviews
DROP TABLE ProductImages
DROP TABLE ProductCategories
DROP TABLE Reviews
DROP TABLE Images
DROP TABLE Categories
DROP TABLE Products

CREATE TABLE Products (
	ArticleNumber nvarchar(256) not null primary key,
	[Name] nvarchar(max) not null,
	[Description] nvarchar(max) null,
	Price money not null --Alla priser får vara i SEK till att börja med, sedan kan man omvandla det till andra valutor genom en service 
)

CREATE TABLE Categories (
	Id int identity not null primary key,
	[Name] nvarchar(256) not null unique
)

CREATE TABLE Images (
	Id int identity not null primary key,
	ImageURL nvarchar(450) not null unique
)

CREATE TABLE Reviews (
	Id int identity not null primary key,
	CustomerId int not null,
	Rating tinyint not null,
	Title nvarchar(max) null,
	Content nvarchar(max) null
)

CREATE TABLE ProductCategories (
	ArticleNumber nvarchar(256) not null references Products(ArticleNumber),
	CategoryId int not null references Categories(Id),
	primary key (ArticleNumber, CategoryId)
)

CREATE TABLE ProductImages (
	ImageId int not null references Images(Id),
	ArticleNumber nvarchar(256) not null references Products(ArticleNumber) --ArticleNumber behöver inte vara en nyckel här, eftersom varje ImageId ändå bara kommer finnas en gång i tabellen
	primary key (ImageId, ArticleNumber)
)

CREATE TABLE ProductReviews (
	ReviewId int not null references Reviews(Id),
	ArticleNumber nvarchar(256) not null references Products(ArticleNumber) --ArticleNumber behöver inte vara en nyckel här, eftersom varje ReviewId ändå bara kommer finnas en gång i tabellen
	primary key (ReviewId, ArticleNumber)
)