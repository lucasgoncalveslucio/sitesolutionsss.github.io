ALTER PROCEDURE [dbo].[SP_DeletarUsuario] @IdUsuario INT,
                                           @IdEmpresa INT
AS BEGIN BEGIN TRY BEGIN TRANSACTION 
  
  DELETE FROM RelacPerfilUsuario WHERE Idusuario = @IdUsuario;
  DELETE FROM Ponto WHERE Idusuario = @IdUsuario;
  DELETE FROM Usuario WHERE Idusuario = @IdUsuario AND IdEmpresa = @IdEmpresa;

  COMMIT TRAN -- Transaction Success!
END TRY BEGIN CATCH IF @@TRANCOUNT > 0
ROLLBACK TRAN -- RollBack in case of Error

 DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
        DECLARE @ErrorState INT = ERROR_STATE()

    -- Use RAISERROR inside the CATCH block to return error  
    -- information about the original error that caused  
    -- execution to jump to the CATCH block.  
    RAISERROR (@ErrorMessage, -- Message text.  
               @ErrorSeverity, -- Severity.  
               @ErrorState -- State.  
               );
 END CATCH END