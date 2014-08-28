/* Must SET these server options before compiling any sproc */
SET ANSI_WARNINGS OFF
SET ANSI_NULLS ON
SET ANSI_PADDING OFF
SET QUOTED_IDENTIFIER OFF
SET ANSI_NULL_DFLT_ON OFF
SET IMPLICIT_TRANSACTIONS OFF
SET NOCOUNT ON
go
/*******************************************************
Created by:		Hanggara Prameswara
Created date:	27/04/2012
Description:	Retrieve user information for
				email purpose
********************************************************/

if exists (select * from sysobjects
           where name = N'RetrieveUserEmail' and type = N'P')
    drop proc dbo.RetrieveUserEmail
go
CREATE PROCEDURE [dbo].[RetrieveUserEmail]
	@chMachineName		char(50),
	@iEventid			bigint 
	
AS
BEGIN

Declare @suppressedList Table
(
	iEventId int,
	chUsername char(60)
)

	insert into @suppressedList
	select iEventId,
		   chUsername
	from tblSuppressedNotification(nolock)
	where iEventId = @iEventid

--test only
--select * from @suppressedList

	select u.chUsername,
		   u.vchFirstname,
		   u.vchLastname,
		   u.vchPrimaryEmail,
		   u.vchSecondaryEmail
	from tblUser u(nolock)
	inner join tblMonitoring mon(nolock) on mon.chUsername = u.chUsername
	inner join tblMachine m(nolock) on m.chMachineName = mon.chMachineName
	where m.chMachineName = @chMachineName 
	and u.chUsername not in(
		select chUsername
		from @suppressedList
		
	)
	/*and @iEventid not in(		select iEventId
								from tblSuppressedNotification(nolock)
								where iEventId = @iEventid
								and chUsername = u.chUsername
													)*/

END
GO
grant execute on RetrieveUserEmail to Public
go

/*
exec RetrieveUserEmail 'ca-tst-ofs-02.icaa.org.au',257

select * from tblEventLog
order by dttimegenerated desc
where iEventId is not null
*/