using Microsoft.EntityFrameworkCore; //pt traducerea SQL-DB
using EcoChef.Web.Models; //aducem ce avem in Models

namespace EcoChef.Web.Data
{
    public class ApplicationDbContext : DbContext //clasa creata ApplicationDbContext mosteneste DbContext de la Microsoft
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Ingredient> Ingrediente {  get; set; } //DbSet creeaza tabelul din BD numit "Ingrediente" iar coloanele+regulile sunt luate din clasa "Ingredient" 

        public DbSet<Reteta> Retete { get; set; }
        public DbSet<IngredientReteta> IngredientRetete { get; set; }
    }
}
