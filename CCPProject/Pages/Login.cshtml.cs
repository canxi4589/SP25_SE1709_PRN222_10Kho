namespace CCPProject.Pages
{
    using CCP.Repositori.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Security.Claims;

    public class LoginProcessModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginProcessModel(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public string ErrorMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(string email, string password, bool rememberMe)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ErrorMessage = "Invalid input.";
                return Redirect("/signin");
            }

            var user = await _signInManager.UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                ErrorMessage = "Invalid email or password.";
                return Redirect("/signin");
            }

            // Check password manually
            var passwordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!passwordValid)
            {
                ErrorMessage = "Invalid email or password.";
               return Redirect("/signin");
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email ?? ""),
                    new Claim("LastLogin", user.LastLogin?.ToString() ?? ""),
                    new Claim(ClaimTypes.Sid, user.Id),
                    new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString())
                };

            var claimsIdentity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await _signInManager.SignInAsync(user, rememberMe);

            user.LastLogin = DateTime.Now;
            await _userManager.UpdateAsync(user);

            // Redirect based on role
            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return Redirect("/admin-dashboard");
            }

            return Redirect("/");
        }
    }
}