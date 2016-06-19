using System;
using System.Web;
using System.Web.Security;

namespace Suporte
{
    public static class LoginHelper
    {
        public static void SetAuthentication(string name, bool persistentCookie, string userData)
        {
            FormsAuthentication.SetAuthCookie(name, persistentCookie);
            var ticket = new FormsAuthenticationTicket(1, name, DateTime.Now, DateTime.Now.AddDays(1), false, userData ?? string.Empty);
            string encTicket = FormsAuthentication.Encrypt(ticket);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Clear();
        }
    }
}