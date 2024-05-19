```
USE [TestDb]
GO
/****** Object:  Table [dbo].[Tbl_BackupMeterBill]    Script Date: 5/19/2024 10:09:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_BackupMeterBill](
	[MeterBillId] [int] IDENTITY(1,1) NOT NULL,
	[MeterBillCode] [varchar](250) NOT NULL,
	[MeterBillFilePath] [varchar](250) NOT NULL,
	[MeterBillFileData] [varchar](500) NULL,
	[CreatedUserId] [varchar](225) NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[ModifiedUserId] [varchar](225) NULL,
	[ModifiedDateTime] [datetime] NULL,
 CONSTRAINT [PK_Tbl_BackupMeterBill] PRIMARY KEY CLUSTERED 
(
	[MeterBillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_MeterBill]    Script Date: 5/19/2024 10:09:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_MeterBill](
	[MeterBillId] [int] IDENTITY(1,1) NOT NULL,
	[MeterBillCode] [varchar](250) NOT NULL,
	[MeterBillFilePath] [varchar](250) NOT NULL,
	[MeterBillFileData] [varchar](500) NULL,
	[CreatedUserId] [varchar](225) NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[ModifiedUserId] [varchar](225) NULL,
	[ModifiedDateTime] [datetime] NULL,
 CONSTRAINT [PK_Tbl_MeterBill] PRIMARY KEY CLUSTERED 
(
	[MeterBillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


```