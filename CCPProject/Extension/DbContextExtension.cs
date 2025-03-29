using CCP.Repositori.Data;
using CCP.Repositori.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HCP.Repository.DatabaseExtension
{
    public static class DbContextExtension
    {
        public static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        config.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("CCP.Repositori")
    )
);

            return services;
        }

        public static async Task<WebApplication> AddAutoMigrateAndSeedDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            try
            {
                var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

                await SeedRolesAndUsersAsync(roleManager, userManager, context);
            }
            catch (Exception ex)
            {
                throw new Exception("KeyConst.Exception", ex);
            }

            return app;
        }

        private static async Task SeedRolesAndUsersAsync(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            // Seed Roles
            string[] roles = { "Admin", "Parent", "Expert" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed Admin User
            var adminEmail = "admin@hcp.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new AppUser
                {
                    FullName = "System Admin",
                    Email = adminEmail,
                    NormalizedEmail = adminEmail.ToUpper(),
                    UserName = "AdminUser",
                    NormalizedUserName = "ADMINUSER",
                    IsActive = true,
                    RegistrationDate = DateTime.UtcNow,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            // Seed Parent Users
            var parents = new List<(string FullName, string Email, string UserName)>
            {
                ("Parent One", "parent1@hcp.com", "ParentOne"),
                ("Parent Two", "parent2@hcp.com", "ParentTwo")
            };

            foreach (var (fullName, email, userName) in parents)
            {
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var parent = new AppUser
                    {
                        FullName = fullName,
                        Email = email,
                        NormalizedEmail = email.ToUpper(),
                        UserName = userName,
                        NormalizedUserName = userName.ToUpper(),
                        IsActive = true,
                        RegistrationDate = DateTime.UtcNow.AddDays(-10),
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true
                    };

                    var result = await userManager.CreateAsync(parent, "Parent@123");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(parent, "Parent");
                    }
                }
            }

            // Seed Specialty if empty
            var specialty = context.Specialties.FirstOrDefault();
            if (specialty == null)
            {
                specialty = new Specialty
                {
                    Id = Guid.NewGuid(),
                    Name = "Child Nutrition"
                };
                context.Specialties.Add(specialty);
                await context.SaveChangesAsync();
            }

            // Seed Expert Users
            var experts = new List<(string FullName, string Email, string UserName)>
            {
                ("Expert One", "expert1@hcp.com", "ExpertOne"),
                ("Expert Two", "expert2@hcp.com", "ExpertTwo")
            };

            foreach (var (fullName, email, userName) in experts)
            {
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var expertUser = new AppUser
                    {
                        FullName = fullName,
                        Email = email,
                        NormalizedEmail = email.ToUpper(),
                        UserName = userName,
                        NormalizedUserName = userName.ToUpper(),
                        IsActive = true,
                        RegistrationDate = DateTime.UtcNow.AddDays(-20),
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true
                    };

                    var result = await userManager.CreateAsync(expertUser, "Expert@123");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(expertUser, "Expert");

                        var expert = new Expert
                        {
                            Name = fullName,
                            UserId = expertUser.Id,
                            SpecialtyId = specialty.Id,
                            ContactInfo = "123-456-7890",
                            Certificate = "Certified Childcare Expert",
                            Price = 50.00M,
                        };
                        context.Experts.Add(expert);
                    }
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
