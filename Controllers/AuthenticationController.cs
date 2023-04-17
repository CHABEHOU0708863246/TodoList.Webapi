using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoList.Webapi.Authentification;
using TodoList.Webapi.Models;

namespace TodoList.Webapi.Controllers
{
    // Déclaration de la classe AuthenticationController qui hérite de ControllerBase et est utilisée pour la gestion de l'authentification
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        // Injection de UserManager pour gérer les utilisateurs
        private readonly UserManager<ApplicationUser> userManager;

        // Injection de RoleManager pour gérer les rôles
        private readonly RoleManager<IdentityRole> roleManager;

        // Injection de IConfiguration pour accéder aux informations de configuration de l'application
        private readonly IConfiguration _configuration;

        // Constructeur de la classe AuthenticationController
        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            // Initialisation de l'objet userManager
            this.userManager = userManager;

            // Initialisation de l'objet roleManager
            this.roleManager = roleManager;

            // Initialisation de l'objet _configuration
            _configuration = configuration;
        }

        // Déclaration de la méthode Login qui permet de se connecter à l'application
        [HttpPost]
        [Route("login")]

        // Déclaration de la méthode Login qui permet de se connecter à l'application
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Récupération de l'utilisateur à partir de son nom d'utilisateur
            var user = await userManager.FindByNameAsync(model.Username);

            // Vérification de l'existence de l'utilisateur
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                // Ajout des rôles de l'utilisateur
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                // Génération du token
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                // Création du token
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        // Déclaration de la méthode Register qui permet d'enregistrer un nouvel utilisateur
        [HttpPost]
        [Route("register")]

        // Déclaration de la méthode Register qui permet d'enregistrer un nouvel utilisateur
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // Vérification de l'existence de l'utilisateur
            var userExists = await userManager.FindByNameAsync(model.Username);

            // Si l'utilisateur existe déjà, on retourne une erreur
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            };

            // Création d'un nouvel utilisateur
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            // Création de l'utilisateur
            var result = await userManager.CreateAsync(user, model.Password);

            // Si la création de l'utilisateur a échoué, on retourne une erreur
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            // Si le rôle Admin n'existe pas, on le crée
            return Ok(new Response { Status = "Success", Message = "User created successfully" });
        }

        // Déclaration de la méthode RegisterAdmin qui permet d'enregistrer un nouvel administrateur
        [HttpPost]
        [Route("register-admin")]

        // Déclaration de la méthode RegisterAdmin qui permet d'enregistrer un nouvel administrateur
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            // Vérification de l'existence de l'utilisateur
            var userExists = await userManager.FindByNameAsync(model.Username);

            // Si l'utilisateur existe déjà, on retourne une erreur
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            };

            // Création d'un nouvel utilisateur
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            // Création de l'utilisateur
            var result = await userManager.CreateAsync(user, model.Password);

            // Si la création de l'utilisateur a échoué, on retourne une erreur
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            // Si le rôle Admin n'existe pas, on le crée
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                // Création du rôle Admin
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            // Si le rôle User n'existe pas, on le crée
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
            {
                // Création du rôle User
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            //  Ajout de l'utilisateur au rôle Admin
            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                // Ajout de l'utilisateur au rôle Admin
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            // Retourne une réponse de succès
            return Ok(new Response { Status = "Success", Message = "User created successfully" });
        }
    }
}
