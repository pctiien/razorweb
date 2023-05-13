using Microsoft.EntityFrameworkCore;
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

    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();
}
static void ConfigureServices(WebApplicationBuilder builder)
{
    var services = builder.Services;
    services.AddRazorPages();
    services.AddOptions();
    services.AddDbContext<MyBlogContext>(
        options=>{
            string connectionString = builder.Configuration.GetConnectionString("MySqlDatabase");
            options.UseMySQL(connectionString);
        }
    );

}
