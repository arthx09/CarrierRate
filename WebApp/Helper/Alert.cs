using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Helper
{
    public class Alert : Controller
    {
        public enum TypeErroMessage
        {
            danger,
            warning,
            success,
            info
        }

        public enum TypeMessageBox
        {
            Fixed,
            NonFixed
        }

        public JsonResult Show(bool Result, ModelStateDictionary ModelState, bool msgFixed)
        {
            var msg = string.Join("<br>", ModelState.Where(t => t.Value.Errors.Count > 0).Select(t => t.Value.Errors[0].ErrorMessage));
            return Json(new
            {
                result = Result,
                message = msg,
                typemessage = TypeErroMessage.danger.ToString(),
                messageisfixed = true,
            });
        }

        public JsonResult Show(bool Result, string Message, TypeErroMessage TypeMessageErro, TypeMessageBox TypeMessage)
        {
            return Json(new
            {
                result = Result,
                message = Message,
                typemessage = TypeMessageErro.ToString(),
                messageisfixed = TypeMessage == TypeMessageBox.Fixed
            });
        }
    }
}