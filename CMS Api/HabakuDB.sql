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

 Date: 14/03/2023 19:46:20
*/


-- ----------------------------
-- Table structure for Content
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND type IN ('U'))
	DROP TABLE [dbo].[Content]
GO

CREATE TABLE [dbo].[Content] (
  [content_id] int  IDENTITY(1,1) NOT NULL,
  [section_id] int  NULL,
  [header] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [title] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [description] varchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [image] varchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [url] varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [created_at] datetime  NULL,
  [created_by] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [modified_at] datetime  NULL,
  [modified_by] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [status] bit  NULL
)
GO

ALTER TABLE [dbo].[Content] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Content
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Content] ON
GO

INSERT INTO [dbo].[Content] ([content_id], [section_id], [header], [title], [description], [image], [url], [created_at], [created_by], [modified_at], [modified_by], [status]) VALUES (N'1', N'1', N'Ini Header', N'Ini Title', N'Ini Description', N'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', N'iniurl.com', N'2023-03-12 23:00:32.860', N'Bagas GG', N'2023-03-13 07:20:09.520', N'Wow Bagas', N'1')
GO

SET IDENTITY_INSERT [dbo].[Content] OFF
GO


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

INSERT INTO [dbo].[Menu] ([menu_id], [menu_name], [status], [created_at], [created_by], [modified_at], [modified_by]) VALUES (N'1', N'Beranda', N'1', N'2023-03-04 11:51:29.220', N'Bagas Luar Biasa Tampan', NULL, NULL)
GO

INSERT INTO [dbo].[Menu] ([menu_id], [menu_name], [status], [created_at], [created_by], [modified_at], [modified_by]) VALUES (N'2', N'Produk', N'1', N'2023-03-04 11:59:49.590', N'Bagas Yang Paling Tampan', N'2023-03-12 22:41:40.333', N'bagas.tampan@projectkita.id')
GO

INSERT INTO [dbo].[Menu] ([menu_id], [menu_name], [status], [created_at], [created_by], [modified_at], [modified_by]) VALUES (N'3', N'Artikel', N'1', N'2023-03-04 13:40:22.387', N'Bagas Mempesona', NULL, NULL)
GO

INSERT INTO [dbo].[Menu] ([menu_id], [menu_name], [status], [created_at], [created_by], [modified_at], [modified_by]) VALUES (N'4', N'Tentang Kami', N'1', N'2023-03-04 15:04:57.567', N'Bagas Wijaya', N'2023-03-12 21:17:50.060', N'bagas.tampan@projectkita.id')
GO

INSERT INTO [dbo].[Menu] ([menu_id], [menu_name], [status], [created_at], [created_by], [modified_at], [modified_by]) VALUES (N'5', N'Hubungi Kami', N'1', N'2023-03-14 13:50:00.000', N'Bagas GG', NULL, NULL)
GO

INSERT INTO [dbo].[Menu] ([menu_id], [menu_name], [status], [created_at], [created_by], [modified_at], [modified_by]) VALUES (N'6', N'Detail Artikel', N'1', N'2023-03-14 14:04:03.000', N'Bagas GG', NULL, NULL)
GO

SET IDENTITY_INSERT [dbo].[Menu] OFF
GO


-- ----------------------------
-- Table structure for Section
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Section]') AND type IN ('U'))
	DROP TABLE [dbo].[Section]
GO

CREATE TABLE [dbo].[Section] (
  [section_id] int  IDENTITY(1,1) NOT NULL,
  [menu_id] int  NULL,
  [section_name] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [section_number] int  NULL,
  [created_at] datetime  NULL,
  [created_by] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [modified_at] datetime  NULL,
  [modified_by] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [section_approve] int  NULL,
  [status] bit  NULL
)
GO

