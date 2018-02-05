/* create by haipeng.tao */
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Users]
Go
CREATE TABLE dbo.Users(
    Id uniqueidentifier  NOT NULL
    ,Name nvarchar(30)  NOT NULL
    ,PassWord nvarchar(50)  NOT NULL
    ,LastLoginDate datetime  NOT NULL
    ,IsFreeze bit  NOT NULL
    ,[key] int  NOT NULL
    ,Nickname nvarchar(256)  NOT NULL
    ,Type int  NOT NULL
    ,EMail nvarchar(50)  NOT NULL
    ,Tel nvarchar(30)  NOT NULL
    ,UpperName nvarchar(30)  NOT NULL
    ,RegisterTime datetime  NOT NULL
    ,Remark nvarchar(50)  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.Users ADD CONSTRAINT
PK_Users PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Users', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Users', N'COLUMN', N'Name'
Go
DECLARE @v sql_variant 
SET @v = N'密码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Users', N'COLUMN', N'PassWord'
Go
DECLARE @v sql_variant 
SET @v = N'最后登录日期'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Users', N'COLUMN', N'LastLoginDate'
Go
DECLARE @v sql_variant 
SET @v = N'是否冻结'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Users', N'COLUMN', N'IsFreeze'
Go
DECLARE @v sql_variant 
SET @v = N'代码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Users', N'COLUMN', N'[key]'
Go
DECLARE @v sql_variant 
SET @v = N'昵称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Users', N'COLUMN', N'Nickname'
Go
DECLARE @v sql_variant 
SET @v = N'用户类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Users', N'COLUMN', N'Type'
Go
DECLARE @v sql_variant 
SET @v = N'邮箱'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Users', N'COLUMN', N'EMail'
Go
DECLARE @v sql_variant 
SET @v = N'电话号码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Users', N'COLUMN', N'Tel'
Go
DECLARE @v sql_variant 
SET @v = N'全名'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Users', N'COLUMN', N'UpperName'
Go
DECLARE @v sql_variant 
SET @v = N'注册时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Users', N'COLUMN', N'RegisterTime'
Go
DECLARE @v sql_variant 
SET @v = N'备注'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Users', N'COLUMN', N'Remark'
Go
DECLARE @v sql_variant 
SET @v = N'用户'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Users' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Modules]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Modules]
Go
CREATE TABLE dbo.Modules(
    Id uniqueidentifier  NOT NULL
    ,Code varchar(50)  NOT NULL
    ,Name nvarchar(20)  NOT NULL
    ,ControllerName varchar(200)  NOT NULL
    ,ActionName varchar(100)  NOT NULL
    ,IsMenu bit  NOT NULL
    ,Url varchar(400)  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.Modules ADD CONSTRAINT
PK_Modules PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Modules', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'代码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Modules', N'COLUMN', N'Code'
Go
DECLARE @v sql_variant 
SET @v = N'名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Modules', N'COLUMN', N'Name'
Go
DECLARE @v sql_variant 
SET @v = N'控制器类名'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Modules', N'COLUMN', N'ControllerName'
Go
DECLARE @v sql_variant 
SET @v = N'控制器类的ACTION'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Modules', N'COLUMN', N'ActionName'
Go
DECLARE @v sql_variant 
SET @v = N'是否菜单项'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Modules', N'COLUMN', N'IsMenu'
Go
DECLARE @v sql_variant 
SET @v = N'功能项地址'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Modules', N'COLUMN', N'Url'
Go
DECLARE @v sql_variant 
SET @v = N'功能项'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Modules' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ModuleRoles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ModuleRoles]
Go
CREATE TABLE dbo.ModuleRoles(
    Id uniqueidentifier  NOT NULL
    ,Code varchar(50)  NOT NULL
    ,Name nvarchar(20)  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.ModuleRoles ADD CONSTRAINT
PK_ModuleRoles PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ModuleRoles', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'代码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ModuleRoles', N'COLUMN', N'Code'
Go
DECLARE @v sql_variant 
SET @v = N'名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ModuleRoles', N'COLUMN', N'Name'
Go
DECLARE @v sql_variant 
SET @v = N'功能角色'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ModuleRoles' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ModuleRoleToModules]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ModuleRoleToModules]
Go
CREATE TABLE dbo.ModuleRoleToModules(
    Id uniqueidentifier  NOT NULL
    ,ModuleRoleId uniqueidentifier  NOT NULL
    ,ModuleId uniqueidentifier  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.ModuleRoleToModules ADD CONSTRAINT
PK_ModuleRoleToModules PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ModuleRoleToModules', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'功能角色ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ModuleRoleToModules', N'COLUMN', N'ModuleRoleId'
Go
DECLARE @v sql_variant 
SET @v = N'功能项ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ModuleRoleToModules', N'COLUMN', N'ModuleId'
Go
DECLARE @v sql_variant 
SET @v = N'功能角色与功能项对应关系'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ModuleRoleToModules' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UserModuleRoles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[UserModuleRoles]
Go
CREATE TABLE dbo.UserModuleRoles(
    Id uniqueidentifier  NOT NULL
    ,UserId uniqueidentifier  NOT NULL
    ,ModuleRoleId uniqueidentifier  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.UserModuleRoles ADD CONSTRAINT
PK_UserModuleRoles PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'UserModuleRoles', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'用户Id'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'UserModuleRoles', N'COLUMN', N'UserId'
Go
DECLARE @v sql_variant 
SET @v = N'功能角色ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'UserModuleRoles', N'COLUMN', N'ModuleRoleId'
Go
DECLARE @v sql_variant 
SET @v = N'用户与功能角色对应关系'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'UserModuleRoles' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UserFastModules]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[UserFastModules]
Go
CREATE TABLE dbo.UserFastModules(
    Id uniqueidentifier  NOT NULL
    ,UserId uniqueidentifier  NOT NULL
    ,ModuleId uniqueidentifier  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.UserFastModules ADD CONSTRAINT
PK_UserFastModules PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'UserFastModules', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'用户Id'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'UserFastModules', N'COLUMN', N'UserId'
Go
DECLARE @v sql_variant 
SET @v = N'功能项ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'UserFastModules', N'COLUMN', N'ModuleId'
Go
DECLARE @v sql_variant 
SET @v = N'用户快速功能权限'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'UserFastModules' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Regions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Regions]
Go
CREATE TABLE dbo.Regions(
    Id uniqueidentifier  NOT NULL
    ,Code varchar(10)  NOT NULL
    ,Name nvarchar(30)  NOT NULL
    ,ParentId uniqueidentifier  NOT NULL
    ,Level tinyint  NOT NULL
    ,CountryId uniqueidentifier  NOT NULL
    ,Sequence int  NOT NULL
    ,ErpCode varchar(10)  NOT NULL
    ,ShortName varchar(30)  NOT NULL
    ,PinYin varchar(200)  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.Regions ADD CONSTRAINT
PK_Regions PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Regions', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'代码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Regions', N'COLUMN', N'Code'
Go
DECLARE @v sql_variant 
SET @v = N'名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Regions', N'COLUMN', N'Name'
Go
DECLARE @v sql_variant 
SET @v = N'父级ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Regions', N'COLUMN', N'ParentId'
Go
DECLARE @v sql_variant 
SET @v = N'层级'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Regions', N'COLUMN', N'Level'
Go
DECLARE @v sql_variant 
SET @v = N'国家ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Regions', N'COLUMN', N'CountryId'
Go
DECLARE @v sql_variant 
SET @v = N'序列号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Regions', N'COLUMN', N'Sequence'
Go
DECLARE @v sql_variant 
SET @v = N'ERP代码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Regions', N'COLUMN', N'ErpCode'
Go
DECLARE @v sql_variant 
SET @v = N'缩写'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Regions', N'COLUMN', N'ShortName'
Go
DECLARE @v sql_variant 
SET @v = N'拼音'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Regions', N'COLUMN', N'PinYin'
Go
DECLARE @v sql_variant 
SET @v = N'地区'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Regions' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Menus]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Menus]
Go
CREATE TABLE dbo.Menus(
    Id uniqueidentifier  NOT NULL
    ,Name nvarchar(30)  NOT NULL
    ,Url nvarchar(50)  NOT NULL
    ,ParentMenuID uniqueidentifier  NOT NULL
    ,IsVisible bit  NOT NULL
    ,IsParent bit  NOT NULL
    ,IsFavorite bit  NOT NULL
    ,Sequence int  NOT NULL
    ,Icons nvarchar(100)  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.Menus ADD CONSTRAINT
PK_Menus PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Menus', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Menus', N'COLUMN', N'Name'
Go
DECLARE @v sql_variant 
SET @v = N'路径'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Menus', N'COLUMN', N'Url'
Go
DECLARE @v sql_variant 
SET @v = N'父级ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Menus', N'COLUMN', N'ParentMenuID'
Go
DECLARE @v sql_variant 
SET @v = N'是否可见'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Menus', N'COLUMN', N'IsVisible'
Go
DECLARE @v sql_variant 
SET @v = N'是否父节点'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Menus', N'COLUMN', N'IsParent'
Go
DECLARE @v sql_variant 
SET @v = N'是否快捷菜单'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Menus', N'COLUMN', N'IsFavorite'
Go
DECLARE @v sql_variant 
SET @v = N'排序号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Menus', N'COLUMN', N'Sequence'
Go
DECLARE @v sql_variant 
SET @v = N'图标'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Menus', N'COLUMN', N'Icons'
Go
DECLARE @v sql_variant 
SET @v = N'菜单'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Menus' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[LoginLogs]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[LoginLogs]
Go
CREATE TABLE dbo.LoginLogs(
    Id uniqueidentifier  NOT NULL
    ,UserName nvarchar(50)  NOT NULL
    ,LoginIp nvarchar(50)  NOT NULL
    ,CreateTime datetime  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.LoginLogs ADD CONSTRAINT
PK_LoginLogs PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LoginLogs', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'登录用户名'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LoginLogs', N'COLUMN', N'UserName'
Go
DECLARE @v sql_variant 
SET @v = N'登录主机IP地址'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LoginLogs', N'COLUMN', N'LoginIp'
Go
DECLARE @v sql_variant 
SET @v = N'登录时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LoginLogs', N'COLUMN', N'CreateTime'
Go
DECLARE @v sql_variant 
SET @v = N'登陆日志'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LoginLogs' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ExceptionLogs]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ExceptionLogs]
Go
CREATE TABLE dbo.ExceptionLogs(
    Id uniqueidentifier  NOT NULL
    ,Message nvarchar(999)  NOT NULL
    ,InnerException nvarchar(max)  NULL
    ,Ip nvarchar(50)  NOT NULL
    ,CreateTime datetime  NOT NULL
    ,UserName nvarchar(50)  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.ExceptionLogs ADD CONSTRAINT
PK_ExceptionLogs PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExceptionLogs', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'异常消息'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExceptionLogs', N'COLUMN', N'Message'
Go
DECLARE @v sql_variant 
SET @v = N'内部异常'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExceptionLogs', N'COLUMN', N'InnerException'
Go
DECLARE @v sql_variant 
SET @v = N'调用主机IP'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExceptionLogs', N'COLUMN', N'Ip'
Go
DECLARE @v sql_variant 
SET @v = N'调用时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExceptionLogs', N'COLUMN', N'CreateTime'
Go
DECLARE @v sql_variant 
SET @v = N'用户名'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExceptionLogs', N'COLUMN', N'UserName'
Go
DECLARE @v sql_variant 
SET @v = N'异常日志'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ExceptionLogs' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BaseEnums]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[BaseEnums]
Go
CREATE TABLE dbo.BaseEnums(
    Id uniqueidentifier  NOT NULL
    ,Note nvarchar(100)  NOT NULL
    ,Name nvarchar(50)  NOT NULL
    ,Value int  NOT NULL
    ,EnumType nvarchar(50)  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.BaseEnums ADD CONSTRAINT
PK_BaseEnums PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BaseEnums', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'描述'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BaseEnums', N'COLUMN', N'Note'
Go
DECLARE @v sql_variant 
SET @v = N'名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BaseEnums', N'COLUMN', N'Name'
Go
DECLARE @v sql_variant 
SET @v = N'值'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BaseEnums', N'COLUMN', N'Value'
Go
DECLARE @v sql_variant 
SET @v = N'枚举类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BaseEnums', N'COLUMN', N'EnumType'
Go
DECLARE @v sql_variant 
SET @v = N'基础枚举表'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BaseEnums' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ServiceCallLogs]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ServiceCallLogs]
Go
CREATE TABLE dbo.ServiceCallLogs(
    Id uniqueidentifier  NOT NULL
    ,ServiceName nvarchar(100)  NULL
    ,MethodName nvarchar(100)  NULL
    ,RequestTime datetime  NOT NULL
    ,IsSucessful bit  NULL
    ,UserName nvarchar(50)  NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.ServiceCallLogs ADD CONSTRAINT
PK_ServiceCallLogs PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ServiceCallLogs', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'服务名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ServiceCallLogs', N'COLUMN', N'ServiceName'
Go
DECLARE @v sql_variant 
SET @v = N'方法名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ServiceCallLogs', N'COLUMN', N'MethodName'
Go
DECLARE @v sql_variant 
SET @v = N'调用时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ServiceCallLogs', N'COLUMN', N'RequestTime'
Go
DECLARE @v sql_variant 
SET @v = N'是否成功'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ServiceCallLogs', N'COLUMN', N'IsSucessful'
Go
DECLARE @v sql_variant 
SET @v = N'调用用户名'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ServiceCallLogs', N'COLUMN', N'UserName'
Go
DECLARE @v sql_variant 
SET @v = N'服务调用日志'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'ServiceCallLogs' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[LdmDists]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[LdmDists]
Go
CREATE TABLE dbo.LdmDists(
    Id uniqueidentifier  NOT NULL
    ,DIST_NUM varchar(20)  NOT NULL
    ,RUT_ID varchar(30)  NULL
    ,RUT_NAME varchar(100)  NULL
    ,DIST_DATE char(8)  NOT NULL
    ,DLVMAN_ID varchar(30)  NOT NULL
    ,DLVMAN_NAME varchar(80)  NOT NULL
    ,DRIVER_ID varchar(30)  NOT NULL
    ,DRIVER_NAME varchar(80)  NOT NULL
    ,CAR_ID varchar(30)  NOT NULL
    ,CAR_ID varchar(30)  NOT NULL
    ,DIST_CUST_SUM int  NOT NULL
    ,QTY_BAR decimal(18,6)  NOT NULL
    ,AMT_SUM decimal(18,2)  NOT NULL
    ,STATUS varchar(2)  NOT NULL
    ,IS_DOWNLOAD char(1)  NOT NULL
    ,IS_MRB char(2)  NOT NULL
    ,DRIVER_CARD_ID varchar(30)  NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.LdmDists ADD CONSTRAINT
PK_LdmDists PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'配送单编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'DIST_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'线路编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'RUT_ID'
Go
DECLARE @v sql_variant 
SET @v = N'线路名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'RUT_NAME'
Go
DECLARE @v sql_variant 
SET @v = N'送货日期'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'DIST_DATE'
Go
DECLARE @v sql_variant 
SET @v = N'送货员编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'DLVMAN_ID'
Go
DECLARE @v sql_variant 
SET @v = N'送货员名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'DLVMAN_NAME'
Go
DECLARE @v sql_variant 
SET @v = N'驾驶员编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'DRIVER_ID'
Go
DECLARE @v sql_variant 
SET @v = N'驾驶员名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'DRIVER_NAME'
Go
DECLARE @v sql_variant 
SET @v = N'送货车编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'CAR_ID'
Go
DECLARE @v sql_variant 
SET @v = N'送货车编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'CAR_ID'
Go
DECLARE @v sql_variant 
SET @v = N'客户数'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'DIST_CUST_SUM'
Go
DECLARE @v sql_variant 
SET @v = N'送货量'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'QTY_BAR'
Go
DECLARE @v sql_variant 
SET @v = N'应收金额'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'AMT_SUM'
Go
DECLARE @v sql_variant 
SET @v = N'状态'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'STATUS'
Go
DECLARE @v sql_variant 
SET @v = N'下载状态'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'IS_DOWNLOAD'
Go
DECLARE @v sql_variant 
SET @v = N'是否使用'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'IS_MRB'
Go
DECLARE @v sql_variant 
SET @v = N'送货员卡编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists', N'COLUMN', N'DRIVER_CARD_ID'
Go
DECLARE @v sql_variant 
SET @v = N'配送任务单信息'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDists' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[LdmDistLines]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[LdmDistLines]
Go
CREATE TABLE dbo.LdmDistLines(
    Id uniqueidentifier  NOT NULL
    ,DIST_NUM varchar(20)  NOT NULL
    ,CO_NUM varchar(20)  NOT NULL
    ,CUST_ID varchar(100)  NOT NULL
    ,CUST_CODE varchar(30)  NOT NULL
    ,CUST_NAME varchar(100)  NOT NULL
    ,MANAGER varchar(16)  NOT NULL
    ,ADDR varchar(200)  NOT NULL
    ,TEL varchar(16)  NOT NULL
    ,QTY_BAR decimal(18,6)  NOT NULL
    ,AMT_AR decimal(18,2)  NOT NULL
    ,AMT_OR decimal(18,2)  NOT NULL
    ,PMT_STATUS char(1)  NOT NULL
    ,TYPE char(2)  NOT NULL
    ,SEQ int  NOT NULL
    ,LICENSE_CODE varchar(16)  NOT NULL
    ,PAY_TYPE char(2)  NULL
    ,LONGITUDE decimal(11,8)  NULL
    ,LATITUDE decimal(11,8)  NULL
    ,CUST_CARD_ID varchar(30)  NULL
    ,CUST_CARD_CODE varchar(30)  NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.LdmDistLines ADD CONSTRAINT
PK_LdmDistLines PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'配送单编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'DIST_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'订单编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'CO_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'客户内码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'CUST_ID'
Go
DECLARE @v sql_variant 
SET @v = N'客户编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'CUST_CODE'
Go
DECLARE @v sql_variant 
SET @v = N'客户名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'CUST_NAME'
Go
DECLARE @v sql_variant 
SET @v = N'负责人'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'MANAGER'
Go
DECLARE @v sql_variant 
SET @v = N'地址'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'ADDR'
Go
DECLARE @v sql_variant 
SET @v = N'电话'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'TEL'
Go
DECLARE @v sql_variant 
SET @v = N'数量'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'QTY_BAR'
Go
DECLARE @v sql_variant 
SET @v = N'应收金额'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'AMT_AR'
Go
DECLARE @v sql_variant 
SET @v = N'已收金额'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'AMT_OR'
Go
DECLARE @v sql_variant 
SET @v = N'付款状态'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'PMT_STATUS'
Go
DECLARE @v sql_variant 
SET @v = N'订单类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'TYPE'
Go
DECLARE @v sql_variant 
SET @v = N'送货顺序'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'SEQ'
Go
DECLARE @v sql_variant 
SET @v = N'许可证专卖号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'LICENSE_CODE'
Go
DECLARE @v sql_variant 
SET @v = N'结算方式'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'PAY_TYPE'
Go
DECLARE @v sql_variant 
SET @v = N'位置经度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'LONGITUDE'
Go
DECLARE @v sql_variant 
SET @v = N'位置纬度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'LATITUDE'
Go
DECLARE @v sql_variant 
SET @v = N'零售户物理卡全球唯一码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'CUST_CARD_ID'
Go
DECLARE @v sql_variant 
SET @v = N'零售户物理卡编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines', N'COLUMN', N'CUST_CARD_CODE'
Go
DECLARE @v sql_variant 
SET @v = N'配送任务单行订单信息'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistLines' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[LdmDisItems]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[LdmDisItems]
Go
CREATE TABLE dbo.LdmDisItems(
    Id uniqueidentifier  NOT NULL
    ,CO_NUM varchar(20)  NOT NULL
    ,ITEM_ID varchar(100)  NOT NULL
    ,ITEM_NAME varchar(30)  NOT NULL
    ,PRICE decimal(18,6)  NOT NULL
    ,QTY decimal(18,6)  NOT NULL
    ,AMT decimal(18,6)  NOT NULL
    ,IS_ABNORMAL char(1)  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.LdmDisItems ADD CONSTRAINT
PK_LdmDisItems PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDisItems', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'订单编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDisItems', N'COLUMN', N'CO_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'商品编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDisItems', N'COLUMN', N'ITEM_ID'
Go
DECLARE @v sql_variant 
SET @v = N'商品名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDisItems', N'COLUMN', N'ITEM_NAME'
Go
DECLARE @v sql_variant 
SET @v = N'单价'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDisItems', N'COLUMN', N'PRICE'
Go
DECLARE @v sql_variant 
SET @v = N'数量'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDisItems', N'COLUMN', N'QTY'
Go
DECLARE @v sql_variant 
SET @v = N'金额'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDisItems', N'COLUMN', N'AMT'
Go
DECLARE @v sql_variant 
SET @v = N'是否异性烟'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDisItems', N'COLUMN', N'IS_ABNORMAL'
Go
DECLARE @v sql_variant 
SET @v = N'配送任务单行订单商品信息'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDisItems' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[LdmDistCars]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[LdmDistCars]
Go
CREATE TABLE dbo.LdmDistCars(
    Id uniqueidentifier  NOT NULL
    ,CAR_ID varchar(30)  NOT NULL
    ,CAR_NAME varchar(50)  NOT NULL
    ,CAR_LICENSE varchar(15)  NOT NULL
    ,DLVMAN_ID varchar(30)  NOT NULL
    ,DLVMAN_NAME varchar(80)  NOT NULL
    ,DRIVER_ID varchar(30)  NOT NULL
    ,DRIVER_NAME varchar(80)  NOT NULL
    ,IS_MRB char(1)  NOT NULL
    ,COM_ID varchar(30)  NOT NULL
    ,PSW nvarchar(200)  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.LdmDistCars ADD CONSTRAINT
PK_LdmDistCars PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistCars', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'车辆编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistCars', N'COLUMN', N'CAR_ID'
Go
DECLARE @v sql_variant 
SET @v = N'车辆名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistCars', N'COLUMN', N'CAR_NAME'
Go
DECLARE @v sql_variant 
SET @v = N'车牌号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistCars', N'COLUMN', N'CAR_LICENSE'
Go
DECLARE @v sql_variant 
SET @v = N'送货员编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistCars', N'COLUMN', N'DLVMAN_ID'
Go
DECLARE @v sql_variant 
SET @v = N'送货员名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistCars', N'COLUMN', N'DLVMAN_NAME'
Go
DECLARE @v sql_variant 
SET @v = N'驾驶员编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistCars', N'COLUMN', N'DRIVER_ID'
Go
DECLARE @v sql_variant 
SET @v = N'驾驶员名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistCars', N'COLUMN', N'DRIVER_NAME'
Go
DECLARE @v sql_variant 
SET @v = N'是否有效'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistCars', N'COLUMN', N'IS_MRB'
Go
DECLARE @v sql_variant 
SET @v = N'公司号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistCars', N'COLUMN', N'COM_ID'
Go
DECLARE @v sql_variant 
SET @v = N'登录密码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistCars', N'COLUMN', N'PSW'
Go
DECLARE @v sql_variant 
SET @v = N'配送车辆信息'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LdmDistCars' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DistDlvmans]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DistDlvmans]
Go
CREATE TABLE dbo.DistDlvmans(
    Id uniqueidentifier  NOT NULL
    ,USER_ID varchar(30)  NOT NULL
    ,USER_NAME varchar(30)  NOT NULL
    ,ORGAN_ID varchar(30)  NOT NULL
    ,POSITION_CODE varchar(30)  NOT NULL
    ,COM_ID varchar(30)  NOT NULL
    ,PSW nvarchar(200)  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.DistDlvmans ADD CONSTRAINT
PK_DistDlvmans PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistDlvmans', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'送货员帐号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistDlvmans', N'COLUMN', N'USER_ID'
Go
DECLARE @v sql_variant 
SET @v = N'送货员名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistDlvmans', N'COLUMN', N'USER_NAME'
Go
DECLARE @v sql_variant 
SET @v = N'送货员组织编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistDlvmans', N'COLUMN', N'ORGAN_ID'
Go
DECLARE @v sql_variant 
SET @v = N'送货员岗位编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistDlvmans', N'COLUMN', N'POSITION_CODE'
Go
DECLARE @v sql_variant 
SET @v = N'公司号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistDlvmans', N'COLUMN', N'COM_ID'
Go
DECLARE @v sql_variant 
SET @v = N'登录密码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistDlvmans', N'COLUMN', N'PSW'
Go
DECLARE @v sql_variant 
SET @v = N'配送员信息'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistDlvmans' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DistRecordLogs]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DistRecordLogs]
Go
CREATE TABLE dbo.DistRecordLogs(
    Id uniqueidentifier  NOT NULL
    ,LOG_SEQ varchar(30)  NOT NULL
    ,REF_TYPE char(1)  NOT NULL
    ,REF_ID varchar(200)  NOT NULL
    ,OPERATION_TYPE varchar(20)  NOT NULL
    ,LOG_DATE char(8)  NOT NULL
    ,LOG_TIME char(8)  NOT NULL
    ,USER_ID varchar(30)  NOT NULL
    ,LONGITUDE decimal(11,8)  NOT NULL
    ,LATITUDE decimal(11,8)  NOT NULL
    ,NOTE varchar(200)  NOT NULL
    ,OPERATE_MODE char(1)  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.DistRecordLogs ADD CONSTRAINT
PK_DistRecordLogs PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRecordLogs', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'流水号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRecordLogs', N'COLUMN', N'LOG_SEQ'
Go
DECLARE @v sql_variant 
SET @v = N'单据类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRecordLogs', N'COLUMN', N'REF_TYPE'
Go
DECLARE @v sql_variant 
SET @v = N'配送任务单编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRecordLogs', N'COLUMN', N'REF_ID'
Go
DECLARE @v sql_variant 
SET @v = N'操作类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRecordLogs', N'COLUMN', N'OPERATION_TYPE'
Go
DECLARE @v sql_variant 
SET @v = N'操作日期'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRecordLogs', N'COLUMN', N'LOG_DATE'
Go
DECLARE @v sql_variant 
SET @v = N'操作时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRecordLogs', N'COLUMN', N'LOG_TIME'
Go
DECLARE @v sql_variant 
SET @v = N'用户编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRecordLogs', N'COLUMN', N'USER_ID'
Go
DECLARE @v sql_variant 
SET @v = N'操作经度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRecordLogs', N'COLUMN', N'LONGITUDE'
Go
DECLARE @v sql_variant 
SET @v = N'操作纬度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRecordLogs', N'COLUMN', N'LATITUDE'
Go
DECLARE @v sql_variant 
SET @v = N'备注'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRecordLogs', N'COLUMN', N'NOTE'
Go
DECLARE @v sql_variant 
SET @v = N'操作方式'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRecordLogs', N'COLUMN', N'OPERATE_MODE'
Go
DECLARE @v sql_variant 
SET @v = N'配送操作日志表'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRecordLogs' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DistCarChecks]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DistCarChecks]
Go
CREATE TABLE dbo.DistCarChecks(
    Id uniqueidentifier  NOT NULL
    ,CHECK_ID varchar(30)  NOT NULL
    ,CAR_ID varchar(30)  NOT NULL
    ,REF_TYPE char(1)  NOT NULL
    ,REF_ID varchar(200)  NOT NULL
    ,ABNORMAL_DETAIL char(3)  NOT NULL
    ,ABNORMAL_TYPE varchar(30)  NOT NULL
    ,CHECK_TIME char(14)  NOT NULL
    ,LONGITUDE decimal(11,8)  NOT NULL
    ,LATITUDE decimal(11,8)  NOT NULL
    ,CHECK_TYPE char(1)  NOT NULL
    ,OPERATE_MODE char(1)  NOT NULL
    ,NOTE varchar(100)  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.DistCarChecks ADD CONSTRAINT
PK_DistCarChecks PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarChecks', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'车辆检查流水号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarChecks', N'COLUMN', N'CHECK_ID'
Go
DECLARE @v sql_variant 
SET @v = N'送货车编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarChecks', N'COLUMN', N'CAR_ID'
Go
DECLARE @v sql_variant 
SET @v = N'单据类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarChecks', N'COLUMN', N'REF_TYPE'
Go
DECLARE @v sql_variant 
SET @v = N'单据编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarChecks', N'COLUMN', N'REF_ID'
Go
DECLARE @v sql_variant 
SET @v = N'三检异常明细'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarChecks', N'COLUMN', N'ABNORMAL_DETAIL'
Go
DECLARE @v sql_variant 
SET @v = N'三检异常类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarChecks', N'COLUMN', N'ABNORMAL_TYPE'
Go
DECLARE @v sql_variant 
SET @v = N'检查时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarChecks', N'COLUMN', N'CHECK_TIME'
Go
DECLARE @v sql_variant 
SET @v = N'经度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarChecks', N'COLUMN', N'LONGITUDE'
Go
DECLARE @v sql_variant 
SET @v = N'纬度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarChecks', N'COLUMN', N'LATITUDE'
Go
DECLARE @v sql_variant 
SET @v = N'检查类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarChecks', N'COLUMN', N'CHECK_TYPE'
Go
DECLARE @v sql_variant 
SET @v = N'操作方式'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarChecks', N'COLUMN', N'OPERATE_MODE'
Go
DECLARE @v sql_variant 
SET @v = N'备注'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarChecks', N'COLUMN', N'NOTE'
Go
DECLARE @v sql_variant 
SET @v = N'车辆检查信息'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarChecks' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DistCusts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DistCusts]
Go
CREATE TABLE dbo.DistCusts(
    Id uniqueidentifier  NOT NULL
    ,DIST_NUM varchar(20)  NOT NULL
    ,CO_NUM varchar(20)  NOT NULL
    ,CUST_ID varchar(100)  NOT NULL
    ,IS_RECEIVED char(2)  NOT NULL
    ,DIST_SATIS char(2)  NOT NULL
    ,UNLOAD_REASON char(2)  NOT NULL
    ,REC_DATE char(8)  NOT NULL
    ,REC_ARRIVE_TIME char(14)  NOT NULL
    ,REC_LEAVE_TIME char(14)  NOT NULL
    ,HANDOVER_TIME decimal(18,6)  NOT NULL
    ,NOTSATIS_REASON char(6)  NULL
    ,UNUSUAL_TYPE char(2)  NULL
    ,EVALUATE_INFO varchar(100)  NULL
    ,SIGN_TYPE char(1)  NULL
    ,EXP_SIGN_REASON char(3)  NULL
    ,UNLOAD_LON decimal(11,8)  NULL
    ,UNLOAD_LAT decimal(11,8)  NULL
    ,UNLOAD_DISTANCE decimal(18,2)  NULL
    ,EVALUATE_TIME char(14)  NULL
    ,DLVMAN_ID varchar(30)  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.DistCusts ADD CONSTRAINT
PK_DistCusts PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'配送单编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'DIST_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'订单编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'CO_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'客户内码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'CUST_ID'
Go
DECLARE @v sql_variant 
SET @v = N'收货方式'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'IS_RECEIVED'
Go
DECLARE @v sql_variant 
SET @v = N'送货满意度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'DIST_SATIS'
Go
DECLARE @v sql_variant 
SET @v = N'收货状态'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'UNLOAD_REASON'
Go
DECLARE @v sql_variant 
SET @v = N'实际送达日期'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'REC_DATE'
Go
DECLARE @v sql_variant 
SET @v = N'实际到达时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'REC_ARRIVE_TIME'
Go
DECLARE @v sql_variant 
SET @v = N'实际离开时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'REC_LEAVE_TIME'
Go
DECLARE @v sql_variant 
SET @v = N'交接时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'HANDOVER_TIME'
Go
DECLARE @v sql_variant 
SET @v = N'不满意原因'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'NOTSATIS_REASON'
Go
DECLARE @v sql_variant 
SET @v = N'异常处理方式'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'UNUSUAL_TYPE'
Go
DECLARE @v sql_variant 
SET @v = N'送货评价'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'EVALUATE_INFO'
Go
DECLARE @v sql_variant 
SET @v = N'签到方式'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'SIGN_TYPE'
Go
DECLARE @v sql_variant 
SET @v = N'异常签到原因'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'EXP_SIGN_REASON'
Go
DECLARE @v sql_variant 
SET @v = N'卸货经度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'UNLOAD_LON'
Go
DECLARE @v sql_variant 
SET @v = N'卸货纬度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'UNLOAD_LAT'
Go
DECLARE @v sql_variant 
SET @v = N'卸货距离'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'UNLOAD_DISTANCE'
Go
DECLARE @v sql_variant 
SET @v = N'评价时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'EVALUATE_TIME'
Go
DECLARE @v sql_variant 
SET @v = N'送货员编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts', N'COLUMN', N'DLVMAN_ID'
Go
DECLARE @v sql_variant 
SET @v = N'到货确认信息'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCusts' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DistCarRuns]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DistCarRuns]
Go
CREATE TABLE dbo.DistCarRuns(
    Id uniqueidentifier  NOT NULL
    ,INFO_NUM varchar(20)  NOT NULL
    ,REF_TYPE char(1)  NOT NULL
    ,REF_ID varchar(200)  NOT NULL
    ,CAR_ID varchar(30)  NOT NULL
    ,DLVMAN_ID varchar(30)  NOT NULL
    ,CRT_DATE char(14)  NULL
    ,AMT_SUM decimal(18,6)  NULL
    ,THIS_MIL decimal(18,2)  NULL
    ,NOTE varchar(200)  NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.DistCarRuns ADD CONSTRAINT
PK_DistCarRuns PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRuns', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'流水号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRuns', N'COLUMN', N'INFO_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'单据类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRuns', N'COLUMN', N'REF_TYPE'
Go
DECLARE @v sql_variant 
SET @v = N'配送单编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRuns', N'COLUMN', N'REF_ID'
Go
DECLARE @v sql_variant 
SET @v = N'送货车编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRuns', N'COLUMN', N'CAR_ID'
Go
DECLARE @v sql_variant 
SET @v = N'送货员编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRuns', N'COLUMN', N'DLVMAN_ID'
Go
DECLARE @v sql_variant 
SET @v = N'录入时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRuns', N'COLUMN', N'CRT_DATE'
Go
DECLARE @v sql_variant 
SET @v = N'费用总和'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRuns', N'COLUMN', N'AMT_SUM'
Go
DECLARE @v sql_variant 
SET @v = N'本期里程数'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRuns', N'COLUMN', N'THIS_MIL'
Go
DECLARE @v sql_variant 
SET @v = N'备注'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRuns', N'COLUMN', N'NOTE'
Go
DECLARE @v sql_variant 
SET @v = N'车辆运行信息头表'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRuns' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DistCarRunLines]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DistCarRunLines]
Go
CREATE TABLE dbo.DistCarRunLines(
    Id uniqueidentifier  NOT NULL
    ,INFO_NUM varchar(20)  NOT NULL
    ,LINE_ID int  NOT NULL
    ,COST_TYPE char(1)  NOT NULL
    ,FUEL_TYPE char(1)  NULL
    ,LITRE_SUM decimal(18,6)  NULL
    ,FUEL_PRI decimal(18,6)  NULL
    ,AMT decimal(18,6)  NOT NULL
    ,INV_NUM varchar(50)  NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.DistCarRunLines ADD CONSTRAINT
PK_DistCarRunLines PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRunLines', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'流水号(头表流水号)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRunLines', N'COLUMN', N'INFO_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'行号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRunLines', N'COLUMN', N'LINE_ID'
Go
DECLARE @v sql_variant 
SET @v = N'费用类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRunLines', N'COLUMN', N'COST_TYPE'
Go
DECLARE @v sql_variant 
SET @v = N'燃油类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRunLines', N'COLUMN', N'FUEL_TYPE'
Go
DECLARE @v sql_variant 
SET @v = N'加油总量'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRunLines', N'COLUMN', N'LITRE_SUM'
Go
DECLARE @v sql_variant 
SET @v = N'燃油单价'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRunLines', N'COLUMN', N'FUEL_PRI'
Go
DECLARE @v sql_variant 
SET @v = N'费用'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRunLines', N'COLUMN', N'AMT'
Go
DECLARE @v sql_variant 
SET @v = N'相关票据'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRunLines', N'COLUMN', N'INV_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'车辆运行信息行表'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistCarRunLines' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DistFileLines]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DistFileLines]
Go
CREATE TABLE dbo.DistFileLines(
    Id uniqueidentifier  NOT NULL
    ,DOC_ID varchar(20)  NOT NULL
    ,DIST_NUM varchar(20)  NOT NULL
    ,CO_NUM varchar(20)  NOT NULL
    ,CUST_ID varchar(30)  NOT NULL
    ,FILE_TYPE varchar(80)  NOT NULL
    ,CRT_TIME char(14)  NULL
    ,CRT_MAN_ID varchar(30)  NULL
    ,NOTE varchar(200)  NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.DistFileLines ADD CONSTRAINT
PK_DistFileLines PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistFileLines', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'附件编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistFileLines', N'COLUMN', N'DOC_ID'
Go
DECLARE @v sql_variant 
SET @v = N'配送单编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistFileLines', N'COLUMN', N'DIST_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'订单编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistFileLines', N'COLUMN', N'CO_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'零售户编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistFileLines', N'COLUMN', N'CUST_ID'
Go
DECLARE @v sql_variant 
SET @v = N'附件类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistFileLines', N'COLUMN', N'FILE_TYPE'
Go
DECLARE @v sql_variant 
SET @v = N'创建时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistFileLines', N'COLUMN', N'CRT_TIME'
Go
DECLARE @v sql_variant 
SET @v = N'创建人'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistFileLines', N'COLUMN', N'CRT_MAN_ID'
Go
DECLARE @v sql_variant 
SET @v = N'备注'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistFileLines', N'COLUMN', N'NOTE'
Go
DECLARE @v sql_variant 
SET @v = N'照片附件信息'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistFileLines' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CoReturns]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[CoReturns]
Go
CREATE TABLE dbo.CoReturns(
    Id uniqueidentifier  NOT NULL
    ,RETURN_CO_NUM varchar(30)  NOT NULL
    ,CUST_ID varchar(30)  NOT NULL
    ,TYPE char(2)  NOT NULL
    ,STATUS char(2)  NOT NULL
    ,CRT_DATE char(8)  NULL
    ,CRT_USER_NAME varchar(15)  NULL
    ,ORG_CO_NUM varchar(30)  NULL
    ,NOTE varchar(200)  NULL
    ,AMT_SUM decimal(18,2)  NOT NULL
    ,QTY_SUM decimal(18,6)  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.CoReturns ADD CONSTRAINT
PK_CoReturns PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturns', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'退货订单编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturns', N'COLUMN', N'RETURN_CO_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'客户编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturns', N'COLUMN', N'CUST_ID'
Go
DECLARE @v sql_variant 
SET @v = N'类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturns', N'COLUMN', N'TYPE'
Go
DECLARE @v sql_variant 
SET @v = N'状态'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturns', N'COLUMN', N'STATUS'
Go
DECLARE @v sql_variant 
SET @v = N'创建日期'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturns', N'COLUMN', N'CRT_DATE'
Go
DECLARE @v sql_variant 
SET @v = N'创建人'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturns', N'COLUMN', N'CRT_USER_NAME'
Go
DECLARE @v sql_variant 
SET @v = N'原始订单编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturns', N'COLUMN', N'ORG_CO_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'注释'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturns', N'COLUMN', N'NOTE'
Go
DECLARE @v sql_variant 
SET @v = N'金额'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturns', N'COLUMN', N'AMT_SUM'
Go
DECLARE @v sql_variant 
SET @v = N'数量'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturns', N'COLUMN', N'QTY_SUM'
Go
DECLARE @v sql_variant 
SET @v = N'退货单抬头'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturns' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CoReturnLines]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[CoReturnLines]
Go
CREATE TABLE dbo.CoReturnLines(
    Id uniqueidentifier  NOT NULL
    ,RETURN_CO_NUM varchar(30)  NOT NULL
    ,LINE_NUM int  NOT NULL
    ,ITEM_ID varchar(30)  NOT NULL
    ,QTY_ORD decimal(18,6)  NOT NULL
    ,NOTE varchar(200)  NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.CoReturnLines ADD CONSTRAINT
PK_CoReturnLines PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturnLines', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'退货订单编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturnLines', N'COLUMN', N'RETURN_CO_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'行号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturnLines', N'COLUMN', N'LINE_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'商品编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturnLines', N'COLUMN', N'ITEM_ID'
Go
DECLARE @v sql_variant 
SET @v = N'退货数量'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturnLines', N'COLUMN', N'QTY_ORD'
Go
DECLARE @v sql_variant 
SET @v = N'备注'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturnLines', N'COLUMN', N'NOTE'
Go
DECLARE @v sql_variant 
SET @v = N'退货单明细'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoReturnLines' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GisCustPois]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[GisCustPois]
Go
CREATE TABLE dbo.GisCustPois(
    Id uniqueidentifier  NOT NULL
    ,CUST_ID varchar(30)  NOT NULL
    ,MOBILE_TYPE varchar(50)  NOT NULL
    ,ORIGINAL_LONGITUDE decimal(11,8)  NULL
    ,ORIGINAL_LATITUDE decimal(11,8)  NULL
    ,IS_DEFAULT char(1)  NOT NULL
    ,CRT_TIME char(14)  NOT NULL
    ,CRT_USER_ID varchar(30)  NOT NULL
    ,COL_TIME char(14)  NOT NULL
    ,IMP_STATUS char(2)  NOT NULL
    ,NOTE varchar(200)  NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.GisCustPois ADD CONSTRAINT
PK_GisCustPois PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisCustPois', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'零售户编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisCustPois', N'COLUMN', N'CUST_ID'
Go
DECLARE @v sql_variant 
SET @v = N'类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisCustPois', N'COLUMN', N'MOBILE_TYPE'
Go
DECLARE @v sql_variant 
SET @v = N'原始经度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisCustPois', N'COLUMN', N'ORIGINAL_LONGITUDE'
Go
DECLARE @v sql_variant 
SET @v = N'原始纬度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisCustPois', N'COLUMN', N'ORIGINAL_LATITUDE'
Go
DECLARE @v sql_variant 
SET @v = N'是否默认'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisCustPois', N'COLUMN', N'IS_DEFAULT'
Go
DECLARE @v sql_variant 
SET @v = N'插入时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisCustPois', N'COLUMN', N'CRT_TIME'
Go
DECLARE @v sql_variant 
SET @v = N'操作人'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisCustPois', N'COLUMN', N'CRT_USER_ID'
Go
DECLARE @v sql_variant 
SET @v = N'采集时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisCustPois', N'COLUMN', N'COL_TIME'
Go
DECLARE @v sql_variant 
SET @v = N'状态'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisCustPois', N'COLUMN', N'IMP_STATUS'
Go
DECLARE @v sql_variant 
SET @v = N'备注'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisCustPois', N'COLUMN', N'NOTE'
Go
DECLARE @v sql_variant 
SET @v = N'零售户位置信息'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisCustPois' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CoTempReturns]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[CoTempReturns]
Go
CREATE TABLE dbo.CoTempReturns(
    Id uniqueidentifier  NOT NULL
    ,DIST_NUM varchar(20)  NOT NULL
    ,CO_NUM varchar(20)  NOT NULL
    ,STATUS char(2)  NOT NULL
    ,OUT_DIST_NUM varchar(30)  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.CoTempReturns ADD CONSTRAINT
PK_CoTempReturns PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoTempReturns', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'配送单编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoTempReturns', N'COLUMN', N'DIST_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'订单编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoTempReturns', N'COLUMN', N'CO_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'状态'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoTempReturns', N'COLUMN', N'STATUS'
Go
DECLARE @v sql_variant 
SET @v = N'再次出库配送单编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoTempReturns', N'COLUMN', N'OUT_DIST_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'暂存订单表'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CoTempReturns' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GisLastLocrecords]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[GisLastLocrecords]
Go
CREATE TABLE dbo.GisLastLocrecords(
    Id uniqueidentifier  NOT NULL
    ,ID decimal(24,0)  NOT NULL
    ,M_CODE varchar(50)  NOT NULL
    ,M_TYPE varchar(10)  NOT NULL
    ,ORIGINAL_LONGITUDE decimal(11,8)  NOT NULL
    ,ORIGINAL_LATITUDE decimal(11,8)  NOT NULL
    ,SPEED decimal(10,2)  NOT NULL
    ,DIRECTION decimal(10,2)  NOT NULL
    ,HEIGHT decimal(10,2)  NOT NULL
    ,STATLLITE_NUM int  NOT NULL
    ,GPSTIME char(14)  NOT NULL
    ,INPUTDATE char(14)  NOT NULL
    ,STATE char(1)  NOT NULL
    ,CreateTime datetime  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.GisLastLocrecords ADD CONSTRAINT
PK_GisLastLocrecords PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisLastLocrecords', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'位置内码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisLastLocrecords', N'COLUMN', N'ID'
Go
DECLARE @v sql_variant 
SET @v = N'移动编码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisLastLocrecords', N'COLUMN', N'M_CODE'
Go
DECLARE @v sql_variant 
SET @v = N'移动类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisLastLocrecords', N'COLUMN', N'M_TYPE'
Go
DECLARE @v sql_variant 
SET @v = N'原始经度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisLastLocrecords', N'COLUMN', N'ORIGINAL_LONGITUDE'
Go
DECLARE @v sql_variant 
SET @v = N'原始纬度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisLastLocrecords', N'COLUMN', N'ORIGINAL_LATITUDE'
Go
DECLARE @v sql_variant 
SET @v = N'速度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisLastLocrecords', N'COLUMN', N'SPEED'
Go
DECLARE @v sql_variant 
SET @v = N'方向'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisLastLocrecords', N'COLUMN', N'DIRECTION'
Go
DECLARE @v sql_variant 
SET @v = N'高度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisLastLocrecords', N'COLUMN', N'HEIGHT'
Go
DECLARE @v sql_variant 
SET @v = N'卫星数'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisLastLocrecords', N'COLUMN', N'STATLLITE_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'卫星时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisLastLocrecords', N'COLUMN', N'GPSTIME'
Go
DECLARE @v sql_variant 
SET @v = N'写入时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisLastLocrecords', N'COLUMN', N'INPUTDATE'
Go
DECLARE @v sql_variant 
SET @v = N'定位状态'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisLastLocrecords', N'COLUMN', N'STATE'
Go
DECLARE @v sql_variant 
SET @v = N'创建时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisLastLocrecords', N'COLUMN', N'CreateTime'
Go
DECLARE @v sql_variant 
SET @v = N'零售户位置信息'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'GisLastLocrecords' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Retailers]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Retailers]
Go
CREATE TABLE dbo.Retailers(
    Id uniqueidentifier  NOT NULL
    ,CUST_ID varchar(30)  NOT NULL
    ,CUST_NAME varchar(100)  NOT NULL
    ,LICENSE_CODE varchar(16)  NOT NULL
    ,STATUS char(2)  NOT NULL
    ,COM_ID varchar(30)  NOT NULL
    ,PSW varchar(200)  NOT NULL
    ,LONGITUDE decimal(11,8)  NULL
    ,LATITUDE decimal(11,8)  NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.Retailers ADD CONSTRAINT
PK_Retailers PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Retailers', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'客户内码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Retailers', N'COLUMN', N'CUST_ID'
Go
DECLARE @v sql_variant 
SET @v = N'客户名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Retailers', N'COLUMN', N'CUST_NAME'
Go
DECLARE @v sql_variant 
SET @v = N'专卖证号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Retailers', N'COLUMN', N'LICENSE_CODE'
Go
DECLARE @v sql_variant 
SET @v = N'客户状态'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Retailers', N'COLUMN', N'STATUS'
Go
DECLARE @v sql_variant 
SET @v = N'公司号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Retailers', N'COLUMN', N'COM_ID'
Go
DECLARE @v sql_variant 
SET @v = N'收货密码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Retailers', N'COLUMN', N'PSW'
Go
DECLARE @v sql_variant 
SET @v = N'原始经度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Retailers', N'COLUMN', N'LONGITUDE'
Go
DECLARE @v sql_variant 
SET @v = N'原始纬度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Retailers', N'COLUMN', N'LATITUDE'
Go
DECLARE @v sql_variant 
SET @v = N'零售户基础信息'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Retailers' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeliveryWarnings]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DeliveryWarnings]
Go
CREATE TABLE dbo.DeliveryWarnings(
    Id uniqueidentifier  NOT NULL
    ,DIST_NUM varchar(20)  NOT NULL
    ,CO_NUM varchar(20)  NOT NULL
    ,Longitude decimal(11,8)  NULL
    ,Latitude decimal(11,8)  NULL
    ,RealLongitude decimal(11,8)  NULL
    ,RealLatitude decimal(11,8)  NULL
    ,DeliveryTime datetime  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.DeliveryWarnings ADD CONSTRAINT
PK_DeliveryWarnings PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DeliveryWarnings', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'配送单编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DeliveryWarnings', N'COLUMN', N'DIST_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'订单编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DeliveryWarnings', N'COLUMN', N'CO_NUM'
Go
DECLARE @v sql_variant 
SET @v = N'原始经度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DeliveryWarnings', N'COLUMN', N'Longitude'
Go
DECLARE @v sql_variant 
SET @v = N'原始纬度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DeliveryWarnings', N'COLUMN', N'Latitude'
Go
DECLARE @v sql_variant 
SET @v = N'实际纬度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DeliveryWarnings', N'COLUMN', N'RealLongitude'
Go
DECLARE @v sql_variant 
SET @v = N'实际经度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DeliveryWarnings', N'COLUMN', N'RealLatitude'
Go
DECLARE @v sql_variant 
SET @v = N'签收时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DeliveryWarnings', N'COLUMN', N'DeliveryTime'
Go
DECLARE @v sql_variant 
SET @v = N'配送点预警信息'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DeliveryWarnings' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AppVersions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[AppVersions]
Go
CREATE TABLE dbo.AppVersions(
    Id uniqueidentifier  NOT NULL
    ,ApkPacket nvarchar(100)  NOT NULL
    ,ApkName nvarchar(50)  NOT NULL
    ,VersionCode int  NOT NULL
    ,VersionName nvarchar(50)  NULL
    ,Url nvarchar(255)  NOT NULL
    ,IsValid bit  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.AppVersions ADD CONSTRAINT
PK_AppVersions PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'AppVersions', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'APK包名'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'AppVersions', N'COLUMN', N'ApkPacket'
Go
DECLARE @v sql_variant 
SET @v = N'APK名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'AppVersions', N'COLUMN', N'ApkName'
Go
DECLARE @v sql_variant 
SET @v = N'版本号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'AppVersions', N'COLUMN', N'VersionCode'
Go
DECLARE @v sql_variant 
SET @v = N'版本描述'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'AppVersions', N'COLUMN', N'VersionName'
Go
DECLARE @v sql_variant 
SET @v = N'APK地址'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'AppVersions', N'COLUMN', N'Url'
Go
DECLARE @v sql_variant 
SET @v = N'是否有效'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'AppVersions', N'COLUMN', N'IsValid'
Go
DECLARE @v sql_variant 
SET @v = N'APP版本信息'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'AppVersions' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DistRuts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DistRuts]
Go
CREATE TABLE dbo.DistRuts(
    Id uniqueidentifier  NOT NULL
    ,RUT_ID varchar(40)  NOT NULL
    ,RUT_NAME varchar(100)  NOT NULL
    ,IS_MRB char(1)  NOT NULL
    ,COM_ID varchar(30)  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.DistRuts ADD CONSTRAINT
PK_DistRuts PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRuts', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'APK包名'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRuts', N'COLUMN', N'RUT_ID'
Go
DECLARE @v sql_variant 
SET @v = N'APK名称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRuts', N'COLUMN', N'RUT_NAME'
Go
DECLARE @v sql_variant 
SET @v = N'是否有效'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRuts', N'COLUMN', N'IS_MRB'
Go
DECLARE @v sql_variant 
SET @v = N'公司代码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRuts', N'COLUMN', N'COM_ID'
Go
DECLARE @v sql_variant 
SET @v = N'配送线路信息'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DistRuts' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EntranceCards ]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[EntranceCards ]
Go
CREATE TABLE dbo.EntranceCards (
    Id uniqueidentifier  NOT NULL
    ,CardNum nvarchar(100)  NULL
    ,CardDec nvarchar(100)  NULL
    ,GlobalCode nvarchar(100)  NOT NULL
    ,IsValid bit  NOT NULL
    ,CreateTime datetime  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.EntranceCards  ADD CONSTRAINT
PK_EntranceCards  PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'EntranceCards ', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'卡号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'EntranceCards ', N'COLUMN', N'CardNum'
Go
DECLARE @v sql_variant 
SET @v = N'卡描述'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'EntranceCards ', N'COLUMN', N'CardDec'
Go
DECLARE @v sql_variant 
SET @v = N'全球唯一码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'EntranceCards ', N'COLUMN', N'GlobalCode'
Go
DECLARE @v sql_variant 
SET @v = N'是否可用'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'EntranceCards ', N'COLUMN', N'IsValid'
Go
DECLARE @v sql_variant 
SET @v = N'创建时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'EntranceCards ', N'COLUMN', N'CreateTime'
Go
DECLARE @v sql_variant 
SET @v = N'出入门卡信息'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'EntranceCards ' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[WeiXinMassMsgs]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[WeiXinMassMsgs]
Go
CREATE TABLE dbo.WeiXinMassMsgs(
    Id uniqueidentifier  NOT NULL
    ,thumb_media_id nvarchar(255)  NOT NULL
    ,author nvarchar(50)  NULL
    ,title nvarchar(100)  NOT NULL
    ,content_source_url nvarchar(255)  NULL
    ,content nvarchar(max)  NOT NULL
    ,digest nvarchar(255)  NULL
    ,show_cover_pic nvarchar(10)  NULL
    ,parentId uniqueidentifier  NULL
    ,media_id nvarchar(255)  NULL
    ,type nvarchar(20)  NULL
    ,pic_url nvarchar(255)  NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.WeiXinMassMsgs ADD CONSTRAINT
PK_WeiXinMassMsgs PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgs', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'图文消息缩略图的media_id'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgs', N'COLUMN', N'thumb_media_id'
Go
DECLARE @v sql_variant 
SET @v = N'图文消息的作者'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgs', N'COLUMN', N'author'
Go
DECLARE @v sql_variant 
SET @v = N'图文消息的标题'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgs', N'COLUMN', N'title'
Go
DECLARE @v sql_variant 
SET @v = N'在图文消息页面点击“阅读原文”后的页面'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgs', N'COLUMN', N'content_source_url'
Go
DECLARE @v sql_variant 
SET @v = N'图文消息页面的内容'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgs', N'COLUMN', N'content'
Go
DECLARE @v sql_variant 
SET @v = N'图文消息的描述'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgs', N'COLUMN', N'digest'
Go
DECLARE @v sql_variant 
SET @v = N'是否显示封面'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgs', N'COLUMN', N'show_cover_pic'
Go
DECLARE @v sql_variant 
SET @v = N'父节点ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgs', N'COLUMN', N'parentId'
Go
DECLARE @v sql_variant 
SET @v = N'媒体文件/图文消息上传后获取的唯一标识'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgs', N'COLUMN', N'media_id'
Go
DECLARE @v sql_variant 
SET @v = N'媒体文件类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgs', N'COLUMN', N'type'
Go
DECLARE @v sql_variant 
SET @v = N'图片本地地址'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgs', N'COLUMN', N'pic_url'
Go
DECLARE @v sql_variant 
SET @v = N'微信群发信息'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgs' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[WeiXinMedias]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[WeiXinMedias]
Go
CREATE TABLE dbo.WeiXinMedias(
    Id uniqueidentifier  NOT NULL
    ,media_id nvarchar(255)  NULL
    ,type nvarchar(20)  NULL
    ,created_at nvarchar(30)  NULL
    ,local_url nvarchar(255)  NULL
    ,fliename nvarchar(255)  NULL
    ,extention nvarchar(10)  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.WeiXinMedias ADD CONSTRAINT
PK_WeiXinMedias PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMedias', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'媒体文件/图文消息上传后获取的唯一标识'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMedias', N'COLUMN', N'media_id'
Go
DECLARE @v sql_variant 
SET @v = N'媒体文件类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMedias', N'COLUMN', N'type'
Go
DECLARE @v sql_variant 
SET @v = N'创建时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMedias', N'COLUMN', N'created_at'
Go
DECLARE @v sql_variant 
SET @v = N'本地服务器地址'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMedias', N'COLUMN', N'local_url'
Go
DECLARE @v sql_variant 
SET @v = N'文件名'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMedias', N'COLUMN', N'fliename'
Go
DECLARE @v sql_variant 
SET @v = N'扩展名'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMedias', N'COLUMN', N'extention'
Go
DECLARE @v sql_variant 
SET @v = N'微信资源'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMedias' 
Go

Commit
BEGIN TRANSACTION
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[WeiXinMassMsgHists]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[WeiXinMassMsgHists]
Go
CREATE TABLE dbo.WeiXinMassMsgHists(
    Id uniqueidentifier  NOT NULL
    ,media_id nvarchar(255)  NULL
    ,type nvarchar(20)  NULL
    ,tagetid int  NOT NULL
    ,groupid int  NULL
    ,msg_status nvarchar(20)  NULL
    ,TotalCount int  NULL
    ,FilterCount int  NULL
    ,SentCount int  NULL
    ,ErrorCount int  NULL
    ,WeiXinMassMsgId uniqueidentifier  NULL
    ,WeiXinMediaId uniqueidentifier  NULL
    ,CreateTime datetime  NOT NULL
    )  ON [PRIMARY]
Go
ALTER TABLE dbo.WeiXinMassMsgHists ADD CONSTRAINT
PK_WeiXinMassMsgHists PRIMARY KEY CLUSTERED (
Id
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go
DECLARE @v sql_variant 
SET @v = N'ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgHists', N'COLUMN', N'Id'
Go
DECLARE @v sql_variant 
SET @v = N'群发资源标示'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgHists', N'COLUMN', N'media_id'
Go
DECLARE @v sql_variant 
SET @v = N'媒体文件类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgHists', N'COLUMN', N'type'
Go
DECLARE @v sql_variant 
SET @v = N'群发对象类型'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgHists', N'COLUMN', N'tagetid'
Go
DECLARE @v sql_variant 
SET @v = N'群发分组id'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgHists', N'COLUMN', N'groupid'
Go
DECLARE @v sql_variant 
SET @v = N'群发状态'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgHists', N'COLUMN', N'msg_status'
Go
DECLARE @v sql_variant 
SET @v = N'群发总人数'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgHists', N'COLUMN', N'TotalCount'
Go
DECLARE @v sql_variant 
SET @v = N'准备发送的粉丝数'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgHists', N'COLUMN', N'FilterCount'
Go
DECLARE @v sql_variant 
SET @v = N'发送成功的粉丝数'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgHists', N'COLUMN', N'SentCount'
Go
DECLARE @v sql_variant 
SET @v = N'发送失败的粉丝数'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgHists', N'COLUMN', N'ErrorCount'
Go
DECLARE @v sql_variant 
SET @v = N'图文id'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgHists', N'COLUMN', N'WeiXinMassMsgId'
Go
DECLARE @v sql_variant 
SET @v = N'资源ID'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgHists', N'COLUMN', N'WeiXinMediaId'
Go
DECLARE @v sql_variant 
SET @v = N'群发时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgHists', N'COLUMN', N'CreateTime'
Go
DECLARE @v sql_variant 
SET @v = N'微信群发历史'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'WeiXinMassMsgHists' 
Go

Commit
Begin Transaction
ALTER TABLE dbo.ModuleRoleToModules ADD CONSTRAINT
FK_ModuleRoleToModules_ModuleRoles FOREIGN KEY(
ModuleRoleId
) REFERENCES dbo.ModuleRoles(
[Id]
) ON UPDATE  NO ACTION
 ON DELETE  NO ACTION
ALTER TABLE dbo.ModuleRoleToModules ADD CONSTRAINT
FK_ModuleRoleToModules_Modules FOREIGN KEY(
ModuleId
) REFERENCES dbo.Modules(
[Id]
) ON UPDATE  NO ACTION
 ON DELETE  NO ACTION
ALTER TABLE dbo.UserModuleRoles ADD CONSTRAINT
FK_UserModuleRoles_Users FOREIGN KEY(
UserId
) REFERENCES dbo.Users(
[Id]
) ON UPDATE  NO ACTION
 ON DELETE  NO ACTION
ALTER TABLE dbo.UserModuleRoles ADD CONSTRAINT
FK_UserModuleRoles_ModuleRoles FOREIGN KEY(
ModuleRoleId
) REFERENCES dbo.ModuleRoles(
[Id]
) ON UPDATE  NO ACTION
 ON DELETE  NO ACTION
ALTER TABLE dbo.UserFastModules ADD CONSTRAINT
FK_UserFastModules_Users FOREIGN KEY(
UserId
) REFERENCES dbo.Users(
[Id]
) ON UPDATE  NO ACTION
 ON DELETE  NO ACTION
ALTER TABLE dbo.UserFastModules ADD CONSTRAINT
FK_UserFastModules_Modules FOREIGN KEY(
ModuleId
) REFERENCES dbo.Modules(
[Id]
) ON UPDATE  NO ACTION
 ON DELETE  NO ACTION

Commit
