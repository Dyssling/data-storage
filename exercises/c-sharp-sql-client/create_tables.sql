DROP TABLE Customers

CREATE TABLE [Customers] (
  [Id] int identity PRIMARY KEY NOT NULL,
  [FirstName] nvarchar(256) NOT NULL,
  [LastName] nvarchar(256) NOT NULL
)