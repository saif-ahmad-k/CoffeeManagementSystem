CREATE TABLE tblUser (
    Id uniqueidentifier NOT NULL,
    FullName varchar(255) NOT NULL,
    Password nvarchar(max),
	Email nvarchar(200),
	Phone nvarchar(50),
	Role varchar(40),
    PRIMARY KEY (ID)
);
CREATE TABLE tblCategory (
    ID int identity(1,1) NOT NULL,
    Name varchar(255) NOT NULL,
    PRIMARY KEY (ID),
	UserID uniqueidentifier NOT NULL FOREIGN KEY REFERENCES tblUser(Id)
);
CREATE TABLE tblProduct (
    ID int identity(1,1) NOT NULL,
    Name varchar(255) NOT NULL,
    PRIMARY KEY (ID),
	CategoryID int NOT NULL FOREIGN KEY REFERENCES tblCategory(ID),
	UserID uniqueidentifier NOT NULL FOREIGN KEY REFERENCES tblUser(Id)
);
CREATE TABLE tblShippingPeriod (
    ID int identity(1,1) NOT NULL,
    Name varchar(255) NOT NULL,
    PRIMARY KEY (ID),
	UserID uniqueidentifier NOT NULL FOREIGN KEY REFERENCES tblUser(Id)
);
--drop table tblProductPricing
CREATE TABLE tblProductPricing (
    ID int identity(1,1) NOT NULL,
    CreatedDate datetime NOT NULL,
	PricingDate datetime NOT NULL,
	Price decimal(18,4) NOT NULL,
	Notes nvarchar(max),
    PRIMARY KEY (ID),
	ProductID int NOT NULL FOREIGN KEY REFERENCES tblProduct(ID),
	UserID uniqueidentifier NOT NULL FOREIGN KEY REFERENCES tblUser(Id),
	ShippingPeriodID int NULL FOREIGN KEY REFERENCES tblShippingPeriod(ID)
);

--INSERT INTO tblUser  VALUES (NEWID(),'Administrator','Y1ZqR2VwWkhHTTMyb1pKMjRPOUhVVlZPN05QdFZEczV6Tlg1OGt4bVVHWT0','admin@yopmail.com','56896652','Admin')
alter PROCEDURE pFOBOfferList
AS
BEGIN
SELECT *
FROM [dbo].[tblProductPricing] pp
inner join [dbo].[tblProduct] p on p.ID = pp.ProductID
inner join [dbo].[tblCategory] c on c.ID = p.CategoryID
inner join [dbo].[tblUser] u on u.Id = pp.UserID
left join [dbo].[tblShippingPeriod] s on s.Id = pp.ShippingPeriodID
where pp.IsActive = 1
order by c.Sequance, c.Name
end
alter table [dbo].[tblProductPricing] alter column Price nvarchar(100)
alter table [dbo].[tblProductPricing] add IsActive bit
alter table [dbo].[tblCategory] add Sequance int