using PontoMap.BOs;
using PontoMap.DAOs;
using PontoMap.Models;
using PontoMap.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PontoMap.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            var usuarioLogado = new UsuarioBo().Login(usuario);

            if (usuarioLogado == null)
            {
                ModelState.AddModelError(string.Empty, "Usuário e/ou senha incorreto(s).");
                return View(usuario);
            }

            List<Perfil> perfilList = new PerfilDao().GetPerfisByEmail(new Usuario { DsEmail = usuario.DsEmail });
            string[] perfis = perfilList.Select(x => x.DsPerfil).ToArray();


            var principle = new GenericPrincipal(new GenericIdentity($"{usuario.NmUsuario} {usuario.NmUsuario}"), perfis);
            var ticket = new FormsAuthenticationTicket(1, principle.Identity.Name, DateTime.Now, DateTime.Now.AddMonths(30), true, string.Join(",", perfis));
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.SetCookie(cookie);

            Session["Nome"] = usuarioLogado.NmUsuario;
            Session["IdEmpresa"] = usuarioLogado.IdEmpresa;
            Session["IdUsuario"] = usuarioLogado.IdUsuario;

            Session["Role"] = "authorize";

            TempData["message"] = "Logado com sucesso!";

            if (usuarioLogado.Perfis.Select(x => x.DsPerfil).Contains("master"))
            {
                Session["Role"] = "master";
                return RedirectToAction("Index", "Empresa");
            }


            if (usuarioLogado.Perfis.Select(x => x.DsPerfil).Contains("admin"))
            {
                Session["Role"] = "admin";
                return RedirectToAction("Index", "Usuario");
            }

            if (usuarioLogado.Perfis.Select(x => x.DsPerfil).Contains("funcionario"))
            {
                Session["Role"] = "funcionario";
                return RedirectToAction("EspelhoPonto", "Usuario");
            }

            return RedirectToAction("Index", "Home");
        }

        [CustomAuthorize]
        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            TempData["message"] = "Usuario deslogado com sucesso.";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrarViewModel registrarObj)
        {
            Empresa empresa = registrarObj.Empresa;
            empresa.UsuarioAdmin = registrarObj.Usuario;
            empresa.UsuarioAdmin.CdPassword = registrarObj.Password;

            new EmpresaBo().Create(empresa);

            if (empresa.Status == 0)
            {
                ModelState.AddModelError(string.Empty, empresa.Mensagem);
                return View(registrarObj);
            }

            TempData["message"] = "Parabens, você se cadastrou com sucesso. Utilize o formulário abaixo para entrar e começar.";
            return RedirectToAction("Login", "Account");
        }


        public ActionResult PaginaNaoAutorizada()
        {
            return View();
        }
    }
}
