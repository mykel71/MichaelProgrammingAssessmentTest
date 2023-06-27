-- CREATE DATABASE SQLAssPrepsDb;

use SQLAssPrepsDb
-- CREATE SCHEMA openbox
/*
CREATE TABLE openbox.Customer(CustomerId int not null,
								Firstname nvarchar(50) not null,
								Surname nvarchar(50) not null,
								CustomerStatusId int not null,
								ChannelId int not null,
								CreateDateTime datetime2(7) not null,
								LevelId int not null);

CREATE TABLE openbox.Stock(StockId int not null,
							Description nvarchar(250) not null,
							Quantity int not null,
							StockStatusId int not null,
							StockGroupId int not null);

CREATE TABLE openbox.Sale(SaleId int not null,
							CustomerId int not null,
							CreateDatetime datetime2(7) not null,
							SaleStatus nvarchar(10) not null,
							CompleteDatetime datetime2(7) not null,
							SaleChannelId int not null);

CREATE TABLE openbox.SaleItem(SaleItemId int not null,
									 SaleId int not null,
									 StockId int not null,
									 Quantity int not null,
									 Price decimal not null,
									 Tax decimal not null,
									 Discount decimal not null);
									 */


/*
	QUESTION
	1.	A list of clients with a CustomerStatusId of 1, ordered by Surname then Firstname. 
	Columns required are Surname, Firstname, CustomerStatusId and CreateDateTime.
*/

SELECT Surname, Firstname, CustomerStatusId, CreateDateTime
FROM openbox.Customer
WHERE CustomerStatusId = 1
ORDER BY Surname, Firstname;


/*
	QUESTION 
	2.	A list of stock items that were sold in January 2018. Stock Description is the only required field. 
*/

SELECT * FROM openbox.Sale; SELECT * FROM openbox.SaleItem; SELECT * FROM openbox.Stock;

SELECT [Description] 
FROM openbox.Stock sk
JOIN openbox.SaleItem si
	ON sk.StockId = si.StockId
JOIN openbox.Sale s
	ON si.SaleId = s.SaleId
WHERE CreateDatetime BETWEEN '2018-01-01' AND '2018-02-01';

--OR using Sub-Query

SELECT [Description]
FROM openbox.Stock
WHERE StockId IN(
		 SELECT StockId 
		 FROM openbox.SaleItem
		 WHERE SaleId IN (
					SELECT SaleId 
					FROM openbox.Sale
					WHERE CreateDatetime BETWEEN '2018-01-01' AND '2018-02-01'));


/*
	QUESTION 
	3.	A list of stock items that were not sold in January 2018. 
			Stock Description is the only required field.
*/


SELECT [Description] 
FROM openbox.Stock sk
JOIN openbox.SaleItem si
	ON sk.StockId = si.StockId
JOIN openbox.Sale s
	ON si.SaleId = s.SaleId
WHERE CreateDatetime NOT BETWEEN '2018-01-01' AND '2018-02-01';

--OR using Sub-Query

SELECT [Description]
FROM openbox.Stock
WHERE StockId IN(
		 SELECT StockId 
		 FROM openbox.SaleItem
		 WHERE SaleId NOT IN (
					SELECT SaleId 
					FROM openbox.Sale
					WHERE CreateDatetime BETWEEN '2018-01-01' AND '2018-02-01'));


/*
	QUESTION 
	4.	A list of the top 10 highest selling stock items for January 2018. 
			Fields required are Description, Quantity Sold.
*/

SELECT TOP 10 sk.Description, SUM(si.Quantity) AS [Quantity Sold]
FROM openbox.Stock sk
JOIN openbox.SaleItem si
	ON sk.StockId = si.StockId
JOIN openbox.Sale s
	ON si.SaleId = s.SaleId
WHERE CreateDatetime BETWEEN '2018-01-01' AND '2018-02-01'
GROUP BY sk.Description;

/*
	QUESTION
	5.	A list of the top 10 customers for January 2018 in terms of sale value. 
		Fields required are Firstname, Surname, Number of Sales, Value of Sales.
*/

			-- ASSUMPTIONS --

--	Quantity Price - Discount = Value of each sale
--	Assumptions : Tax is a decimal = 0,15 
--	Assumption 2 : Discount is a decimal e.g 0,1
--	Assumption 3 : Discount is calculated before tax

SELECT TOP 10 
		c.Firstname, c.Surname, 
		COUNT(s.SaleId) AS [Number of Sales],
		SUM(si.Quantity * (si.Price - (si.Discount * si.Price)) * (1 + si.Tax)) AS [Value of Sales]
FROM openbox.Customer c
JOIN openbox.Sale s
	ON c.CustomerId = s.CustomerId
JOIN openbox.SaleItem si
	ON s.SaleId = si.SaleId
WHERE s.CreateDatetime BETWEEN '2017-12-31' AND '2018-02-01'
GROUP BY
		c.Firstname, c.Surname;

-- Otherway round -- 
-- NB: find this time consuming and a bit complicated however here we go lol

--		Quantity Price - Discount = Value of each sale
--		Assumptions : Tax is a decimal = 0,15 
--		Assumption 2 : Discount is a decimal e.g 0,1
--		Cursor return the value only for the time period im intrested in

-- a stored Procedure that will return the customers needed
Create Procedure sp_Top10CustomersForJanuary2018

AS
	BEGIN
	--Create a temporary table
Create Table #SaleValue (SaleId int not null, SaleItemId int not null, [Value] decimal not null)


DECLARE @SaleItemId int, @StockId int, @Quantity int, @Value decimal 

DECLARE Sale_cursor CURSOR FOR 
SELECT Distinct SaleItemId 
FROM openbox.SaleItem si
WHERE SaleId IN (
	Select SaleId
	From openbox.Sale s
	Where CreateDateTime Between '2018-01-01' and '2010-02-01') 

OPEN Sale_cursor  
FETCH NEXT FROM Sale_cursor INTO @SaleItemId 

WHILE @@FETCH_STATUS = 0  
BEGIN  

	 Insert into #SaleValue(SaleId, SaleItemId, [Value])

	 Values 
			((
			Select si.SaleId From openbox.SaleItem si Where SaleItemId = @SaleItemId
			), 
			@SaleItemId, 
			-- Calculate the value of each item
			(
			Select (Quantity *(Price - (Discount * Price)) * (1+ Tax) )
			From openbox.SaleItem si
			Where SaleItemId = @SaleItemId
			))

      FETCH NEXT FROM Sale_cursor INTO @SaleItemId
END 

CLOSE Sale_cursor  
DEALLOCATE Sale_cursor 


Select TOP 10 c.Firstname, c.Surname, NumberOfSales AS [Number of Sales], ValueOfSales AS [Value Of Sales]
From openbox.Customer c
INNER JOIN (
SELECT CustomerId, Count(SaleId) AS NumberOfSales From openbox.Sale
Where SaleId IN (Select SaleId
	From openbox.Sale s
	Where CreateDateTime Between '2018-01-01' and '2010-02-01')
GROUP BY CustomerId

)AS T1 ON c.CustomerId = T1.CustomerId

INNER JOIN (

SELECT CustomerId, SUM(SaleTotal) AS ValueOfSales FROM 
(
SELECT CustomerId, s.SaleId,SaleTotal FROM 
(SELECT SaleId, Sum(Value)AS SaleTotal From #SaleValue) AS Ts
INNER JOIN openbox.Sale s ON s.SaleId = Ts.SaleId) AS tt


)AS T2 ON c.CustomerId = T2.CustomerId
Order By [Value Of Sales] DESC
Drop Table #SaleValue
END;

