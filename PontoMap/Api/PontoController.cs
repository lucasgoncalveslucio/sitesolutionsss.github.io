using PontoMap.CustomValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PontoMap.BOs;
using PontoMap.Models;

namespace PontoMap.Api
{
    public class PontoController : ApiController
    {

        [AllowAnonymous]
        [HttpGet]
        [Route("api/data/time")]
        public IHttpActionResult Get()
        {
            return Ok("Now server time is: " + DateTime.Now);
        }

        [CustomAuthorize]
        [HttpPost]
        [Route("api/ponto/RegistrarPonto")]
        public IHttpActionResult RegistrarPonto()
        {
            Ponto ponto = new Ponto();

            if (string.IsNullOrWhiteSpace(HttpContext.Current.Request.Params["lat"]) ||
                string.IsNullOrWhiteSpace(HttpContext.Current.Request.Params["lng"]))
            {
                ponto.Mensagem = "lat e lng obrigatórios";
                return Content(HttpStatusCode.BadRequest, JsonConvert.SerializeObject(ponto));
            }


            var identity = (ClaimsIdentity)User.Identity;
            dynamic obj = new JavaScriptSerializer().DeserializeObject(identity.FindFirst("credencial").Value);

            ponto = new Ponto
            {
                CdLat = decimal.Parse(HttpContext.Current.Request.Params["lat"]),
                CdLng = decimal.Parse(HttpContext.Current.Request.Params["lng"]),
                IdEmpresa = int.Parse(obj["IdEmpresa"].ToString()),
                IdUsuario = int.Parse(obj["IdUsuario"].ToString()),
                DtRegistro = Util.HrBrasilia()
            };

            new PontoBo().Create(ponto);

            if (ponto.Status == 1)
            {
                return Ok(JsonConvert.SerializeObject(ponto));
            }
            return BadRequest(JsonConvert.SerializeObject(ponto));
        }

        [CustomAuthorize]
        [HttpGet]
        [Route("api/ponto/GetPontoList")]
        public IHttpActionResult GetPontoList()
        {

            var identity = (ClaimsIdentity)User.Identity;
            dynamic obj = new JavaScriptSerializer().DeserializeObject(identity.FindFirst("credencial").Value);


            Ponto ponto = new Ponto
            {
                IdUsuario = int.Parse(obj["IdUsuario"].ToString()),
                IdEmpresa = int.Parse(obj["IdEmpresa"].ToString())
            };

            List<Ponto> registrosPonto = new PontoBo().Read(ponto);

            if (ponto.Status == 1)
                return Ok(JsonConvert.SerializeObject(registrosPonto));

            return BadRequest(JsonConvert.SerializeObject(ponto));
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("api/ponto/GetPontoListWs")]
        public IHttpActionResult GetPontoListWs()
        {

            var identity = (ClaimsIdentity)User.Identity;
            dynamic obj = new JavaScriptSerializer().DeserializeObject(identity.FindFirst("credencial").Value);


            DateTime dtInicio;
            DateTime dtFim;

            try
            {
                dtInicio = DateTime.Parse(HttpContext.Current.Request.Params["dtInicio"]);
                dtFim = DateTime.Parse(HttpContext.Current.Request.Params["dtFim"]);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, JsonConvert.SerializeObject("dtInicio e dtFim obrigatórios"));
            }

            var ponto = new Ponto
            {
                IdEmpresa = int.Parse(obj["IdEmpresa"].ToString())
            };

            List<Ponto> registrosPonto = new PontoBo().RelatorioPontoWs(ponto, dtInicio, dtFim);

            if (ponto.Status == 1)
                return Ok(JsonConvert.SerializeObject(registrosPonto));

            return BadRequest(JsonConvert.SerializeObject(ponto));
        }


    }
}
