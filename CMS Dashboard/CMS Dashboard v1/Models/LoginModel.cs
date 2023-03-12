using System.ComponentModel.DataAnnotations;

namespace CMS_Dashboard_v1.Models
{
    public class LoginModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username tidak boleh kosong")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password tidak boleh kosong")]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

    }
}
