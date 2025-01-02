use ColorsDB;


create table Colors(
 ColorID INT IDENTITY(1,1) PRIMARY KEY,
 ColorName nvarchar(50) NOT NULL,
 Price DECIMAL (10, 2) NOT NULL,
 DisplayOrder INT NOT NULL,
 IsInStock BIT NOT NULL
);