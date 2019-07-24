using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PontoMap.DAOs;
using PontoMap.Models;

namespace PontoMap.Controllers
{
    public class EmpresaController : Controller
    {

        [CustomAuthorize(Roles = "master")]
        public ActionResult Index()
        {
            List<Empresa> empresaList = new EmpresaDao().Read(new Empresa());

            return View(empresaList);
        }

        [CustomAuthorize(Roles = "master")]
        public ActionResult Details(int id)
        {
            return View();
        }

        [CustomAuthorize(Roles = "master")]
        public ActionResult Create()
        {
            return View();
        }

        [CustomAuthorize(Roles = "master")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [CustomAuthorize(Roles = "master")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [CustomAuthorize(Roles = "master")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [CustomAuthorize(Roles = "master")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [CustomAuthorize(Roles = "master")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
