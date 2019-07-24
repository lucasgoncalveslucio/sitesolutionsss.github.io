ALTER PROCEDURE [dbo].[SP_CriarEmpresa] @DsCnpj VARCHAR(50),
                                        @DsRazaoSocial VARCHAR(50),
                                        @NmFantasia VARCHAR(50),
                                        @CdCpf VARCHAR(50),
                                        @DsEmail VARCHAR(50),
                                        @DsCelular VARCHAR(50),
                                        @CdPassword VARCHAR(50),
                                        @DtNascimento DATE, 
                                        @NmUsuario VARCHAR(50) 
AS BEGIN BEGIN TRY BEGIN TRANSACTION 
	DECLARE @IdEmpresaIdentity int DECLARE @IdUsuarioIdentity int
	INSERT INTO dbo.Empresa (DsCnpj, DsRazaoSocial, NmFantasia )
	VALUES (@DsCnpj,
	        @DsRazaoSocial,
	        @NmFantasia)
	SET @IdEmpresaIdentity = SCOPE_IDENTITY();
	
	
	INSERT INTO dbo.Usuario (IdEmpresa, CdCpf, DsEmail, DsCelular, CdPassword, DtNascimento, NmUsuario,CdIsAdmin)
	VALUES (@IdEmpresaIdentity,
	        @CdCpf,
	        @DsEmail,
	        @DsCelular,
	        @CdPassword,
	        @DtNascimento,
	        @NmUsuario,
	        1)
	SET @IdUsuarioIdentity = SCOPE_IDENTITY();
	
	
	INSERT INTO RelacPerfilUsuario ( DsPerfil, Idusuario )
	VALUES( 'admin',
	        @IdUsuarioIdentity )
	        
		INSERT INTO RelacPerfilUsuario ( DsPerfil, Idusuario )
	VALUES( 'funcionario',
	        @IdUsuarioIdentity )
	        
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