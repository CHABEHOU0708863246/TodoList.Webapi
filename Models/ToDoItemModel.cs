using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Webapi.Models
{
    // Cette classe contient les propriétés de l'item à ajouter à la liste
    public class ToDoItemModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "ItemName is required")]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }

        [Required(ErrorMessage = "ItemDescription is required")]
        [Column(TypeName = "nvarchar(100)")]
        public string Description { get; set; }

        [Required(ErrorMessage = "ItemStatus is required")]
        [Column(TypeName = "nvarchar(100)")]
        public string Status { get; set; }
    }
}
