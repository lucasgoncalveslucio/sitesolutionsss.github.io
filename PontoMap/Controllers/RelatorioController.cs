using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using PontoMap.BOs;
using PontoMap.CustomValidations;
using PontoMap.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PontoMap.Controllers
{
    public class RelatorioController : Controller
    {
        //Refatorar para apenas admin 
        [CustomAuthorize(Roles = "admin")]
        [HttpGet]
        public ActionResult GetRel(DateTime dtInicial, DateTime dtFinal, string tipoRel, string idUsuario)
        {
            Ponto ponto = new Ponto
            {
                IdEmpresa = Util.ValidaInteiro(Session["IdEmpresa"].ToString(), 0),
                IdUsuario = Util.ValidaInteiro(idUsuario, 0)
            };
            List<Ponto> pontos = new PontoBo().RelatorioPonto(ponto, dtInicial, dtFinal);

            if (ponto.Status != 1)
                return Content(JsonConvert.SerializeObject(ponto));

            switch (tipoRel)
            {
                case "grid":
                    return View("Grid", pontos);
                case "pdf":
                    return Pdf(pontos);
                default:
                    return Content(JsonConvert.SerializeObject(ponto));
            }
        }


        //Refatorar para apenas admin 
        [CustomAuthorize(Roles = "funcionario")]
        [HttpGet]
        public ActionResult GetRelUser(DateTime dtInicial, DateTime dtFinal, string tipoRel)
        {
            Ponto ponto = new Ponto
            {
                IdEmpresa = Util.ValidaInteiro(Session["IdEmpresa"].ToString(), 0),
                IdUsuario = Util.ValidaInteiro(Session["IdUsuario"].ToString(), 0)
            };
            List<Ponto> pontos = new PontoBo().RelatorioPonto(ponto, dtInicial, dtFinal);

            if (ponto.Status != 1)
                return Content(JsonConvert.SerializeObject(ponto));

            switch (tipoRel)
            {
                case "grid":
                    return View("Grid", pontos);
                case "pdf":
                    return Pdf(pontos);
                default:
                    return Content(JsonConvert.SerializeObject(ponto));
            }
        }



        public FileStreamResult Pdf(List<Ponto> pontos)
        {

            MemoryStream workStream = new MemoryStream();
            Document document = new Document();
            PdfWriter.GetInstance(document, workStream).CloseStream = false;
            PdfPCell c = new PdfPCell();
            PdfPTable t = new PdfPTable(2);

            document.Open();

            //imagem

            var logoPontoMap = Server.MapPath("~/Content/Img/") + "pontomap.png";
            var imgInstancia = Image.GetInstance(logoPontoMap);
            imgInstancia.Alignment = iTextSharp.text.Image.UNDERLYING;
            imgInstancia.ScaleAbsolute(100.0F, 70.0F);

            document.Add(imgInstancia);
            document.Add(new Paragraph(" ", FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));

            if (pontos.Count == 0)
            {

                Paragraph msgNaoHaRegistros = new Paragraph(" Não há registros para o intervalo de data selecionado.",
                    FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                msgNaoHaRegistros.SpacingBefore = 100f;
                document.Add(msgNaoHaRegistros);
            }
            else
            {
                Usuario user = new UsuarioBo().Get(new Usuario { IdUsuario = pontos[0].IdUsuario, IdEmpresa = int.Parse(Session["IdEmpresa"].ToString()) });

                t.TotalWidth = 144f;
                t.SpacingBefore = 100f;

                c = new PdfPCell();
                c.AddElement(new Chunk("FUNCIONÁRIO: " + user.NmUsuario));
                c.Colspan = 2;
                t.AddCell(c);
            }



            foreach (Ponto p in pontos)
            {
                c = new PdfPCell();
                c.AddElement(new Chunk("Data do registro"));
                t.AddCell(c);

                c = new PdfPCell();
                c.AddElement(new Chunk(p.DtRegistroString));
                t.AddCell(c);
            }


            document.Add(t);
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }


    }
}