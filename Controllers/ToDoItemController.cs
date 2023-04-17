using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Webapi.Data;
using TodoList.Webapi.Models;

namespace TodoList.Webapi.Controllers
{
    /*[Authorize]*/
    [Route("api/[controller]")]
    [ApiController]
    // Déclaration de la classe ToDoItemController qui hérite de ControllerBase et est utilisée pour la gestion des tâches
    public class ToDoItemController : ControllerBase
    {
        // Injection de ApplicationDbContext pour gérer les données de l'application
        private readonly ApplicationDbContext _context;

        // Constructeur de la classe ToDoItemController
        public ToDoItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDoItem
        [HttpGet]
        // Déclaration de la méthode GetToDoItems qui permet de récupérer la liste des tâches
        public async Task<ActionResult<IEnumerable<ToDoItemModel>>> GetToDoItems()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        // GET: api/ToDoItem/3
        [HttpGet("{id}")]
        // Déclaration de la méthode GetToDoItemModel qui permet de récupérer une tâche à partir de son identifiant
        public async Task<ActionResult<ToDoItemModel>> GetToDoItemModel(int id)
        {
            // Récupération de la tâche à partir de son identifiant
            var toDoItemModel = await _context.ToDoItems.FindAsync(id);

            // Vérification de l'existence de la tâche
            if (toDoItemModel == null)
            {
                return NotFound();
            }

            // Retour de la tâche
            return toDoItemModel;
        }

        // PUT: api/ToDoItem/3
        [HttpPut("{id}")]
        // Déclaration de la méthode PutToDoItemModel qui permet de mettre à jour une tâche
        public async Task<IActionResult> PutToDoItemModel(int id, ToDoItemModel toDoItemModel)
        {
            // Vérification de l'existence de la tâche
            if (id != toDoItemModel.Id)
            {
                // Retour d'une erreur 400
                return BadRequest();
            }

            // Modification de l'état de l'entité
            _context.Entry(toDoItemModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoItemModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Retour d'une réponse 204
            return NoContent();
        }

        // POST: api/ToDoItem
        [HttpPost]
        // Déclaration de la méthode PostToDoItemModel qui permet d'ajouter une tâche
        public async Task<ActionResult<ToDoItemModel>> PostToDoItemModel(ToDoItemModel toDoItemModel)
        {
            // Ajout de la tâche
            _context.ToDoItems.Add(toDoItemModel);

            // Sauvegarde des modifications
            await _context.SaveChangesAsync();

            // Retour de la tâche
            return CreatedAtAction("GetToDoItemModel", new { id = toDoItemModel.Id }, toDoItemModel);
        }

        // DELETE: api/ToDoItem/5
        [HttpDelete("{id}")]
        // Déclaration de la méthode DeleteToDoItemModel qui permet de supprimer une tâche
        public async Task<IActionResult> DeleteToDoItemModel(int id)
        {
            // Récupération de la tâche à partir de son identifiant
            var toDoItemModel = await _context.ToDoItems.FindAsync(id);

            // Vérification de l'existence de la tâche
            if (toDoItemModel == null)
            {
                return NotFound();
            }

            // Suppression de la tâche
            _context.ToDoItems.Remove(toDoItemModel);

            // Sauvegarde des modifications
            await _context.SaveChangesAsync();

            // Retour d'une réponse 204
            return NoContent();
        }

        // Déclaration de la méthode ToDoItemModelExists qui permet de vérifier l'existence d'une tâche
        private bool ToDoItemModelExists(int id)
        {
            // Retour de la vérification
            return _context.ToDoItems.Any(e => e.Id == id);
        }
    }
}
