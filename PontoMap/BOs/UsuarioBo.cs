using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PontoMap.CustomValidations;
using PontoMap.DAOs;
using PontoMap.Interfaces;
using PontoMap.Models;

namespace PontoMap.BOs
{
    public class UsuarioBo : ICrud<Usuario>
    {
        public bool Create(Usuario usuario)
        {
            try
            {
                List<string> atributosParaValidar = new List<string>();
                atributosParaValidar.Add(nameof(usuario.CdCpf));
                atributosParaValidar.Add(nameof(usuario.DsEmail));
                atributosParaValidar.Add(nameof(usuario.DsCelular));
                atributosParaValidar.Add(nameof(usuario.CdPassword));
                atributosParaValidar.Add(nameof(usuario.DtNascimento));
                atributosParaValidar.Add(nameof(usuario.NmUsuario));

                if (Util.ValidaAtributos(usuario, atributosParaValidar))
                {
                    usuario.CdCpf = Util.RemoveNaoNumericos(usuario.CdCpf);
                    usuario.DsCelular = Util.RemoveNaoNumericos(usuario.DsCelular);
                    usuario.CdPassword = Util.RemoveNaoNumericos(usuario.CdPassword);
                    return new UsuarioDao().Create(usuario);
                }
                return false;
            }
            catch (SqlException sqlExc)
            {
                foreach (SqlError error in sqlExc.Errors)
                {
                    usuario.Status = 0;

                    if (error.Number == 2627)
                    {
                        usuario.Mensagem = "Já existe um funcionário com as informações fornecidas no sistema, para dúvidas ou informações entre em contato";
                        return false;
                    }
                    usuario.Mensagem += string.Format("{0}: {1}", error.Number, error.Message);
                }
                return false;
            }
            catch (Exception ex)
            {
                usuario.Mensagem = ex.ToString();
                return false;
            }
        }

        public Usuario Get(Usuario usuario)
        {

            try
            {

                List<string> atributosParaValidar = new List<string>();
                atributosParaValidar.Add(nameof(usuario.IdUsuario));
                atributosParaValidar.Add(nameof(usuario.IdEmpresa));
                if (Util.ValidaAtributos(usuario, atributosParaValidar))
                {
                    return new UsuarioDao().Get(usuario);
                }
                return null;
            }
            catch (Exception ex)
            {
                usuario.Mensagem = ex.ToString();
                return null;
            }
        }

        public bool Delete(Usuario usuario)
        {
            try
            {
                List<string> atributosParaValidar = new List<string>();
                atributosParaValidar.Add(nameof(usuario.IdUsuario));
                atributosParaValidar.Add(nameof(usuario.IdEmpresa));
                if (Util.ValidaAtributos(usuario, atributosParaValidar))
                {
                    return new UsuarioDao().Delete(usuario);
                }
                return false;
            }
            catch (Exception ex)
            {
                usuario.Mensagem = ex.ToString();
                return false;
            }
        }

        public Usuario Login(Usuario usuario)
        {

            try
            {

                List<string> atributosParaValidar = new List<string>();
                atributosParaValidar.Add(nameof(usuario.DsEmail));
                atributosParaValidar.Add(nameof(usuario.CdPassword));
                if (Util.ValidaAtributos(usuario, atributosParaValidar))
                {
                    return new UsuarioDao().Login(usuario);
                }
                return null;
            }
            catch (Exception ex)
            {
                usuario.Mensagem = ex.ToString();
                return null;
            }
        }

        public List<Usuario> Read(Usuario usuario)
        {
            try
            {
                List<string> atributosParaValidar = new List<string>();
                atributosParaValidar.Add(nameof(usuario.IdEmpresa));
                if (Util.ValidaAtributos(usuario, atributosParaValidar))
                    return new UsuarioDao().Read(usuario);
                return null;
            }
            catch (Exception ex)
            {
                usuario.Mensagem = ex.ToString();
                return null;
            }

        }

        public bool Update(Usuario usuario)
        {
            try
            {
                if (Util.ValidaObjeto(usuario))
                {
                    usuario.CdCpf = Util.RemoveNaoNumericos(usuario.CdCpf);
                    usuario.DsCelular = Util.RemoveNaoNumericos(usuario.DsCelular);
                    return new UsuarioDao().Update(usuario);
                }

                return false;
            }
            catch (SqlException sqlExc)
            {
                foreach (SqlError error in sqlExc.Errors)
                {
                    usuario.Status = 0;

                    if (error.Number == 2627)
                    {
                        usuario.Mensagem = "Já existe um funcionário com as informações, para dúvidas ou informações entre em contato";
                        return false;
                    }
                    usuario.Mensagem += string.Format("{0}: {1}", error.Number, error.Message);
                }
                return false;
            }
            catch (Exception ex)
            {
                usuario.Mensagem = ex.ToString();
                return false;
            }
        }
    }
}