using Microsoft.EntityFrameworkCore; //pt traducerea SQL-DB
using EcoChef.Web.Models; //aducem ce avem in Models
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EcoChef.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext //clasa creata ApplicationDbContext mosteneste DbContext de la Microsoft
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //totul vine din Program.cs
        }
        public DbSet<Ingredient> Ingrediente {  get; set; } //DbSet creeaza tabelul din BD numit "Ingrediente" iar coloanele+regulile sunt luate din clasa "Ingredient" 

        public DbSet<Reteta> Retete { get; set; }
        public DbSet<IngredientReteta> IngredientRetete { get; set; }

        public DbSet<Pierdere> Pierderi { get; set; }
        
        public DbSet<Setari> Setari { get; set; }
        public DbSet<IstoricGatire> IstoricGatire { get; set; }
    }
}
