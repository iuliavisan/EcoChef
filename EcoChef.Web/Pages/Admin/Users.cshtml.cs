using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace EcoChef.Web.Pages.Admin
{
    [Authorize(Policy = "Admin")]
    public class UsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<IdentityUser> Utilizatori { get; set; } = new();
        public Dictionary<string, string> RoluriUtilizator { get; set; } = new();

        [BindProperty]
        public string Email { get; set; } = "";

        [BindProperty]
        public string Parola { get; set; } = "";

        [BindProperty]
        public string Rol { get; set; } = "";


        public async Task OnGetAsync()
        {
            Utilizatori = _userManager.Users.ToList();
            foreach (var user in Utilizatori)
            {
                var roluri = await _userManager.GetRolesAsync(user);
                RoluriUtilizator[user.Id] = roluri.FirstOrDefault() ?? "Fără rol";

            }
        }

        public async Task <IActionResult> OnPostAsync()
        {
            var user = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = Email,
                Email = Email
            };
            var result = await _userManager.CreateAsync(user, Parola);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Rol);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                Utilizatori = _userManager.Users.ToList();
                return Page();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostStergeAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToPage();
        }
    }
}

