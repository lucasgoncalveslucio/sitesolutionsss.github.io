using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Dapper;
using PontoMap.CustomValidations;
using PontoMap.Interfaces;
using PontoMap.Models;

namespace PontoMap.DAOs
{
    public class PontoDao : BaseDao, ICrud<Ponto>
    {

        private StringBuilder strSql = new StringBuilder();

        public bool Create(Ponto ponto)
        {
            strSql.Append("INSERT INTO [dbo].[Ponto]");
            strSql.Append("		([Idusuario]");
            strSql.Append("		,[IdEmpresa]");
            strSql.Append("		,[DtRegistro]");
            strSql.Append("		,[CdLat]");
            strSql.Append("		,[CdLng])");
            strSql.Append("	VALUES");
            strSql.Append("		(@Idusuario");
            strSql.Append("		,@IdEmpresa");
            strSql.Append("		,@DtRegistro");
            strSql.Append("		,@CdLat");
            strSql.Append("		,@CdLng)");

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@Idusuario", ponto.IdUsuario, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@IdEmpresa", ponto.IdEmpresa, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@DtRegistro", ponto.DtRegistro, DbType.Date, ParameterDirection.Input);
            parametros.Add("@CdLat", ponto.CdLat, DbType.Decimal, ParameterDirection.Input);
            parametros.Add("@CdLng", ponto.CdLng, DbType.Decimal, ParameterDirection.Input);

            Execute(strSql.ToString(), parametros);
            ponto.Status = 1;
            ponto.Mensagem = "Ponto registrado com sucesso.";
            return true;
        }

        public List<Ponto> Read(Ponto ponto)
        {
            strSql.Append("SELECT [DtRegistro]");
            strSql.Append("		,[CdLat]");
            strSql.Append("		,[CdLng]");
            strSql.Append("	FROM [dbo].[Ponto]");
            strSql.Append("  WHERE Idusuario =  @Idusuario AND");
            strSql.Append(" DtRegistro BETWEEN @AbsoluteStart AND @AbsoluteEnd");

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@Idusuario", ponto.IdUsuario, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@AbsoluteStart", Util.AbsoluteStart(Util.HrBrasilia()), DbType.Date, ParameterDirection.Input);
            parametros.Add("@AbsoluteEnd", Util.AbsoluteEnd(Util.HrBrasilia()), DbType.Date, ParameterDirection.Input);

            ponto.Status = 1;
            return Query<Ponto>(strSql.ToString(), parametros);
        }

        public List<Ponto> RelatorioPonto(Ponto ponto, DateTime dtInicio, DateTime dtFim)
        {
            strSql.Append("SELECT *");
            strSql.Append("	FROM Ponto");
            strSql.Append("  WHERE Idusuario =  @Idusuario AND");
            strSql.Append("   IdEmpresa =  @IdEmpresa AND");
            strSql.Append(" DtRegistro BETWEEN @dtInicio AND @dtFim");

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@Idusuario", ponto.IdUsuario, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@IdEmpresa", ponto.IdEmpresa, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@dtInicio", dtInicio, DbType.Date, ParameterDirection.Input);
            parametros.Add("@dtFim", dtFim.Date.AddHours(23).AddMinutes(59).AddSeconds(59), DbType.Date, ParameterDirection.Input);


            ponto.Status = 1;
            return Query<Ponto>(strSql.ToString(), parametros);
        }

        public List<Ponto> RelatorioPontoWs(Ponto ponto, DateTime dtInicio, DateTime dtFim)
        {
            strSql.Append("SELECT *");
            strSql.Append("	FROM Ponto");
            strSql.Append("  WHERE IdEmpresa =  @IdEmpresa AND");
            strSql.Append(" DtRegistro BETWEEN @dtInicio AND @dtFim");

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@IdEmpresa", ponto.IdEmpresa, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@dtInicio", dtInicio, DbType.Date, ParameterDirection.Input);
            parametros.Add("@dtFim", dtFim.Date.AddHours(23).AddMinutes(59).AddSeconds(59), DbType.Date, ParameterDirection.Input);


            ponto.Status = 1;
            return Query<Ponto>(strSql.ToString(), parametros);
        }

        public bool Delete(Ponto ponto)
        {
            throw new NotImplementedException();
        }

        public bool Update(Ponto ponto)
        {
            throw new NotImplementedException();
        }
    }
}