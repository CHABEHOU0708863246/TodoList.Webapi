using Microsoft.AspNetCore.Identity;

namespace TodoList.Webapi.Authentification
{
    //Cette classe renvoi un message et un code d'état lorsque l'utilisateur se connecte ou s'inscrit à l'application
    public class Response : IdentityUser
    {
        public string Status { get; set; }

        public string Message { get; set; }
    }
}
