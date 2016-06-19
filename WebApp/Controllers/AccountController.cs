using Business;
using Entities.Models;
using Suporte;
using Suporte.Criptografia;
using Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string ReturnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pass = Crypto.Encrypt(model.Password);
                    tbUser user;
                    using (var bll = new UserBll())
                    {
                        user = bll.Find(t => t.User == model.User && t.Password == pass).FirstOrDefault();
                    }
                    if (user == null)
                    {
                        throw new MyException("Incorrect user or password.", MessageBox.Erro.danger);
                    }
                    LoginHelper.SetAuthentication(user.Id + "|" + user.User, false, "");
                    var url = !string.IsNullOrEmpty(ReturnUrl) ? ReturnUrl : Url.Content("~/Home/Index");

                    return Redirect(url);

                }
            }
            catch (MyException ex)
            {
                TempData["Exists"] = true;
                TempData["Message"] = ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                TempData["Exists"] = true;
                TempData["Message"] = ex.Message;
                return View();
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Exists"] = true;
                    var msg = string.Join("<br>", ModelState.Where(t => t.Value.Errors.Count > 0).Select(t => t.Value.Errors[0].ErrorMessage));
                    TempData["Message"] = msg;
                    return View(model);
                }

                var pass = Crypto.Encrypt(model.Password);
                tbUser user = new tbUser { User = model.User, Password = pass };

                using (var bll = new UserBll())
                {
                    bll.Insert(user);
                    bll.Save();
                }
                TempData["Exists"] = true;
                TempData["Message"] = string.Format("{0} successfully registered!", model.User);
                return RedirectToAction("Login");
            }
            catch (MyException ex)
            {
                TempData["Exists"] = true;
                TempData["Message"] = string.Format(ex.Message, model.User);
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Exists"] = true;
                TempData["Message"] = string.Format(ex.Message, model.User);
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            LoginHelper.Logout();
            return RedirectToAction("Login", "Account");
        }

    }
}
