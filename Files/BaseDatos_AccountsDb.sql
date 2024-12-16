IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241216043717_Initial migration'
)
BEGIN
    CREATE TABLE [Accounts] (
        [Number] nvarchar(20) NOT NULL,
        [Type] tinyint NOT NULL,
        [Balance] decimal(10,2) NOT NULL,
        [Status] bit NOT NULL,
        [ClientId] int NOT NULL,
        CONSTRAINT [PK_Accounts] PRIMARY KEY ([Number])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241216043717_Initial migration'
)
BEGIN
    CREATE TABLE [Movements] (
        [Id] int NOT NULL IDENTITY,
        [AccountNumber] nvarchar(20) NOT NULL,
        [DateTime] datetime2 NOT NULL,
        [Amount] decimal(10,2) NOT NULL,
        [Balance] decimal(10,2) NOT NULL,
        [Description] nvarchar(200) NOT NULL,
        CONSTRAINT [PK_Movements] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Movements_Accounts_AccountNumber] FOREIGN KEY ([AccountNumber]) REFERENCES [Accounts] ([Number]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241216043717_Initial migration'
)
BEGIN
    CREATE INDEX [IX_Accounts_ClientId] ON [Accounts] ([ClientId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241216043717_Initial migration'
)
BEGIN
    CREATE INDEX [IX_Movements_AccountNumber] ON [Movements] ([AccountNumber]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241216043717_Initial migration'
)
BEGIN
    CREATE INDEX [IX_Movements_DateTime] ON [Movements] ([DateTime]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241216043717_Initial migration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241216043717_Initial migration', N'8.0.11');
END;
GO

COMMIT;
GO

