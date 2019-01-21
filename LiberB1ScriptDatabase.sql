-- Script Date: 21/01/2019 17:46  - ErikEJ.SqlCeScripting version 3.5.2.80
-- Database information:
-- Database: C:\Users\Erivelton\AppData\Roaming\LiberB1\LiberB1DB.db
-- ServerVersion: 3.24.0
-- DatabaseSize: 16 KB
-- Created: 12/01/2019 13:55

-- User Table information:
-- Number of tables: 3
-- Access: -1 row(s)
-- Configuration: -1 row(s)
-- Connection: -1 row(s)

SELECT 1;
PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE [Connection] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [serverName] text NOT NULL
, [dbName] text NOT NULL
, [dbPass] text NOT NULL
, [SapUser] text NOT NULL
, [SapPass] text NOT NULL
, [IsValid] image DEFAULT 0 NOT NULL
, [ConexaoLiber] text DEFAULT 'https:\\staging.edi.libercapital.com.br' NULL
, [userLiber] text NULL
, [PassLiber] text NULL
, [dbId] text DEFAULT sa NULL
, [PortLiber] text DEFAULT '5672' NULL
, [SQLType] text DEFAULT dst_MSSQL2016 NOT NULL
);
CREATE TABLE [Configuration] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [QueueName] text NOT NULL
, [OriginDoc] text NULL
, [TimeSAP] bigint NULL
, [FieldChaveAcesso] text NOT NULL
, [TimeStartSAP] text NULL
, [TimeStopSAP] text NULL
, [Import] int DEFAULT false NOT NULL
);
CREATE TABLE [Access] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [LastTimeSAP] text NOT NULL
, [LastTimeLiber] text NOT NULL
);
COMMIT;

