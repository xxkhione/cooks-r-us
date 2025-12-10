using CooksRUs.Components;
using CooksRUs.Components.Cookie;
using CooksRUs.Database;
using CooksRUs.Services;
using Microsoft.EntityFrameworkCore;

namespace CooksRUs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddHttpClient("BackendApi", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5265/");
            });

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddDbContext<CooksRUsDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("CooksRUsContext")));
            builder.Services.AddScoped<LocalSaveService>();
            builder.Services.AddScoped<UserRecipeService>();
            builder.Services.AddScoped<ICookie, Cookie>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddLogging();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
