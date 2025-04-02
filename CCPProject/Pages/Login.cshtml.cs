namespace CCPProject.Pages
{
    using CCP.Repositori.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class LoginProcessModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginProcessModel(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public string ErrorMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(string email, string password, bool rememberMe)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ErrorMessage = "Invalid input.";
                return RedirectToPage("/Signin", new { error = ErrorMessage });
            }

            var user = await _signInManager.UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                ErrorMessage = "Invalid email or password.";
                return RedirectToPage("/Signin", new { error = ErrorMessage });
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, rememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                user.LastLogin = DateTime.Now;
                await _signInManager.UserManager.UpdateAsync(user);
                return Redirect("/");
            }
            else if (result.IsLockedOut)
            {
                ErrorMessage = "Your account is locked out. Please try again later or contact support.";
            }
            else if (result.RequiresTwoFactor)
            {
                ErrorMessage = "Two-factor authentication is required. Please complete the 2FA process.";
            }
            else if (result.IsNotAllowed)
            {
                ErrorMessage = "Sign-in is not allowed. Please verify your email or contact support.";
            }
            else
            {
                ErrorMessage = "Invalid email or password.";
            }

            return RedirectToPage("/Signin", new { error = ErrorMessage });
        }
    }
}