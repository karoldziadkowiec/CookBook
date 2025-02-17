using CookBook.DbManager;
using CookBook.Repositories.Classes;
using CookBook.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllersWithViews();

// MS SQL database connection
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("connection") ??
        throw new InvalidOperationException("MS SQL connection string is not found!"));
});

// services
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();

// AutoMapper service
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();