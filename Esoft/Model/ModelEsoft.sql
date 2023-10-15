
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/15/2023 11:58:29
-- Generated from EDMX file: D:\projectsVS\College\Esoft\Esoft\Model\ModelEsoft.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [esoftDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Demands__IdClien__6477ECF3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Demands] DROP CONSTRAINT [FK__Demands__IdClien__6477ECF3];
GO
IF OBJECT_ID(N'[dbo].[FK__Demands__IdRealt__656C112C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Demands] DROP CONSTRAINT [FK__Demands__IdRealt__656C112C];
GO
IF OBJECT_ID(N'[dbo].[FK__Demands__IdTypeO__66603565]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Demands] DROP CONSTRAINT [FK__Demands__IdTypeO__66603565];
GO
IF OBJECT_ID(N'[dbo].[FK__Offers__IdClient__5812160E]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Offers] DROP CONSTRAINT [FK__Offers__IdClient__5812160E];
GO
IF OBJECT_ID(N'[dbo].[FK__Offers__IdEstate__59FA5E80]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Offers] DROP CONSTRAINT [FK__Offers__IdEstate__59FA5E80];
GO
IF OBJECT_ID(N'[dbo].[FK__Offers__IdRealto__59063A47]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Offers] DROP CONSTRAINT [FK__Offers__IdRealto__59063A47];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Clients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clients];
GO
IF OBJECT_ID(N'[dbo].[Demands]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Demands];
GO
IF OBJECT_ID(N'[dbo].[Estates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Estates];
GO
IF OBJECT_ID(N'[dbo].[Offers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Offers];
GO
IF OBJECT_ID(N'[dbo].[Realtors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Realtors];
GO
IF OBJECT_ID(N'[dbo].[TypesOfEstate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypesOfEstate];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LastName] nvarchar(50)  NULL,
    [FirstName] nvarchar(30)  NULL,
    [Patronymic] nvarchar(30)  NULL,
    [Email] nvarchar(50)  NULL,
    [MobileNumber] nvarchar(11)  NULL
);
GO

-- Creating table 'Demands'
CREATE TABLE [dbo].[Demands] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IdClient] int  NOT NULL,
    [IdRealtor] int  NOT NULL,
    [IdTypeOfEstate] int  NOT NULL,
    [MinPrice] int  NULL,
    [MaxPrice] int  NULL,
    [MinTotalArea] float  NULL,
    [MaxTotalArea] float  NULL,
    [MinNumbOfRooms] int  NULL,
    [MaxNumbOfRooms] int  NULL,
    [MinFloorNumber] int  NULL,
    [MaxFloorNumber] int  NULL,
    [MinNumbOfStroyes] int  NULL,
    [MaxNumbOfStroyes] int  NULL,
    [CityAddress] nvarchar(50)  NULL,
    [StreetAddress] nvarchar(100)  NULL,
    [HouseNumber] nvarchar(10)  NULL,
    [ApartmentNumber] nvarchar(10)  NULL
);
GO

-- Creating table 'Estates'
CREATE TABLE [dbo].[Estates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CityAddress] nvarchar(50)  NULL,
    [StreetAddress] nvarchar(100)  NULL,
    [HouseNumber] nvarchar(10)  NULL,
    [ApartmentNumber] nvarchar(10)  NULL,
    [Latitude] float  NULL,
    [Longtitude] float  NULL,
    [FloorNumber] int  NULL,
    [NumberOfStroyes] int  NULL,
    [NumberOfRooms] int  NULL,
    [TotalArea] float  NULL
);
GO

-- Creating table 'Offers'
CREATE TABLE [dbo].[Offers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IdClient] int  NOT NULL,
    [IdRealtor] int  NOT NULL,
    [IdEstate] int  NOT NULL,
    [Price] int  NOT NULL
);
GO

-- Creating table 'Realtors'
CREATE TABLE [dbo].[Realtors] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [FirstName] nvarchar(30)  NOT NULL,
    [Patronymic] nvarchar(30)  NULL,
    [Commission] int  NULL
);
GO

-- Creating table 'TypesOfEstates'
CREATE TABLE [dbo].[TypesOfEstates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TypeName] nvarchar(30)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Demands'
ALTER TABLE [dbo].[Demands]
ADD CONSTRAINT [PK_Demands]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Estates'
ALTER TABLE [dbo].[Estates]
ADD CONSTRAINT [PK_Estates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Offers'
ALTER TABLE [dbo].[Offers]
ADD CONSTRAINT [PK_Offers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Realtors'
ALTER TABLE [dbo].[Realtors]
ADD CONSTRAINT [PK_Realtors]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TypesOfEstates'
ALTER TABLE [dbo].[TypesOfEstates]
ADD CONSTRAINT [PK_TypesOfEstates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdClient] in table 'Demands'
ALTER TABLE [dbo].[Demands]
ADD CONSTRAINT [FK__Demands__IdClien__6477ECF3]
    FOREIGN KEY ([IdClient])
    REFERENCES [dbo].[Clients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Demands__IdClien__6477ECF3'
CREATE INDEX [IX_FK__Demands__IdClien__6477ECF3]
ON [dbo].[Demands]
    ([IdClient]);
GO

-- Creating foreign key on [IdClient] in table 'Offers'
ALTER TABLE [dbo].[Offers]
ADD CONSTRAINT [FK__Offers__IdClient__5812160E]
    FOREIGN KEY ([IdClient])
    REFERENCES [dbo].[Clients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Offers__IdClient__5812160E'
CREATE INDEX [IX_FK__Offers__IdClient__5812160E]
ON [dbo].[Offers]
    ([IdClient]);
GO

-- Creating foreign key on [IdRealtor] in table 'Demands'
ALTER TABLE [dbo].[Demands]
ADD CONSTRAINT [FK__Demands__IdRealt__656C112C]
    FOREIGN KEY ([IdRealtor])
    REFERENCES [dbo].[Realtors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Demands__IdRealt__656C112C'
CREATE INDEX [IX_FK__Demands__IdRealt__656C112C]
ON [dbo].[Demands]
    ([IdRealtor]);
GO

-- Creating foreign key on [IdTypeOfEstate] in table 'Demands'
ALTER TABLE [dbo].[Demands]
ADD CONSTRAINT [FK__Demands__IdTypeO__66603565]
    FOREIGN KEY ([IdTypeOfEstate])
    REFERENCES [dbo].[TypesOfEstates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Demands__IdTypeO__66603565'
CREATE INDEX [IX_FK__Demands__IdTypeO__66603565]
ON [dbo].[Demands]
    ([IdTypeOfEstate]);
GO

-- Creating foreign key on [IdEstate] in table 'Offers'
ALTER TABLE [dbo].[Offers]
ADD CONSTRAINT [FK__Offers__IdEstate__59FA5E80]
    FOREIGN KEY ([IdEstate])
    REFERENCES [dbo].[Estates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Offers__IdEstate__59FA5E80'
CREATE INDEX [IX_FK__Offers__IdEstate__59FA5E80]
ON [dbo].[Offers]
    ([IdEstate]);
GO

-- Creating foreign key on [IdRealtor] in table 'Offers'
ALTER TABLE [dbo].[Offers]
ADD CONSTRAINT [FK__Offers__IdRealto__59063A47]
    FOREIGN KEY ([IdRealtor])
    REFERENCES [dbo].[Realtors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Offers__IdRealto__59063A47'
CREATE INDEX [IX_FK__Offers__IdRealto__59063A47]
ON [dbo].[Offers]
    ([IdRealtor]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------