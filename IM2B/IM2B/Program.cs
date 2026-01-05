using context;
using context.Repositories;
using context.Seeders;
using context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using shared.Interfaces;
using shared.Models;

static string ConnectionSelector()
{
    while (true)
    {
        Console.WriteLine("##################################");
        Console.WriteLine("Select database connection string:\n");
        Console.WriteLine("1) Container DB");
        Console.WriteLine("2) Sergio DB");
        Console.WriteLine("3) Talita DB");
        Console.WriteLine();
        Console.Write("\nChoice: ");
        string? input = Console.ReadLine();

        switch (input)
        {
            case "1":
                return "ContainerConnection";
            case "2":
                return "SergioConnection";
            case "3":
                return "TalitaConnection";
            default:
                Console.WriteLine("\nInvalid option. Press any key...");
                Console.ReadKey();
                break;
        }
    }
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



// Configurar Entity Framework e SQL Server
//string? connectionString = builder.Configuration.GetConnectionString(ConnectionSelector());
string? connectionString = builder.Configuration.GetConnectionString(ConnectionSelector());
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("context")));

builder.Services.AddScoped<IGenericRepository<Filme>, FilmeRepository>();
builder.Services.AddScoped<IGenericRepository<Ator>, AtorRepository>();
builder.Services.AddScoped<IPapelRepository<Papel>, PapelRepository>();

// Configurar Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Configura��es de senha
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    // Configura��es de bloqueio
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Configura��es de utilizador
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationContext>()
.AddDefaultTokenProviders();

// Configurar autentica��o por cookie
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

    try
    {
        var seeder = new Seeder(db);
        await seeder.SeedContentsAsync(); // generate default number random films
    }
    catch(Exception ex)
    {
        Console.WriteLine("Erro ao popular base de dados: " + ex.Message);
    }
    //seeder.SeedContentsAsync(5); // generate specified number random films
}

// Criar roles ao iniciar a aplica��o
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await CuradorSeeder.Seed(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao criar roles: " + ex.Message);
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
