using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using razorEntity.models;
using razorEntity.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder);



var app = builder.Build();


// Configure the HTTP request pipeline.
Configure(app);





static void Configure(WebApplication app)
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapRazorPages();

    IdentityUser user ;


    app.Run();
}
static void ConfigureServices(WebApplicationBuilder builder)
{
    var services = builder.Services;
    services.AddRazorPages();
    services.AddOptions();
     var mailSetting = builder.Configuration.GetSection("MailSettings");
    services.Configure<MailSettings>(mailSetting);
    services.AddSingleton<IEmailSender,SendMailService>();

    services.AddDbContext<MyBlogContext>(
        options=>{
            string connectionString = builder.Configuration.GetConnectionString("MySqlDatabase");
            options.UseMySQL(connectionString);
        }
    );
    services.AddIdentity<AppUser,IdentityRole>()
            .AddEntityFrameworkStores<MyBlogContext>()
            .AddDefaultTokenProviders();
    // services.AddDefaultIdentity<AppUser>()
    //         .AddEntityFrameworkStores<MyBlogContext>();
           // .AddDefaultTokenProviders();
    // Truy cập IdentityOptions
    services.Configure<IdentityOptions> (options => {
        // Thiết lập về Password
        options.Password.RequireDigit = false; // Không bắt phải có số
        options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
        options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
        options.Password.RequireUppercase = false; // Không bắt buộc chữ in
        options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
        options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

        // Cấu hình Lockout - khóa user
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes (5); // Khóa 5 phút
        options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
        options.Lockout.AllowedForNewUsers = true;

        // Cấu hình về User.
        options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;  // Email là duy nhất

        // Cấu hình đăng nhập.
        options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
        options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
        options.SignIn.RequireConfirmedAccount = true;
    });
    services.ConfigureApplicationCookie(options=>
    {
        options.LoginPath ="/login/";
        options.LogoutPath="/logout/";
        options.AccessDeniedPath ="/Identity/Account/AccessDenied";
    });
}
/*
    Identity :
        - Authentication : Xac dinh danh tinh - Login - Logout
        - Authorization : Xac thuc quyen truy cap
        - Quan li user : Sign up , User , Role ...
    Phat sinh code cho UI Dang nhap dang ky 
    dotnet aspnet-codegenerator identity -dc razorEntity.Models.MyBlogContext
*/
