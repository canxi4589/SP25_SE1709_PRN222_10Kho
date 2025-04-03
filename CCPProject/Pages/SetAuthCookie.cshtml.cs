namespace CCPProject.Pages
{
    using CCP.Repositori.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class SetAuthCookieModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;

        public SetAuthCookieModel(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("/login");
            }

            var user = await _signInManager.UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Redirect("/login");
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            return Redirect("/");
        }
    }
}