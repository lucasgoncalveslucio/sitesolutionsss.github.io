using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PontoMap.CustomValidations;
using PontoMap.DAOs;
using PontoMap.Interfaces;
using PontoMap.Models;

namespace PontoMap.BOs
{
    public class PontoBo : ICrud<Ponto>
    {
        public bool Create(Ponto ponto)
        {
            try
            {
                List<string> atributosParaValidar = new List<string>();
                atributosParaValidar.Add(nameof(ponto.IdUsuario));
                atributosParaValidar.Add(nameof(ponto.IdEmpresa));
                atributosParaValidar.Add(nameof(ponto.CdLat));
                atributosParaValidar.Add(nameof(ponto.CdLng));
                atributosParaValidar.Add(nameof(ponto.DtRegistro));
                if (Util.ValidaAtributos(ponto, atributosParaValidar))
                {
                    return new PontoDao().Create(ponto);
                }
                return false;
            }
            catch (Exception ex)
            {
                ponto.Mensagem = ex.ToString();
                return false;
            }
        }

        public bool Delete(Ponto obj)
        {
            throw new NotImplementedException();
        }

        public List<Ponto> Read(Ponto ponto)
        {
            List<string> atributosParaValidar = new List<string>();
            atributosParaValidar.Add(nameof(ponto.IdUsuario));
            atributosParaValidar.Add(nameof(ponto.IdEmpresa));
            if (Util.ValidaAtributos(ponto, atributosParaValidar))
            {
                return new PontoDao().Read(ponto);
            }
            return null;
        }

        public List<Ponto> RelatorioPonto(Ponto ponto, DateTime dtInicio, DateTime dtFim)
        {
            List<string> atributosParaValidar = new List<string>();
            atributosParaValidar.Add(nameof(ponto.IdUsuario));
            atributosParaValidar.Add(nameof(ponto.IdEmpresa));
            if (Util.ValidaAtributos(ponto, atributosParaValidar))
            {
                return new PontoDao().RelatorioPonto(ponto, dtInicio, dtFim);
            }
            return null;
        }

        public List<Ponto> RelatorioPontoWs(Ponto ponto, DateTime dtInicio, DateTime dtFim)
        {
            List<string> atributosParaValidar = new List<string>();
            atributosParaValidar.Add(nameof(ponto.IdEmpresa));
            if (!Util.ValidaAtributos(ponto, atributosParaValidar))
            {
                return null;
            }

            try
            {
                return new PontoDao().RelatorioPontoWs(ponto, dtInicio, dtFim);
            }
            catch (Exception)
            {
                return null;
            }
            
            
        }

        public bool Update(Ponto obj)
        {
            throw new NotImplementedException();
        }
    }
}