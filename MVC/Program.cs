using BLL.DAL;
using BLL.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// IOC container
var connectionString = "Server=127.0.0.1;Port=5432;Database=movie_app;User Id=furkanadiguzel;Password=123Frkn;";
builder.Services.AddDbContext<Db>(options => options.UseNpgsql(connectionString));

// Register services
// builder.Services.AddScoped<IGenreServices, GenreServices>();
builder.Services.AddScoped<ImovieService, movieservices>(); // Register IMovieServices
builder.Services.AddScoped<IDirectorService, DirectorService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();