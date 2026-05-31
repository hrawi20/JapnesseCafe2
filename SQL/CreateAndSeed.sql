IF DB_ID(N'JapnesseCafeDb') IS NULL
BEGIN
    CREATE DATABASE JapnesseCafeDb;
END
GO

USE JapnesseCafeDb;
GO

IF OBJECT_ID(N'dbo.MenuItems', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.MenuItems
    (
        Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_MenuItems PRIMARY KEY,
        Name NVARCHAR(120) NOT NULL,
        Description NVARCHAR(600) NOT NULL,
        Price DECIMAL(18,2) NOT NULL,
        Category NVARCHAR(60) NOT NULL,
        ImageData VARBINARY(MAX) NULL,
        ImageMimeType NVARCHAR(80) NULL,
        IsAvailable BIT NOT NULL,
        CreatedAt DATETIME NOT NULL
    );
END
GO

IF OBJECT_ID(N'dbo.Products', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Products
    (
        Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Products PRIMARY KEY,
        Name NVARCHAR(120) NOT NULL,
        Description NVARCHAR(600) NOT NULL,
        Price DECIMAL(18,2) NOT NULL,
        Category NVARCHAR(60) NOT NULL,
        ImageData VARBINARY(MAX) NULL,
        ImageMimeType NVARCHAR(80) NULL,
        IsAvailable BIT NOT NULL,
        CreatedAt DATETIME NOT NULL
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.MenuItems)
BEGIN
    INSERT dbo.MenuItems (Name, Description, Price, Category, IsAvailable, CreatedAt) VALUES
    (N'Matcha Latte', N'Creamy ceremonial matcha with steamed milk and a tiny sakura foam art finish.', 4.75, N'Drinks', 1, GETDATE()),
    (N'Sakura Iced Tea', N'Refreshing floral tea with cherry blossom syrup, citrus, and crystal ice.', 4.25, N'Drinks', 1, GETDATE()),
    (N'Taiyaki', N'Warm fish-shaped cake filled with sweet red bean custard.', 3.95, N'Desserts', 1, GETDATE()),
    (N'Mochi Dessert Plate', N'Soft mochi trio with strawberry, matcha, and vanilla bean flavors.', 6.50, N'Desserts', 1, GETDATE()),
    (N'Onigiri Set', N'Rice ball trio with salmon, tuna mayo, and umeboshi fillings.', 7.25, N'Snacks', 1, GETDATE()),
    (N'Chicken Katsu Curry', N'Crispy chicken cutlet over rice with mild Japanese curry.', 12.95, N'Main Course Meals', 1, GETDATE()),
    (N'Ramen Bowl', N'Comforting shoyu ramen with egg, nori, scallions, and chashu.', 11.95, N'Main Course Meals', 1, GETDATE()),
    (N'Anime Bento Box', N'Colorful cafe bento with karaage, tamagoyaki, rice, and seasonal sides.', 13.50, N'Main Course Meals', 1, GETDATE());
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Products)
BEGIN
    INSERT dbo.Products (Name, Description, Price, Category, IsAvailable, CreatedAt) VALUES
    (N'Anime Hero Figure', N'Poseable hero figure with display stand and effect parts.', 29.99, N'Figures', 1, GETDATE()),
    (N'Sakura Magical Girl Figure', N'Pastel magical girl figure with sakura wand and ribbon base.', 34.99, N'Figures', 1, GETDATE()),
    (N'Anime T-Shirt', N'Soft cotton tee with cafe mascot artwork.', 19.99, N'T-Shirts', 1, GETDATE()),
    (N'Shonen Power T-Shirt', N'Bold action-inspired graphic tee for tournament arcs and coffee runs.', 21.99, N'T-Shirts', 1, GETDATE()),
    (N'Keychain Set', N'Acrylic charm set featuring drinks, desserts, and mascot faces.', 9.99, N'Anime Accessories', 1, GETDATE()),
    (N'Anime Pin Pack', N'Enamel pin pack with sakura, ramen, cat, and star motifs.', 8.50, N'Anime Accessories', 1, GETDATE()),
    (N'Manga Tote Bag', N'Canvas tote with manga panel pattern and sturdy handles.', 16.99, N'Anime Accessories', 1, GETDATE()),
    (N'Cat Ear Headband', N'Cute cafe cosplay headband with soft plush ears.', 12.99, N'Anime Accessories', 1, GETDATE());
END
GO
