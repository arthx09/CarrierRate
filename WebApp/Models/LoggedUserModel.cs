using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public static class LoggedUserModel
    {
        public static int? idUser {
            get
            {
                try
                {
                    return int.Parse(HttpContext.Current.User.Identity.Name.Split('|')[0]);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}