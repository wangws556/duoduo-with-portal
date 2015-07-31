/**************Script 2************************************/
use tempdb
go
if exists (select * from dbo.sysobjects where id = object_id(N'[tbl1]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [tbl1]
GO
create table tbl1
(studentID int,studnetName varchar(20),Male char(2), age int,enter_date datetime,memo varchar(5000))
go
declare @i int
set @i=0
declare  @j int
set @j=0
while @i<50000
begin
if (rand()*10>3) set @j=1 else set @j=0
insert into tbl1 values(@i,
 char( rand()*10+100)+char( rand()*5+50)+char( rand()*3+100)+char( rand()*6+80),
@j, 20+rand()*10,convert(varchar(20), getdate()-rand()*3000,112), 
replicate(char( rand()*9+100)+char( rand()*4+50)+char( rand()*2+130)+char( rand()*5+70),100))
set @i=@i+1
end
/**************************************************/
go
set nocount on
declare @i int,@StudentID int,@Rdate datetime
set @i=0
while (@i<3000)
begin
set @i=@i+1
set @StudentID=rand()*50000
select  @Rdate=enter_date from tbl1 where studentID=@StudentID
end
