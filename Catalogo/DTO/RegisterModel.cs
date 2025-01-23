using System.ComponentModel.DataAnnotations;

namespace Catalogo.DTO
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

    }
}
