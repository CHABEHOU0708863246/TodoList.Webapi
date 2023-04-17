using System.ComponentModel.DataAnnotations;

namespace TodoList.Webapi.Models
{
    public class LoginModel
    {
        // Cette classe contient les propriétés de l'utilisateur qui se connecte à l'application
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } //Le nom d'utilisateur

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, " +
            "one number and one special character")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } //Le mot de passe
    }
}
