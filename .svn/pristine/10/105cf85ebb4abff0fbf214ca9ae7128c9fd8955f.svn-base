USE [AdventureWorks]
GO
drop table Employee_Demo_Heap
go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Employee_Demo_Heap](
	[EmployeeID] [int] NOT NULL,
	[NationalIDNumber] [nvarchar](15) NOT NULL,
	[ContactID] [int] NOT NULL,
	[LoginID] [nvarchar](256) NOT NULL,
	[ManagerID] [int] NULL,
	[Title] [nvarchar](50) NOT NULL,
	[BirthDate] [datetime] NOT NULL,
	[MaritalStatus] [nchar](1) NOT NULL,
	[Gender] [nchar](1) NOT NULL,
	[HireDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL   DEFAULT (getdate()),
 CONSTRAINT [PK_Employee_EmployeeID_Demo_Heap] PRIMARY KEY nonCLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, 
IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_Employee_ManagerID_Demo_Heap] ON [Employee_Demo_Heap] 
(
	[ManagerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Employee_ModifiedDate_Demo_Heap] ON [Employee_Demo_Heap] 
(
	[ModifiedDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
insert into Employee_Demo_Heap
select [EmployeeID] ,
	[NationalIDNumber] ,
	[ContactID] ,
	[LoginID] ,
	[ManagerID],
	[Title] ,
	[BirthDate] ,
	[MaritalStatus] ,
	[Gender] ,
	[HireDate] ,
	[ModifiedDate] from HumanResources.Employee
go
