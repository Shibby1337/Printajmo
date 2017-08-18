
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/17/2017 14:24:10
-- Generated from EDMX file: D:\Websites\Printajmo\Printajmo\Printajmo\Models\Tiskarne.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Printajmo];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tiskarne]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tiskarne];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tiskarne'
CREATE TABLE [dbo].[tiskarne] (
    [idtiskarne] int IDENTITY(1,1) NOT NULL,
    [naziv] varchar(45)  NULL,
    [a4cb] decimal(10,2)  NULL,
    [a4barvno] decimal(10,2)  NULL,
    [a4cboboje] decimal(10,2)  NULL,
    [a4barvnooboje] decimal(10,2)  NULL,
    [ulica] varchar(45)  NULL,
    [postnast] int  NULL,
    [mesto] varchar(45)  NULL,
    [email] varchar(45)  NULL,
    [telefonska] varchar(45)  NULL,
    [dodatno] varchar(200)  NULL,
    [longitude] float  NOT NULL,
    [latitude] float  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [idtiskarne] in table 'tiskarne'
ALTER TABLE [dbo].[tiskarne]
ADD CONSTRAINT [PK_tiskarne]
    PRIMARY KEY CLUSTERED ([idtiskarne] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------