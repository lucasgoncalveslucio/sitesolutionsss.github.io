using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using PontoMap.DAOs;

namespace PontoMap.Models
{
    public class Roles : RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            Usuario user = new UsuarioDao().Login(new Usuario());
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            List<Perfil> perfilList = new PerfilDao().GetPerfisByEmail(new Usuario { DsEmail = username });
            string[] retorno = perfilList.Select(x => x.DsPerfil).ToArray();
            return retorno;
        }
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}