-- 1. Crear la base de datos
USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'ProductsDB')
BEGIN
    ALTER DATABASE ProductsDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE ProductsDB;
END
GO

CREATE DATABASE ProductsDB;
GO

USE ProductsDB;
GO

-- 2. Creación de tablas
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(200) NOT NULL,
    Stock INT NOT NULL,
    Precio DECIMAL NOT NULL,
    FechaCreacion DATETIME NOT NULL
);

CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(200) NOT NULL,
    Password NVARCHAR(200) NOT NULL
);
