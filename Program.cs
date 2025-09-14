using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LagerverwaltungApp.Data;
using LagerverwaltungApp.Models;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Diagnostics;

namespace LagerverwaltungApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          
            var supportedCultures = new[] { new CultureInfo("de-DE") };
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("de-DE");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            // Verbindungszeichenfolge und DbContext
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null)));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Identity-Setup mit Rollen
            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
                    options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI();

            // Autorisierung
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));

                options.AddPolicy("ManagerOnly", policy =>
                    policy.RequireRole("Manager", "Admin"));

                options.AddPolicy("LageristOnly", policy =>
                    policy.RequireRole("Lagerist", "Admin"));
            });


            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Artikel", "AdminOnly");
                options.Conventions.AuthorizeFolder("/Bestellungen", "ManagerOnly");    // Beispiel
                options.Conventions.AuthorizeFolder("/Warenausgaenge", "LageristOnly");
                options.Conventions.AuthorizeFolder("/Kategorien", "ManagerOnly"); // Oder AdminOnly

            });

            // Auth-Cookie konfigurieren
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            var app = builder.Build();

            // Rollen und Admin-User erstellen
            // Rollen und Admin-User erstellen
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string[] roles = new[] { "Admin", "Lagerist", "Manager" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // Admin-Benutzer anlegen
                string adminEmail = "admin@lager.local";
                string adminPassword = "Admin123!";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(adminUser, adminPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }
                else
                {
                    if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }

                // Manager-Benutzer anlegen
                string managerEmail = "manager@lager.local";
                string managerPassword = "Manager123!";

                var managerUser = await userManager.FindByEmailAsync(managerEmail);
                if (managerUser == null)
                {
                    managerUser = new ApplicationUser
                    {
                        UserName = managerEmail,
                        Email = managerEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(managerUser, managerPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(managerUser, "Manager");
                        Console.WriteLine($"Manager-Benutzer '{managerEmail}' wurde erstellt.");
                    }
                }
                else
                {
                    if (!await userManager.IsInRoleAsync(managerUser, "Manager"))
                    {
                        await userManager.AddToRoleAsync(managerUser, "Manager");
                        Console.WriteLine($"Benutzer '{managerEmail}' wurde zur Rolle 'Manager' hinzugefügt.");
                    }
                }

                // Lagerist-Benutzer anlegen
                string lageristEmail = "lagerist@lager.local";
                string lageristPassword = "Lagerist123!";

                var lageristUser = await userManager.FindByEmailAsync(lageristEmail);
                if (lageristUser == null)
                {
                    lageristUser = new ApplicationUser
                    {
                        UserName = lageristEmail,
                        Email = lageristEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(lageristUser, lageristPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(lageristUser, "Lagerist");
                        Console.WriteLine($"Lagerist-Benutzer '{lageristEmail}' wurde erstellt.");
                    }
                }
                else
                {
                    if (!await userManager.IsInRoleAsync(lageristUser, "Lagerist"))
                    {
                        await userManager.AddToRoleAsync(lageristUser, "Lagerist");
                        Console.WriteLine($"Benutzer '{lageristEmail}' wurde zur Rolle 'Lagerist' hinzugefügt.");
                    }
                }
            }
            var localizationOptions = app.Services.GetRequiredService<Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>().Value;
            app.UseRequestLocalization(localizationOptions);
            // HTTP-Pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRequestLocalization(); // ← Wichtig!

            app.MapRazorPages();
            //  app.MapFallbackToPage("/ArtikelVerwaltung/Index"); // <- hinzufügen oder ersetzen
            // Routing für MVC-Controller aktivieren
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Startseite weiterleiten zu /Artikel
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/Artikel");
                return Task.CompletedTask;
            });

            app.MapControllers();
         
            app.Run();
            // 🔽 HIER: Nach dem Start öffne Browser automatisch
            try
            {
                var url = builder.Configuration["Urls"] ?? "http://localhost:5000";
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch
            {
                // Fehler ignorieren (z. B. bei Linux/Server)
            }
        }
    }
}
