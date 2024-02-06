CREATE TABLE [Images] (
  [Id] int PRIMARY KEY NOT NULL,
  [ContentURL] nvarchar(max) UNIQUE NOT NULL
)

CREATE TABLE [Categories] (
  [Id] int PRIMARY KEY NOT NULL,
  [ParentCategoryId] int REFERENCES Categories(Id),
  [Name] nvarchar(256) NOT NULL
)

CREATE TABLE [Currencies] (
  [ISOCode] char(3) PRIMARY KEY NOT NULL,
  [Name] nvarchar(256) UNIQUE NOT NULL,
  [Symbol] nchar(1) UNIQUE NOT NULL
)

CREATE TABLE [Products] (
  [ArticleNumber] nvarchar(256) PRIMARY KEY NOT NULL,
  [Name] nvarchar(256) NOT NULL,
  [Ingress] nvarchar(256),
  [Description] nvarchar(4000) NOT NULL,
  [ThumbnailImageId] int REFERENCES Images(Id),
  [DetailedSpecs] nvarchar(max)
)

CREATE TABLE [Customers] (
  [Id] int PRIMARY KEY NOT NULL,
  [FirstName] nvarchar(256) NOT NULL,
  [LastName] nvarchar(256),
  [RegisteredDate] datetime2 NOT NULL
)

CREATE TABLE [Reviews] (
  [Id] int PRIMARY KEY NOT NULL,
  [CustomerId] int NOT NULL REFERENCES Customers(Id),
  [Rating] tinyint NOT NULL,
  [Title] nvarchar(256),
  [Content] nvarchar(4000)
)

CREATE TABLE [Orders] (
  [OrderId] int PRIMARY KEY NOT NULL,
  [CustomerId] int NOT NULL REFERENCES Customers(Id),
  [ProductList] nvarchar(4000) NOT NULL,
  [Cost] money NOT NULL,
  [CurrencyISOCode] char(3) NOT NULL REFERENCES Currencies(ISOCode),
  [DateOrdered] datetime2 NOT NULL
)

CREATE TABLE [ProductOrderRows] (
  [ProductId] nvarchar(256) NOT NULL REFERENCES Products(ArticleNumber),
  [OrderId] int NOT NULL REFERENCES Orders(OrderId),
  [Price] money,
  [CurrencyISOCode] char(3) REFERENCES Currencies(ISOCode),
  [Count] int,
  PRIMARY KEY ([ProductId], [OrderId])
)

CREATE TABLE [ProductCategory] (
  [CategoryId] int NOT NULL REFERENCES Categories(Id),
  [ArticleNumber] nvarchar(256) NOT NULL REFERENCES Products(ArticleNumber),
  PRIMARY KEY ([CategoryId], [ArticleNumber])
)

CREATE TABLE [ProductImages] (
  [ArticleNumber] nvarchar(256) NOT NULl REFERENCES Products(ArticleNumber),
  [ImageId] int NOT NULL REFERENCES Images(Id),
  PRIMARY KEY ([ArticleNumber], [ImageId])
)


CREATE TABLE [ProductReviews] (
  [ArticleNumber] nvarchar(256) NOT NULL REFERENCES Products(ArticleNumber),
  [ReviewId] int NOT NULL REFERENCES Reviews(Id),
  PRIMARY KEY ([ArticleNumber], [ReviewId])
)

CREATE TABLE [ProductPrices] (
  [ArticleNumber] nvarchar(256) PRIMARY KEY NOT NULL REFERENCES Products(ArticleNumber),
  [CurrencyISOCode] char(3) NOT NULL REFERENCES Currencies(ISOCode),
  [Price] money NOT NULL,
  [PriceDiscounted] money,
  [PriceLast30Days] money,
  [Type] nvarchar(256)