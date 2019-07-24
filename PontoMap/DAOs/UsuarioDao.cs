using Dapper;
using PontoMap.Interfaces;
using PontoMap.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace PontoMap.DAOs
{
    public class UsuarioDao : BaseDao, ICrud<Usuario>
    {
        private StringBuilder strSql = new StringBuilder();

        public bool Create(Usuario usuario)
        {
            strSql.Append("INSERT INTO Usuario(");
            strSql.Append("		 IdEmpresa");
            strSql.Append("		,CdCpf");
            strSql.Append("		,DsEmail");
            strSql.Append("		,DsCelular");
            strSql.Append("		,CdPassword");
            strSql.Append("		,DtNascimento");
            strSql.Append("		,NmUsuario)");
            strSql.Append("	VALUES");
            strSql.Append("	    (@IdEmpresa");
            strSql.Append("		,@CdCpf");
            strSql.Append("		,@DsEmail");
            strSql.Append("		,@DsCelular");
            strSql.Append("		,@CdPassword");
            strSql.Append("		,@DtNascimento");
            strSql.Append("		,@NmUsuario);");
            strSql.Append(" SELECT CAST(SCOPE_IDENTITY() as int)");


            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@IdEmpresa", usuario.IdEmpresa, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@CdCpf", usuario.CdCpf, DbType.String, ParameterDirection.Input);
            parametros.Add("@DsEmail", usuario.DsEmail, DbType.String, ParameterDirection.Input);
            parametros.Add("@DsCelular", usuario.DsCelular, DbType.String, ParameterDirection.Input);
            parametros.Add("@CdPassword", usuario.CdPassword, DbType.String, ParameterDirection.Input);
            parametros.Add("@DtNascimento", usuario.DtNascimento, DbType.String, ParameterDirection.Input);
            parametros.Add("@NmUsuario", usuario.NmUsuario, DbType.String, ParameterDirection.Input);

            parametros.Add("@IdUsuario", DbType.Int32, direction: ParameterDirection.Output);
            int idUsuario =  QueryFirstOrDefault<int>(strSql.ToString(), parametros);

            //Associando o usuário inserido ao perfil de funcionario ==============================
            strSql =  new StringBuilder();
            strSql.Append(" INSERT INTO RelacPerfilUsuario( DsPerfil, Idusuario) VALUES (");
            strSql.Append(" @DsPerfil, @IdUsuario)");

            parametros = new DynamicParameters();
            parametros.Add("@DsPerfil", "funcionario", DbType.String, ParameterDirection.Input);
            parametros.Add("@Idusuario", idUsuario, DbType.Int32, ParameterDirection.Input);
            Execute(strSql.ToString(), parametros);
            //==================================================================================

            usuario.Status = 1;
            return true;
        }

        public Usuario Get(Usuario usuario)
        {
            strSql.Append("SELECT * ");
            strSql.Append("	FROM [dbo].[Usuario]");
            strSql.Append("  WHERE IdUsuario = @IdUsuario AND IdEmpresa = @IdEmpresa AND CdAtivo = 1");

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@IdUsuario", usuario.IdUsuario, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@idEmpresa", usuario.IdEmpresa, DbType.Int32, ParameterDirection.Input);

            var userToReturn = QueryFirstOrDefault<Usuario>(strSql.ToString(), parametros);

            if (userToReturn != null)
                userToReturn.Perfis = new PerfilDao().GetPerfisByEmail(userToReturn);

            usuario.Status = 1;
            return userToReturn;
        }

        public List<Usuario> Read(Usuario usuario)
        {
            strSql.Append("SELECT *");
            strSql.Append("	FROM [dbo].[Usuario]");
            strSql.Append(" WHERE IdEmpresa = @IdEmpresa AND CdAtivo = 1");

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@IdEmpresa", usuario.IdEmpresa, DbType.Int32, ParameterDirection.Input);

            usuario.Status = 1;
            return Query<Usuario>(strSql.ToString(), parametros);
        }

        public bool Update(Usuario usuario)
        {
            strSql.Append("UPDATE [dbo].[Usuario]");
            strSql.Append("	SET [IdEmpresa] = @IdEmpresa");
            strSql.Append("		,[CdCpf] = @CdCpf");
            strSql.Append("		,[DsEmail] = @DsEmail");
            strSql.Append("		,[DsCelular] = @DsCelular");
            strSql.Append("		,[CdPassword] = @CdPassword");
            strSql.Append("		,[DtNascimento] = @DtNascimento");
            strSql.Append("		,[NmUsuario] = @NmUsuario");
            strSql.Append("  WHERE IdUsuario = @IdUsuario AND IdEmpresa = @IdEmpresa");

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@IdUsuario", usuario.IdUsuario, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@IdEmpresa", usuario.IdEmpresa, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@CdCpf", usuario.CdCpf, DbType.String, ParameterDirection.Input);
            parametros.Add("@DsEmail", usuario.DsEmail, DbType.String, ParameterDirection.Input);
            parametros.Add("@DsCelular", usuario.DsCelular, DbType.String, ParameterDirection.Input);
            parametros.Add("@CdPassword", usuario.CdPassword, DbType.String, ParameterDirection.Input);
            parametros.Add("@DtNascimento", usuario.DtNascimento, DbType.Date, ParameterDirection.Input);
            parametros.Add("@NmUsuario", usuario.NmUsuario, DbType.String, ParameterDirection.Input);

            Execute(strSql.ToString(), parametros);
            usuario.Status = 1;
            return true;
        }

        public bool Delete(Usuario usuario)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@IdUsuario", usuario.IdUsuario, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@IdEmpresa", usuario.IdEmpresa, DbType.Int32, ParameterDirection.Input);

            Execute("SP_DeletarUsuario", CommandType.StoredProcedure, parametros);
            usuario.Status = 1;
            return true;
        }

        public bool Desativar(Usuario usuario)
        {
            strSql.Append("UPDATE [dbo].[Usuario]");
            strSql.Append("	SET [CdAtivo] = @CdAtivo");
            strSql.Append("  WHERE IdUsuario = @IdUsuario AND IdEmpresa = @IdEmpresa");

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@IdUsuario", usuario.IdUsuario, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@idEmpresa", usuario.IdEmpresa, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@CdAtivo", false, DbType.Boolean, ParameterDirection.Input);

            Execute(strSql.ToString(), parametros);
            usuario.Status = 1;
            return true;
        }

        public Usuario Login(Usuario usuario)
        {
            strSql.Append("SELECT * ");
            strSql.Append("	FROM [dbo].[Usuario]");
            strSql.Append("  WHERE DsEmail = @DsEmail AND CdPassword = @CdPassword AND CdAtivo = 1");

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@DsEmail", usuario.DsEmail, DbType.String, ParameterDirection.Input);
            parametros.Add("@CdPassword", usuario.CdPassword, DbType.String, ParameterDirection.Input);

            var userToReturn = QueryFirstOrDefault<Usuario>(strSql.ToString(), parametros);

            if (userToReturn != null)
                userToReturn.Perfis = new PerfilDao().GetPerfisByEmail(userToReturn);

            usuario.Status = 1;
            return userToReturn;
        }
    }
}