DROP TABLE [dbo].[OLD_User]
GO
DROP TABLE [dbo].[OLD_Team_Year]
GO
DROP TABLE [dbo].[OLD_Team]
GO
DROP TABLE [dbo].[OLD_Schedule_Team]
GO
DROP TABLE [dbo].[OLD_Schedule]
GO
DROP TABLE [dbo].[OLD_Player_Stats]
GO
DROP TABLE [dbo].[OLD_Player]
GO
DROP TABLE [dbo].[OLD_PageContent]
GO
DROP TABLE [dbo].[OLD_News]
GO
DROP TABLE [dbo].[OLD_GameType]
GO
DROP TABLE [dbo].[OLD_GameStatus]
GO
DROP TABLE [dbo].[OLD_Division]
GO
DROP TABLE [dbo].[OLD_Conference]
GO
DROP TABLE [dbo].[OLD_Contact]
GO
DROP TABLE [dbo].[OLD_Church]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OLD_User](
	[U_Id] int NULL,
	[U_Name] varchar(20) NULL,
	[U_Password] varchar(20) NULL,
	[U_CO_Id] int NULL,
	[U_T_Id] int NULL,
	[U_Admin] int NULL,
	[U_Create_Date] varchar(20) NULL,
	[U_Create_User] varchar(20) NULL,
	[U_Update_Date] varchar(20) NULL,
	[U_Update_User] varchar(20) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OLD_Team_Year](
	[TY_Id] int NULL,
	[TY_Year] int NULL,
	[TY_T_Id] int NULL,
	[TY_D_Id] int NULL,
	[TY_Name] varchar(50) NULL,
	[TY_CH_Id] int NULL,
	[TY_CO_Id] int NULL,
	[TY_Keeps_Stats] int NULL,
	[TY_ClinchChar] varchar(1) NULL,
	[TY_Update_Date] varchar(20) NULL,
	[TY_Update_User] varchar(20) NULL,
	[TY_Create_Date] varchar(20) NULL,
	[TY_Create_User] varchar(20) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OLD_Team](
	[T_Id] int NULL,
	[T_Paid] int NULL,
	[T_Field_Directions] varchar(4000) NULL,
	[T_Comments] varchar(4000) NULL,
	[T_Update_Date] varchar(20) NULL,
	[T_Update_User] varchar(20) NULL,
	[T_Create_Date] varchar(20) NULL,
	[T_Create_User] varchar(20) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OLD_Schedule_Team](
	[ST_S_Id] int NULL,
	[ST_T_Id] int NULL,
	[ST_Host] int NULL,
	[ST_Runs_Scored] int NULL,
	[ST_Update_Date] varchar(20) NULL,
	[ST_Update_User] varchar(20) NULL,
	[ST_Create_Date] varchar(20) NULL,
	[ST_Create_User] varchar(20) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OLD_Schedule](
	[S_Id] int NULL,
	[S_Date] varchar(20) NULL,
	[S_GS_Id] int NULL,
	[S_GT_Id] int NULL,
	[S_Update_Date] varchar(20) NULL,
	[S_Update_User] varchar(20) NULL,
	[S_Create_Date] varchar(20) NULL,
	[S_Create_User] varchar(20) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OLD_Player_Stats](
	[PS_P_Id] int NULL,
	[PS_S_Id] int NULL,
	[PS_T_Id] int NULL,
	[PS_Order] int NULL,
	[PS_1B] int NULL,
	[PS_2B] int NULL,
	[PS_3B] int NULL,
	[PS_HR] int NULL,
	[PS_BB] int NULL,
	[PS_Sac] int NULL,
	[PS_Outs] int NULL,
	[PS_FC] int NULL,
	[PS_E] int NULL,
	[PS_SO] int NULL,
	[PS_Runs] int NULL,
	[PS_RBI] int NULL,
	[PS_Update_Date] varchar(20) NULL,
	[PS_Update_User] varchar(20) NULL,
	[PS_Create_Date] varchar(20) NULL,
	[PS_Create_User] varchar(20) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OLD_Player](
	[P_Id] int NULL,
	[P_Name_Last] varchar(30) NULL,
	[P_Name_First] varchar(20) NULL,
	[P_Active] int NULL,
	[P_T_Id] int NULL,
	[P_Update_Date] varchar(20) NULL,
	[P_Update_User] varchar(20) NULL,
	[P_Create_Date] varchar(20) NULL,
	[P_Create_User] varchar(20) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OLD_PageContent](
	[PC_Id] int NULL,
	[PC_Page] varchar(30) NULL,
	[PC_Content] varchar(8000) NULL,
	[PC_Update_Date] varchar(20) NULL,
	[PC_Update_User] varchar(20) NULL,
	[PC_Create_Date] varchar(20) NULL,
	[PC_Create_User] varchar(20) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OLD_News](
	[N_Id] int NULL,
	[N_Date_Start] varchar(20) NULL,
	[N_Date_End] varchar(20) NULL,
	[N_Active] int NULL,
	[N_T_Id] int NULL,
	[N_Subject] varchar(255) NULL,
	[N_Content] varchar(max) NULL,
	[N_Update_Date] varchar(20) NULL,
	[N_Update_User] varchar(20) NULL,
	[N_Create_Date] varchar(20) NULL,
	[N_Create_User] varchar(20) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OLD_GameType](
	[GT_Id] int NULL,
	[GT_Description] varchar(30) NULL,
	[GT_Record] int NULL,
	[GT_Create_Date] varchar(20) NULL,
	[GT_Create_User] varchar(20) NULL,
	[GT_Update_Date] varchar(20) NULL,
	[GT_Update_User] varchar(20) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OLD_GameStatus](
	[GS_Id] int NULL,
	[GS_Description] varchar(30) NULL,
	[GS_Display_Scores] int NULL,
	[GS_Completed] int NULL,
	[GS_Create_Date] varchar(20) NULL,
	[GS_Create_User] varchar(20) NULL,
	[GS_Update_Date] varchar(20) NULL,
	[GS_Update_User] varchar(20) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OLD_Division](
	[D_Id] int NULL,
	[D_Name] varchar(20) NULL,
	[D_C_Id] int NULL,
	[D_In_League] int NULL,
	[D_Create_Date] varchar(20) NULL,
	[D_Create_User] varchar(20) NULL,
	[D_Update_Date] varchar(20) NULL,
	[D_Update_User] varchar(20) NULL,
	[D_Sort] int NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OLD_Contact](
	[CO_Id] int NULL,
	[CO_Name_Last] varchar(30) NULL,
	[CO_Name_First] varchar(20) NULL,
	[CO_Address_Street] varchar(50) NULL,
	[CO_Address_City] varchar(30) NULL,
	[CO_Address_State] varchar(2) NULL,
	[CO_Address_Zip] varchar(10) NULL,
	[CO_Phone] varchar(10) NULL,
	[CO_Email] varchar(50) NULL,
	[CO_Comments] varchar(4000) NULL,
	[CO_Update_Date] varchar(20) NULL,
	[CO_Update_User] varchar(20) NULL,
	[CO_Create_Date] varchar(20) NULL,
	[CO_Create_User] varchar(20) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OLD_Church](
	[CH_Id] int NULL,
	[CH_Name] varchar(100) NULL,
	[CH_Name_Short] varchar(100) NULL,
	[CH_Address_Street] varchar(50) NULL,
	[CH_Address_City] varchar(30) NULL,
	[CH_Address_State] varchar(2) NULL,
	[CH_Address_Zip] varchar(10) NULL,
	[CH_Phone] varchar(10) NULL,
	[CH_Info] varchar(4000) NULL,
	[CH_Update_Date] varchar(20) NULL,
	[CH_Update_User] varchar(20) NULL,
	[CH_Create_Date] varchar(20) NULL,
	[CH_Create_User] varchar(20) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OLD_Conference](
  [C_Id] int NULL,
  [C_Name] varchar(20) NULL,
  [C_In_League] int NULL,
  [C_Create_Date] varchar(20) NULL,
  [C_Create_User] varchar(20) NULL,
  [C_Update_Date] varchar(20) NULL,
  [C_Update_User] varchar(20) NULL,
  [C_Sort] int NULL
) ON [PRIMARY]
GO
