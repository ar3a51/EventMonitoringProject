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
Description:	Retrieve suppressed list based on 
				particular username.
********************************************************/

if exists (select * from sysobjects
           where name = N'RetrieveSuppressedList' and type = N'P')
    drop proc dbo.retrieveSuppressedList
go
CREATE PROCEDURE [dbo].[RetrieveSuppressedList]
	@chUsername			char(50) 
	
AS
BEGIN

	select iSuppressedId, 
		   iEventId, 
		   vchsource 
	from tblSuppressedNotification(nolock)
	where chUsername = @chUsername 


END
GO
grant execute on RetrieveSuppressedList to Public
go