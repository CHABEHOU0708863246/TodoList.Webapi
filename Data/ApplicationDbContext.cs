using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoList.Webapi.Authentification;
using TodoList.Webapi.Models;

namespace TodoList.Webapi.Data
{
    // Cette classe va servir à mapper les modèles aux tables qui seront créées lors de la migration.
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=AUXENCE;Database=Todolist_Db;User Id=sa;Password=1234; Trusted_Connection=True; Encrypt=False;");
        }

        public DbSet<ToDoItemModel> ToDoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ToDoItemModel>(entity =>
            {
                entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100);
            });

            base.OnModelCreating(builder);
        }
    }
}
