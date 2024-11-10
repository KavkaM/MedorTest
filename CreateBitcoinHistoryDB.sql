-- 1 - Create Database
CREATE DATABASE [BitcoinHistoryDB]
GO
USE[BitcoinHistoryDB];
GO

-- 2 - Create Currencies Table
CREATE TABLE Currencies (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Code VARCHAR(3) NOT NULL
);

-- 3 - Create Records Table
CREATE TABLE Records (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UpdatedAt DATETIME NOT NULL,
    Note VARCHAR(500) NULL
);

-- 4 - Create Rates Table
CREATE TABLE Rates (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Value DECIMAL(18, 3) NOT NULL,
    RecordId INT NOT NULL,
    CurrencyId INT NOT NULL,
    FOREIGN KEY (RecordId) REFERENCES Records(Id),
    FOREIGN KEY (CurrencyId) REFERENCES Currencies(Id)
);

-- 5 - Add CZK, EUR, USD, GBP currency
INSERT INTO Currencies (Code) VALUES('CZK')
INSERT INTO Currencies (Code) VALUES('EUR')
INSERT INTO Currencies (Code) VALUES('USD')
INSERT INTO Currencies (Code) VALUES('GBP')