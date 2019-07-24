using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PontoMap.Interfaces;
using PontoMap.Models;
using System.Text;
using Dapper;
using System.Data;

namespace PontoMap.DAOs
{
    public class EmpresaDao : BaseDao, ICrud<Empresa>
    {
        private StringBuilder strSql = new StringBuilder();

        public bool Create(Empresa empresa)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@DsCnpj", empresa.DsCnpj, DbType.String, ParameterDirection.Input);
            parametros.Add("@DsRazaoSocial", empresa.DsRazaoSocial, DbType.String, ParameterDirection.Input);
            parametros.Add("@NmFantasia", empresa.NmFantasia, DbType.String, ParameterDirection.Input);
            parametros.Add("@CdCpf", empresa.UsuarioAdmin.CdCpf, DbType.String, ParameterDirection.Input);
            parametros.Add("@DsEmail", empresa.UsuarioAdmin.DsEmail, DbType.String, ParameterDirection.Input);
            parametros.Add("@DsCelular", empresa.UsuarioAdmin.DsCelular, DbType.String, ParameterDirection.Input);
            parametros.Add("@CdPassword", empresa.UsuarioAdmin.CdPassword, DbType.String, ParameterDirection.Input);
            parametros.Add("@DtNascimento", empresa.UsuarioAdmin.DtNascimento, DbType.Date, ParameterDirection.Input);
            parametros.Add("@NmUsuario", empresa.UsuarioAdmin.NmUsuario, DbType.String, ParameterDirection.Input);

            Execute("SP_CriarEmpresa", CommandType.StoredProcedure, parametros);
            empresa.Status = 1;
            return true;
        }

        public Empresa Login(Empresa empresa)
        {
            return QueryFirstOrDefault<Empresa>("SELECT * FROM Accounts WHERE Id = @Id", new { empresa.IdEmpresa });
        }

        public List<Empresa> Read(Empresa empresa)
        {
            strSql.Append("SELECT [IdEmpresa]");
            strSql.Append("		,[DsCnpj]");
            strSql.Append("		,[DsRazaoSocial]");
            strSql.Append("		,[NmFantasia]");
            strSql.Append("	FROM [dbo].[Empresa]");

            return Query<Empresa>(strSql.ToString());
        }

        public bool Update(Empresa empresa)
        {
            strSql.Append("UPDATE [dbo].[Empresa]");
            strSql.Append("	SET [IdEmpresa] = <IdEmpresa, int>");
            strSql.Append("		,[DsCnpj] = <DsCnpj, varchar(50)>");
            strSql.Append("		,[DsRazaoSocial] = <DsRazaoSocial, varchar(50)>");
            strSql.Append("		,[NmFantasia] = <NmFantasia, varchar(50)>");
            strSql.Append("	WHERE <Search Conditions>");


            int ret = Execute(strSql.ToString());
            return true;
        }

        public bool Delete(Empresa empresa)
        {
            strSql.Append("DELETE FROM [dbo].[Empresa]");
            strSql.Append("		WHERE <Search Conditions>");

            int ret = Execute(strSql.ToString());
            return true;
        }
        
    }
}