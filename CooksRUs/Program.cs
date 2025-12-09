using CooksRUs.Components;
using CooksRUs.Components.Cookie;
using CooksRUs.Database;
using Microsoft.EntityFrameworkCore;

namespace CooksRUs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            //builder.Services.AddDbContext<CooksRUsDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("CooksRUsContext")));
            builder.Services.AddScoped<ICookie, Cookie>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();
            builder.Services.AddLogging();

            builder.Services.AddDbContextFactory<CooksRUsDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CooksRUsContext")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseAntiforgery();
            app.UseHttpsRedirection();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
