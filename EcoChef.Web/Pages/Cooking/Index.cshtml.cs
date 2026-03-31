using EcoChef.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EcoChef.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoChef.Web.Pages.Cooking
{
    //punctul de la metode inlantuite=aplica pe ce e inainte
    //: inseamna mostenire
    // => inseamna pentru care/astfel incat
    public class IndexModel : PageModel //defineste clasa aici pentru ca e un model de pagina, nu de BD
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Reteta> Retete { get; set; } = new List<Reteta>(); //creeaza o lista Reteta din Retete
        public void OnGet()//fara argumente deoarece afiseaza toate retetele
        {
            Retete = _context.Retete.ToList(); //pui elementele din BD din Retete(tabelul din context) in lista creata
        }

        //onpost primeste datele dupa ce se apasa submit
        public IActionResult OnPost(int RetetaId, int NrPortii) //IActionResult penru ca rerneaza o actiune
        {
            Retete = _context.Retete.ToList();//e iar pt ca lista trebuie reincarcata
            
            var ingrediente = _context.IngredientRetete //tabelul IngredientRetete din BD
                .Where(rand => rand.RetetaId == RetetaId)
                .Include(rand => rand.Ingredient)
                .ToList();

            //primul foreach doar verifica daca ai
            foreach(var ingredient_reteta in ingrediente)
            {
                //pentru fiecare ingredient
                var cantitateTotal = ingredient_reteta.CantitateNecesara * NrPortii;

                if(ingredient_reteta.Ingredient.StocCurent < cantitateTotal)
                {
                    //mesaj de eroare
                    ModelState.AddModelError("",
                        //string Sinterpolation
                        $"Stoc insuficient pentru {ingredient_reteta.Ingredient.Nume}. " +
                        $"Ai {ingredient_reteta.Ingredient.StocCurent} în stoc, îți trebuie {cantitateTotal}.");
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            //al doilea foreach scade din stoc daca SUNT toate ingredientele
            foreach(var ingredient_reteta in ingrediente)
            {
                var cantitateTotal = ingredient_reteta.CantitateNecesara * NrPortii;
                ingredient_reteta.Ingredient.StocCurent -= cantitateTotal;
            }

            _context.SaveChanges();

            return Page(); //ramai pe aceeasi pagina
        }
    }
}
