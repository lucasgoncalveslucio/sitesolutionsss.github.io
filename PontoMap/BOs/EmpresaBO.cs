using PontoMap.DAOs;
using PontoMap.Interfaces;
using PontoMap.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PontoMap.CustomValidations;

namespace PontoMap.BOs
{
    public class EmpresaBo : ICrud<Empresa>
    {
        public bool Create(Empresa empresa)
        {
            try
            {
                List<string> atributosParaValidar = new List<string>();
                atributosParaValidar.Add(nameof(empresa.DsCnpj));
                atributosParaValidar.Add(nameof(empresa.DsRazaoSocial));
                atributosParaValidar.Add(nameof(empresa.NmFantasia));
                atributosParaValidar.Add(nameof(empresa.UsuarioAdmin.CdCpf));
                atributosParaValidar.Add(nameof(empresa.UsuarioAdmin.DsEmail));
                atributosParaValidar.Add(nameof(empresa.UsuarioAdmin.DsCelular));
                atributosParaValidar.Add(nameof(empresa.UsuarioAdmin.CdPassword));
                atributosParaValidar.Add(nameof(empresa.UsuarioAdmin.DtNascimento));
                atributosParaValidar.Add(nameof(empresa.UsuarioAdmin.NmUsuario));

                if (Util.ValidaAtributos(empresa, atributosParaValidar))
                {
                    empresa.DsCnpj = Util.RemoveNaoNumericos(empresa.DsCnpj);
                    empresa.UsuarioAdmin.CdCpf = Util.RemoveNaoNumericos(empresa.UsuarioAdmin.CdCpf);
                    empresa.UsuarioAdmin.DsCelular = Util.RemoveNaoNumericos(empresa.UsuarioAdmin.DsCelular);

                    return new EmpresaDao().Create(empresa);
                }

                return false;


            }
            catch (SqlException sqlExc)
            {
                foreach (SqlError error in sqlExc.Errors)
                {
                    empresa.Status = 0;

                    if(error.Number == 50000)
                    {
                        empresa.Mensagem = "Já existe uma empresa e/ou um funcinário com as informações fornecidas no sistema, para dúvidas ou informações entre em contato";
                        return false;
                    }
                    empresa.Mensagem += string.Format("{0}: {1}", error.Number, error.Message);
                }
                return false;
            }
            catch (Exception ex)
            {
                empresa.Status = 0;
                empresa.Mensagem = ex.ToString();
                return false;
            }
        }

        public bool Delete(Empresa empresa)
        {
            try
            {
                if (new EmpresaDao().Delete(empresa))
                {
                    empresa.Status = 1;
                    empresa.Mensagem = "Empresa Excluida com sucesso!";
                    return true;
                }

                return false;
            }
            catch (SqlException sqlExc)
            {
                foreach (SqlError error in sqlExc.Errors)
                {
                    empresa.Status = 0;

                    if (error.Number == 50000)
                    {
                        empresa.Mensagem = "Já existe uma empresa e/ou um funcinário com as informações fornecidas no sistema, para dúvidas ou informações entre em contato";
                        return false;
                    }
                    empresa.Mensagem += string.Format("{0}: {1}", error.Number, error.Message);
                }
                return false;
            }
            catch (Exception ex)
            {
                empresa.Mensagem = ex.ToString();
                return false;
            }
        }

        public Empresa Login(Empresa empresa)
        {
            try
            {
                return new EmpresaDao().Login(empresa);
            }
            catch (Exception ex)
            {
                empresa.Mensagem = ex.ToString();
                return null;
            }
        }

        public List<Empresa> Read(Empresa empresa)
        {
            try
            {
                return new EmpresaDao().Read(empresa);
            }
            catch (Exception ex)
            {
                empresa.Mensagem = ex.ToString();
                return null;
            }
        }

        public bool Update(Empresa empresa)
        {
            try
            {
                if (new EmpresaDao().Update(empresa))
                {
                    empresa.Status = 1;
                    empresa.Mensagem = "Empresa Editada com sucesso!";
                    return true;
                }

                return false;
            }
            catch (SqlException sqlExc)
            {
                foreach (SqlError error in sqlExc.Errors)
                {
                    empresa.Status = 0;

                    if (error.Number == 50000)
                    {
                        empresa.Mensagem = "Já existe uma empresa e/ou um funcinário com as informações fornecidas no sistema, para dúvidas ou informações entre em contato";
                        return false;
                    }
                    empresa.Mensagem += string.Format("{0}: {1}", error.Number, error.Message);
                }
                return false;
            }
            catch (Exception ex)
            {
                empresa.Mensagem = ex.ToString();
                return false;
            }
        }
    }
}