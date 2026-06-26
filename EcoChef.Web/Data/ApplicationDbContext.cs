using Microsoft.EntityFrameworkCore; //traducerea SQL-DB
using EcoChef.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EcoChef.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Ingredient> Ingrediente {  get; set; } 

        public DbSet<Reteta> Retete { get; set; }
        public DbSet<IngredientReteta> IngredientRetete { get; set; }

        public DbSet<Pierdere> Pierderi { get; set; }
        
        public DbSet<Setari> Setari { get; set; }
        public DbSet<IstoricGatire> IstoricGatire { get; set; }
    }
}
