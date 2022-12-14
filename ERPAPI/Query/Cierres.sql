USE [ERP]
GO
/****** Object:  StoredProcedure [dbo].[Cierres]    Script Date: 23/1/2020 18:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER   PROCEDURE [dbo].[Cierres] 
	--Parametros
	
	@pIdBitacora    int
	
AS
	DECLARE @result int = 0
	DECLARE @result1 int = 0
	DECLARE @result2 int = 0
	DECLARE @result3 int = 0

BEGIN
	--PASO1
	BEGIN TRANSACTION    
		BEGIN TRY  

			--Paso 1
			EXEC @result1 = [dbo].[CierresPaso1_Historicos]  @pIdBitacora;

			--Paso 2 
			EXEC @result2 = [dbo].[CierresPaso2_CertificadosMaxSum]  @pIdBitacora;

			

		END TRY  
		BEGIN CATCH  
			EXECUTE usp_GetErrorInfo;  
			IF @@TRANCOUNT > 0  
				ROLLBACK TRANSACTION; 				
		END CATCH;    
	IF @@TRANCOUNT > 0  
		COMMIT TRANSACTION;  	
	return @result;
END 


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER   PROCEDURE [dbo].[CierresPaso1_Historicos] 
	

	@pIdBitacora    int

AS
BEGIN
			DECLARE 
				@nPasoCierre int = 1


			INSERT INTO [dbo].[CierresAccounting]
           ([AccountId]
           ,[ParentAccountId]
           ,[CompanyInfoId]
           ,[AccountBalance]
           ,[Description]
           ,[IsCash]
           ,[TypeAccountId]
           ,[BlockedInJournal]
           ,[AccountCode]
           ,[IdEstado]
           ,[Estado]
           ,[HierarchyAccount]
           ,[AccountName]
           ,[UsuarioCreacion]
           ,[UsuarioModificacion]
           ,[FechaCreacion]
           ,[FechaModificacion]
           ,[ParentAccountAccountId]
		   ,[BitacoraCierreContableId]
           ,[FechaCierre])
     
			SELECT [AccountId]
           ,[ParentAccountId]
           ,[CompanyInfoId]
           ,[AccountBalance]
           ,[Description]
           ,[IsCash]
           ,[TypeAccountId]
           ,[BlockedInJournal]
           ,[AccountCode]
           ,[IdEstado]
           ,[Estado]
           ,[HierarchyAccount]
           ,[AccountName]
           ,[UsuarioCreacion]
           ,[UsuarioModificacion]
           ,[FechaCreacion]
           ,[FechaModificacion]
           ,[ParentAccountAccountId]
		   ,@pIdBitacora
           ,GETDATE() FROM Accounting	;	

		   --INSERTA LOS DIARIOS A LOS HISTORICOS

	INSERT INTO CierresJournal
					   ([FechaCierre]
					   ,[GeneralLedgerHeaderId]
					   ,[PartyTypeId]
					   ,[PartyTypeName]
					   ,[DocumentId]
					   ,[PartyId]
					   ,[VoucherType]
					   ,[TypeJournalName]
					   ,[Date]
					   ,[DatePosted]
					   ,[Memo]
					   ,[ReferenceNo]
					   ,[Posted]
					   ,[GeneralLedgerHeaderId1]
					   ,[PartyId1]
					   ,[IdPaymentCode]
					   ,[IdTypeofPayment]
					   ,[EstadoId]
					   ,[EstadoName]
					   ,[TotalDebit]
					   ,[TotalCredit]
					   ,[TypeOfAdjustmentId]
					   ,[TypeOfAdjustmentName]
					   ,[CreatedUser]
					   ,[ModifiedUser]
					   ,[CreatedDate]
					   ,[ModifiedDate]
					   ,[BitacoraCierreContableId]
					   ,[JournalEntryId])

				SELECT SYSDATETIME()
					  ,[GeneralLedgerHeaderId]
					  ,[PartyTypeId]
					  ,[PartyTypeName]
					  ,[DocumentId]
					  ,[PartyId]
					  ,[VoucherType]
					  ,[TypeJournalName]
					  ,[Date]
					  ,[DatePosted]
					  ,[Memo]
					  ,[ReferenceNo]
					  ,[Posted]
					  ,[GeneralLedgerHeaderId1]
					  ,[PartyId1]
					  ,[IdPaymentCode]
					  ,[IdTypeofPayment]
				      ,[EstadoId]
					  ,[EstadoName]
					  ,[TotalDebit]
					  ,[TotalCredit]
					  ,[TypeOfAdjustmentId]
					  ,[TypeOfAdjustmentName]
					  ,[CreatedUser]
					  ,[ModifiedUser]
					  ,[CreatedDate]
					  ,[ModifiedDate]
					  ,@pIdBitacora
					  ,[JournalEntryId]
				  FROM JournalEntry;
				
				-- INSERT LOS JOURNAL ENTRY LINES

				PRINT 'INSERT LOS JOURNAL ENTRY LINES'

			INSERT INTO CierresJournalEntryLine
					   ([JournalEntryLineId]
					   ,[JournalEntryId]
					   ,[Description]
					   ,[AccountId]
					   ,[DebitSy]
					   ,[Memo]
					   ,[AccountId1]
					   ,[CreatedUser]
					   ,[ModifiedUser]
					   ,[CreatedDate]
					   ,[ModifiedDate]
					   ,[Credit]
					   ,[CreditME]
					   ,[CreditSy]
					   ,[Debit]
					   ,[DebitME]
					   ,[CostCenterId]
					   ,[CostCenterName]
					   ,[AccountName]
					   )

				SELECT [JournalEntryLineId]
					  ,[JournalEntryId]
					  ,[Description]
					  ,[AccountId]
					  ,[DebitSy]
					  ,[Memo]
					  ,[AccountId1]
					  ,[CreatedUser]
					  ,[ModifiedUser]
					  ,[CreatedDate]
					  ,[ModifiedDate]
					  ,[Credit]
					  ,[CreditME]
					  ,[CreditSy]
					  ,[Debit]
					  ,[DebitME]
					  ,[CostCenterId]
					  ,[CostCenterName]
					  ,[AccountName]
				  FROM [dbo].[JournalEntryLine]

			PRINT 'INSERTO EN LA BITACORA DE PROCESOS';

			UPDATE BitacoraCierreProceso SET  Estatus = 'FINALIZADO' WHERE IdBitacoraCierre = @pIdBitacora And PasoCierre = @nPasoCierre
END

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER   PROCEDURE [dbo].[CierresPaso2_CertificadosMaxSum]
	-- Parametros

	@pIdBitacora    int
AS
BEGIN
	DECLARE 
		@ValorAcumulado decimal,
		@nPasoCierre int = 2;

		
	SET NOCOUNT ON;

	Select  @ValorAcumulado = SUM(cd.Total) from CertificadoDeposito cd

	UPDATE ElementoConfiguracion SET Valordecimal = @ValorAcumulado Where ElementoConfiguracion.Nombre = 'VALOR MAXIMO CERTIFICADOS'


	UPDATE BitacoraCierreProceso SET  Estatus = 'FINALIZADO' WHERE IdBitacoraCierre = @pIdBitacora And PasoCierre = @nPasoCierre
   
END
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER     PROCEDURE [dbo].[CierresPaso3_PolizasVencidas]
	-- Parametros
	@pIdBitacora    int
AS
BEGIN
	DECLARE 
		@nPasoCierre int = 3;



	UPDATE BitacoraCierreProceso SET  Estatus = 'FINALIZADO' WHERE PasoCierre = @pIdBitacora And PasoCierre = @nPasoCierre
   
END

