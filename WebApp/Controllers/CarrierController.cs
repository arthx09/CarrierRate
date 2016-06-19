using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities.Models;
using System.Collections;
using WebApp.Helper;
using Business;
using Support;

namespace WebApp.Controllers
{
    [Authorize]
    public class CarrierController : Controller
    {
        //
        // GET: /Carrier/

        public ActionResult Index()
        {
            using (var bll = new CarrierBll())
            {
                IEnumerable<tbCarrier> list = bll.Find(t => t.Deleted == false).ToList();
                return View(list);
            }
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(tbCarrier model)
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
                model.Status = true;
                model.Deleted = false;
                using (var bll = new CarrierBll())
                {
                    bll.Insert(model);
                    bll.Save();
                }
                TempData["Exists"] = true;
                TempData["Message"] = string.Format("{0} successfully registered!", model.NickName);
                return RedirectToAction("Index");
            }
            catch (MyException ex)
            {
                TempData["Exists"] = true;
                TempData["Message"] = string.Format(ex.Message, model.NickName);
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Exists"] = true;
                TempData["Message"] = string.Format(ex.Message, model.NickName);
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var bll = new CarrierBll())
            {
                tbCarrier carrier = bll.Find(id);
                return View(carrier);
            }
        }

        [HttpPost]
        public ActionResult Edit(tbCarrier model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Exists"] = true;
                    var msg = string.Join("<br>", ModelState.Where(t => t.Value.Errors.Count > 0).Select(t => t.Value.Errors[0].ErrorMessage));
                    TempData["Message"] = string.Format(msg, model.NickName);
                    return View(model);
                } 


                using (var bll = new CarrierBll())
                {
                    bll.Update(model);
                    bll.Save();
                }
                TempData["Exists"] = true;
                TempData["Message"] = string.Format("{0} successfully updated!", model.NickName);
                return RedirectToAction("Index");
            }
            catch (MyException ex)
            {
                TempData["Exists"] = true;
                TempData["Message"] = string.Format(ex.Message, model.NickName);
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Exists"] = true;
                TempData["Message"] = string.Format(ex.Message, model.NickName);
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var bll = new CarrierBll())
            {
                tbCarrier carrier = bll.Find(id);
                bll.Delete(carrier);
                bll.Save();
                return RedirectToAction("Index");
            }
        }

    }
}
