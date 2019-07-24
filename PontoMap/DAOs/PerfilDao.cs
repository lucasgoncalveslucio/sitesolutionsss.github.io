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
    public class PerfilDao : BaseDao, ICrud<Usuario>
    {
        private StringBuilder strSql = new System.Text.StringBuilder();

        public bool Create(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public Usuario Login(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public List<Perfil> GetPerfisByEmail(Usuario usuario)
        {
            strSql.Append(" SELECT DsPerfil FROM RelacPerfilUsuario WHERE Idusuario = ");
            strSql.Append(" (SELECT Idusuario  from Usuario WHERE DsEmail =  @DsEmail)");

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@DsEmail", usuario.DsEmail, DbType.String, ParameterDirection.Input);

            return Query<Perfil>(strSql.ToString(), parametros);
        }

        public List<Usuario> Read(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(Usuario obj)
        {
            throw new NotImplementedException();
        }
    }
}