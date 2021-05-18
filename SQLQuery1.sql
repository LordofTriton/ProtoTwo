--CREATE procedure [dbo].[UpdateVisitorList]
--(
--	@Tag varchar(50),

--	@ID int,
--	@FirstName varchar(50),
--	@LastName varchar(50),
--	@Residence varchar(50),
--	@Phone varchar(50),
--	@Check_In varchar(50),
--	@Check_Out varchar(50),
--	@Purpose varchar(50),
--	@Meeting varchar(50),
--	@RegDate date
--)
--as
--begin
--	UPDATE visitordb
--	SET ID = @ID,
--	FirstName = @FirstName,
--	LastName = @LastName,
--	Residence = @Residence,
--	Phone = @Phone,
--	Check_In = @Check_In,
--	Check_Out = @Check_Out,
--	Purpose = @Purpose,
--	Meeting = @Meeting,
--	RegDate = @RegDate
--	WHERE Tag = @Tag
--End

CREATE procedure [dbo].[DeleteVisitor]
(
	@Tag varchar(50)
)
as
begin
	DELETE FROM visitordb WHERE Tag = @Tag;
End