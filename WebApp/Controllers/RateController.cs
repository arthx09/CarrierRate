using Business;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using System.Linq;
using System.Data.Entity;
using Support;

namespace WebApp.Controllers
{
    [Authorize]
    public class RateController : Controller
    {
        //
        // GET: /Rate/

        public ActionResult Index()
        {
            using (var bll = new RateBll())
            {
                var query = bll.Find(t => t.IdUser == LoggedUserModel.idUser).Include(t => t.tbUser).Include(t => t.tbCarrier);
                return View(query.ToList());
            }
        }

        [HttpGet]
        public ActionResult New()
        {
            using (var bll = new CarrierBll())
            {
                TempData["carriers"] = bll.Find(t => t.Deleted == false).OrderBy(t => t.NickName).Select(t => new SelectListItem { Text = t.NickName, Value = t.Id.ToString() }).ToList();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(tbRate model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    using (var bll = new CarrierBll())
                    {
                        TempData["carriers"] = bll.Find().OrderBy(t => t.NickName).Select(t => new SelectListItem { Text = t.NickName, Value = t.Id.ToString() }).ToList();
                        TempData["Exists"] = true;
                        var msg = string.Join("<br>", ModelState.Where(t => t.Value.Errors.Count > 0).Select(t => t.Value.Errors[0].ErrorMessage));
                        TempData["Message"] = msg;
                        return View(model);
                    }
                }
                model.IdUser = LoggedUserModel.idUser ?? 0;
                using (var bll = new RateBll())
                {
                    bll.Insert(model);
                    bll.Save();
                }
                TempData["Exists"] = true;
                TempData["Message"] = "Rate successfully registered!";
                return RedirectToAction("Index");
            }
            catch (MyException ex)
            {
                using (var bll = new CarrierBll())
                {
                    TempData["carriers"] = bll.Find().OrderBy(t => t.NickName).Select(t => new SelectListItem { Text = t.NickName, Value = t.Id.ToString() }).ToList();
                    TempData["Exists"] = true;
                    TempData["Message"] = ex.Message;
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                using (var bll = new CarrierBll())
                {
                    TempData["carriers"] = bll.Find().OrderBy(t => t.NickName).Select(t => new SelectListItem { Text = t.NickName, Value = t.Id.ToString() }).ToList();
                    TempData["Exists"] = true;
                    TempData["Message"] = ex.Message;
                    return View(model);
                }
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var bll = new RateBll())
            {
                var rate = bll.Find(t => t.Id == id).Include(t => t.tbCarrier).FirstOrDefault();
                TempData["carriers"] = new List<SelectListItem> { new SelectListItem { Value = rate.tbCarrier.Id.ToString(), Text = rate.tbCarrier.NickName}};
                return View(rate);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbRate model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    using (var bll = new CarrierBll())
                    {
                        TempData["carriers"] = bll.Find().OrderBy(t => t.NickName).Select(t => new SelectListItem { Text = t.NickName, Value = t.Id.ToString() }).ToList();
                        TempData["Exists"] = true;
                        var msg = string.Join("<br>", ModelState.Where(t => t.Value.Errors.Count > 0).Select(t => t.Value.Errors[0].ErrorMessage));
                        TempData["Message"] = msg;
                        return View(model);
                    }
                }
                model.IdUser = LoggedUserModel.idUser ?? 0;
                using (var bll = new RateBll())
                {
                    model.Rate = Convert.ToDecimal(model.Rate);
                    bll.Update(model);
                    bll.Save();
                }
                TempData["Exists"] = true;
                TempData["Message"] = "Rate successfully registered!";
                return RedirectToAction("Index");
            }
            catch (MyException ex)
            {
                using (var bll = new CarrierBll())
                {
                    TempData["carriers"] = bll.Find().OrderBy(t => t.NickName).Select(t => new SelectListItem { Text = t.NickName, Value = t.Id.ToString() }).ToList();
                    TempData["Exists"] = true;
                    TempData["Message"] = ex.Message;
                    return View(model);
                }
            }   
            catch (Exception ex)
            {
                using (var bll = new CarrierBll())
                {
                    TempData["carriers"] = bll.Find().OrderBy(t => t.NickName).Select(t => new SelectListItem { Text = t.NickName, Value = t.Id.ToString() }).ToList();
                    TempData["Exists"] = true;
                    TempData["Message"] = ex.Message;
                    return View(model);
                }
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var bll = new RateBll())
            {
                tbRate rate = bll.Find(id);
                bll.Delete(rate);
                bll.Save();
                return RedirectToAction("Index");
            }
        }
    }
}
