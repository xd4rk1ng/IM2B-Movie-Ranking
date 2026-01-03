using context;
using context.Repositories;
using context.Seeders;
using context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using shared.Interfaces;
using shared.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar Entity Framework e SQL Server
string? connectionString = builder.Configuration.GetConnectionString("SergioConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("context")));

builder.Services.AddScoped<IGenericRepository<Filme>, FilmeRepository>();
builder.Services.AddScoped<IGenericRepository<Ator>, AtorRepository>();
builder.Services.AddScoped<IGenericRepository<Papel>, PapelRepository>();

// Configurar Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Configurações de senha
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    // Configurações de bloqueio
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Configurações de utilizador
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationContext>()
.AddDefaultTokenProviders();

// Configurar autenticação por cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(2);
});

var app = builder.Build();

// Fazer seeding da base de dados com filmes e atores aleatorios
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    var seeder = new Seeder(db);
    seeder.Seed(); // generate default number random films
    //seeder.Seed(5); // generate specified number random films
}

// Criar roles ao iniciar a aplicação
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await EnsureRolesAsync(roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao criar roles.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
{
    var roles = new[] { "Curador", "Utilizador" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
