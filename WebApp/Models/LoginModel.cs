using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Inform yout user")]
        public string User { get; set; }

        [Required(ErrorMessage = "Inform your password")]
        public string Password { get; set; }
    }
}