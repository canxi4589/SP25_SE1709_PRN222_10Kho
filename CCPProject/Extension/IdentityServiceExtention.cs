using CCP.Repositori.Data;
using CCP.Repositori.Entities;
using Microsoft.AspNetCore.Identity;

namespace CCPProject.Extension
{
    public static class IdentityServiceExtention
    {
        public static IServiceCollection AddIdentityService(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            // Đăng ký các dịch vụ của Identity
            services
                .AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Truy cập IdentityOptions
            services.Configure<IdentityOptions>(options =>
            {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true; // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true; // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false; // Xác thực số điện thoại
            });

            //// Cấu hình Cookie
            services.ConfigureApplicationCookie(options =>
            {
                // options.Cookie.HttpOnly = true;
                // options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = $"/login/";
                options.LogoutPath = $"/logout/";
                options.AccessDeniedPath = $"/AccessDenied";
            });
            //services.Configure<SecurityStampValidatorOptions>(options =>
            //{
            //    // Trên 5 giây truy cập lại sẽ nạp lại thông tin User (Role)
            //    // SecurityStamp trong bảng User đổi -> nạp lại thông tinn Security
            //    options.ValidationInterval = TimeSpan.FromSeconds(5);
            //});

            //services
            //    .AddAuthentication()
            //    .AddGoogle(googleOptions =>
            //    {
            //        // Đọc thông tin Authentication:Google từ appsettings.json
            //        IConfigurationSection googleAuthNSection = config.GetSection(
            //            "Authentication:Google"
            //        );
            //        // Thiết lập ClientID và ClientSecret để truy cập API google
            //        googleOptions.ClientId = googleAuthNSection["ClientId"]!;
            //        googleOptions.ClientSecret = googleAuthNSection["ClientSecret"]!;
            //        // Cấu hình Url callback lại từ Google (không thiết lập thì mặc định là /signin-google)
            //        googleOptions.CallbackPath = "/dang-nhap-tu-google";
            //        googleOptions.ClaimActions.MapJsonKey("image", "picture");
            //    });

            return services;
        }
    }
}
