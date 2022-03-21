--This script file is auto generated using entity framework core(Script-Migration command)

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

CREATE TABLE [Actors] (
    [ActorId] int NOT NULL IDENTITY,
    [ActorName] nvarchar(max) NOT NULL,
    [Bio] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Actors] PRIMARY KEY ([ActorId])
);
GO

CREATE TABLE [Producers] (
    [ProducerId] int NOT NULL IDENTITY,
    [ProducerName] nvarchar(max) NOT NULL,
    [Bio] nvarchar(max) NOT NULL,
    [Company] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Producers] PRIMARY KEY ([ProducerId])
);
GO

CREATE TABLE [Movies] (
    [MovieId] int NOT NULL IDENTITY,
    [MovieName] nvarchar(max) NOT NULL,
    [DateOfRelease] datetime2 NOT NULL,
    [Plot] nvarchar(max) NOT NULL,
    [ProducerId] int NOT NULL,
    CONSTRAINT [PK_Movies] PRIMARY KEY ([MovieId]),
    CONSTRAINT [FK_Movies_Producers_ProducerId] FOREIGN KEY ([ProducerId]) REFERENCES [Producers] ([ProducerId]) ON UPDATE CASCADE ON DELETE CASCADE
);
GO

CREATE TABLE [ActorMovie] (
    [ActorsActorId] int NOT NULL,
    [MoviesMovieId] int NOT NULL,
    CONSTRAINT [PK_ActorMovie] PRIMARY KEY ([ActorsActorId], [MoviesMovieId]),
    CONSTRAINT [FK_ActorMovie_Actors_ActorsActorId] FOREIGN KEY ([ActorsActorId]) REFERENCES [Actors] ([ActorId]) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT [FK_ActorMovie_Movies_MoviesMovieId] FOREIGN KEY ([MoviesMovieId]) REFERENCES [Movies] ([MovieId]) ON UPDATE CASCADE ON DELETE CASCADE
);
GO

CREATE INDEX [IX_ActorMovie_MoviesMovieId] ON [ActorMovie] ([MoviesMovieId]);
GO

CREATE INDEX [IX_Movies_ProducerId] ON [Movies] ([ProducerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220321073347_InitialiseDB', N'6.0.3');
GO

COMMIT;
GO



