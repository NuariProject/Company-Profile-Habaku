/*
 Navicat Premium Data Transfer

 Source Server         : Latihan
 Source Server Type    : SQL Server
 Source Server Version : 15002000 (15.00.2000)
 Source Host           : localhost:1433
 Source Catalog        : HabakuDB
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 15002000 (15.00.2000)
 File Encoding         : 65001

 Date: 09/03/2023 22:01:03
*/


-- ----------------------------
-- Table structure for Menu
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu]') AND type IN ('U'))
	DROP TABLE [dbo].[Menu]
GO

CREATE TABLE [dbo].[Menu] (
  [menu_id] int  IDENTITY(1,1) NOT NULL,
  [menu_name] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [status] bit  NULL,
  [created_at] datetime  NULL,
  [created_by] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [modified_at] datetime  NULL,
  [modified_by] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Menu] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Menu
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Menu] ON
GO

INSERT INTO [dbo].[Menu] ([menu_id], [menu_name], [status], [created_at], [created_by], [modified_at], [modified_by]) VALUES (N'1', N'Home', N'0', N'2023-03-04 11:51:29.220', N'Bagas Luar Biasa Tampan', NULL, NULL)
GO

INSERT INTO [dbo].[Menu] ([menu_id], [menu_name], [status], [created_at], [created_by], [modified_at], [modified_by]) VALUES (N'2', N'About Us', N'0', N'2023-03-04 11:59:49.590', N'Bagas Yang Paling Tampan', N'2023-03-04 14:40:29.117', N'Si Paling Tampan')
GO

INSERT INTO [dbo].[Menu] ([menu_id], [menu_name], [status], [created_at], [created_by], [modified_at], [modified_by]) VALUES (N'3', N'Product', N'0', N'2023-03-04 13:40:22.387', N'Bagas Mempesona', NULL, NULL)
GO

INSERT INTO [dbo].[Menu] ([menu_id], [menu_name], [status], [created_at], [created_by], [modified_at], [modified_by]) VALUES (N'4', N'Contact Us', N'0', N'2023-03-04 15:04:57.567', N'Bagas Wijaya', NULL, NULL)
GO

SET IDENTITY_INSERT [dbo].[Menu] OFF
GO


-- ----------------------------
-- Table structure for User
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type IN ('U'))
	DROP TABLE [dbo].[User]
GO

CREATE TABLE [dbo].[User] (
  [user_id] int  IDENTITY(1,1) NOT NULL,
  [user_name] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [password] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [role] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [created_at] datetime  NULL,
  [created_by] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [modified_at] datetime  NULL,
  [modified_by] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[User] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of User
-- ----------------------------
SET IDENTITY_INSERT [dbo].[User] ON
GO

INSERT INTO [dbo].[User] ([user_id], [user_name], [password], [role], [created_at], [created_by], [modified_at], [modified_by]) VALUES (N'1', N'Bagas Tampan', N'Admin123', N'Super Duper Admin', N'2023-03-09 20:29:44.300', N'Bagas Amazing', N'2023-03-09 21:57:59.440', N'string')
GO

SET IDENTITY_INSERT [dbo].[User] OFF
GO


-- ----------------------------
-- Auto increment value for Menu
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Menu]', RESEED, 1000)
GO


-- ----------------------------
-- Primary Key structure for table Menu
-- ----------------------------
ALTER TABLE [dbo].[Menu] ADD CONSTRAINT [PK__Menu__4CA0FADC0BC66416] PRIMARY KEY CLUSTERED ([menu_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for User
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[User]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table User
-- ----------------------------
ALTER TABLE [dbo].[User] ADD CONSTRAINT [PK__User__B9BE370F5CBA3204] PRIMARY KEY CLUSTERED ([user_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