ALTER TABLE [dbo].[Section] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Section
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Section] ON
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'1', N'1', N'Solusi Tepat Belanja
Hemat dan Cepat.', N'1', N'2023-03-11 13:04:29.253', N'Bagas Luar Biasa', N'2023-03-12 11:17:58.283', N'Bagas Tampan', N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'2', N'1', N'Apa Itu Habaku?', N'2', N'2023-03-12 11:15:53.220', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'3', N'1', N'Kenapa Harus Belanja di Habaku?', N'3', N'2023-03-12 11:16:17.440', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'4', N'1', N'Apa Kata Mereka?', N'4', N'2023-03-14 13:42:47.000', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'5', N'1', N'Download Aplikasi Habaku Sekarang!', N'5', N'2023-03-14 13:44:12.000', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'6', N'2', N'Habaku’s Solution', N'1', N'2023-03-14 13:45:15.000', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'7', N'2', N'Habaku’s Ecosystem', N'2', N'2023-03-14 13:45:49.000', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'8', N'2', N'Metode Pembayaran', N'3', N'2023-03-14 13:45:49.000', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'9', N'2', N'Promo Spesial Buat Kamu!', N'4', N'2023-03-14 13:45:49.000', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'10', N'2', N'Download Aplikasi Habaku Sekarang!', N'5', N'2023-03-14 13:45:49.000', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'11', N'3', N'Cerita Teman Habaku', N'1', N'2023-03-12 11:15:53.220', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'12', N'3', N'Artikel Terbaru', N'2', N'2023-03-12 11:16:17.440', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'13', N'6', N'Judul Artikel', N'1', N'2023-03-14 13:42:47.000', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'14', N'6', N'Artikel Terbaru', N'2', N'2023-03-14 13:44:12.000', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'15', N'4', N'Tentang Habaku', N'1', N'2023-03-14 13:45:15.000', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'16', N'4', N'Visi', N'2', N'2023-03-14 13:45:49.000', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'17', N'4', N'Misi', N'3', N'2023-03-14 13:45:49.000', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'18', N'4', N'Slogan', N'4', N'2023-03-14 13:45:49.000', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'19', N'5', N'Alamat Habaku', N'1', N'2023-03-14 13:45:49.000', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'20', N'5', N'Kritik dan Saran', N'2', N'2023-03-14 13:45:49.000', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

INSERT INTO [dbo].[Section] ([section_id], [menu_id], [section_name], [section_number], [created_at], [created_by], [modified_at], [modified_by], [section_approve], [status]) VALUES (N'21', N'5', N'Ajukan Pertanyaan', N'3', N'2023-03-14 13:45:49.000', N'Bagas Tampan', NULL, NULL, N'1', N'1')
GO

SET IDENTITY_INSERT [dbo].[Section] OFF
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
  [modified_by] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [status] bit  NULL
)
GO

ALTER TABLE [dbo].[User] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of User
-- ----------------------------
SET IDENTITY_INSERT [dbo].[User] ON
GO

INSERT INTO [dbo].[User] ([user_id], [user_name], [password], [role], [created_at], [created_by], [modified_at], [modified_by], [status]) VALUES (N'1', N'bagas.tampan@projectkita.id', N'Admin123', N'SuperAdmin', N'2023-03-09 20:29:44.300', N'Bagas Amazing', N'2023-03-09 21:57:59.440', N'string', N'1')
GO

INSERT INTO [dbo].[User] ([user_id], [user_name], [password], [role], [created_at], [created_by], [modified_at], [modified_by], [status]) VALUES (N'2', N'Nuari Project2', N'12345678', N'Super Admin1', N'2023-03-10 00:03:14.247', N'Farhan', NULL, NULL, N'1')
GO

INSERT INTO [dbo].[User] ([user_id], [user_name], [password], [role], [created_at], [created_by], [modified_at], [modified_by], [status]) VALUES (N'3', N'Bagas Lagi', N'Admin123321', N'Hamba Allah', N'2023-03-10 00:14:19.113', N'Hamba Allah', NULL, NULL, N'1')
GO

SET IDENTITY_INSERT [dbo].[User] OFF
GO


-- ----------------------------
-- Auto increment value for Content
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Content]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table Content
-- ----------------------------
ALTER TABLE [dbo].[Content] ADD CONSTRAINT [PK__Content__655FE510A0D236DF] PRIMARY KEY CLUSTERED ([content_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Menu
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Menu]', RESEED, 6)
GO


-- ----------------------------
-- Primary Key structure for table Menu
-- ----------------------------
ALTER TABLE [dbo].[Menu] ADD CONSTRAINT [PK__Menu__4CA0FADC0BC66416] PRIMARY KEY CLUSTERED ([menu_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Section
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Section]', RESEED, 21)
GO


-- ----------------------------
-- Primary Key structure for table Section
-- ----------------------------
ALTER TABLE [dbo].[Section] ADD CONSTRAINT [PK__Section__F842676A49FED687] PRIMARY KEY CLUSTERED ([section_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for User
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[User]', RESEED, 3)
GO


-- ----------------------------
-- Primary Key structure for table User
-- ----------------------------
ALTER TABLE [dbo].[User] ADD CONSTRAINT [PK__User__B9BE370F5CBA3204] PRIMARY KEY CLUSTERED ([user_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table Content
-- ----------------------------
ALTER TABLE [dbo].[Content] ADD CONSTRAINT [FK_CONTENT_SECTION] FOREIGN KEY ([section_id]) REFERENCES [dbo].[Section] ([section_id]) ON DELETE NO ACTION ON UPDATE CASCADE
GO


-- ----------------------------
-- Foreign Keys structure for table Section
-- ----------------------------
ALTER TABLE [dbo].[Section] ADD CONSTRAINT [FK_SECTION_MENU] FOREIGN KEY ([menu_id]) REFERENCES [dbo].[Menu] ([menu_id]) ON DELETE NO ACTION ON UPDATE CASCADE
GO

