using DB;
using Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<CoinDeskService>();
builder.Services.AddSingleton<DBService>();
builder.Services.AddScoped<CNBService>();
builder.Services.AddSingleton<AppDbContext>(serviceProvider => new AppDbContext(builder.Configuration["DefaultConnection:AppDbConnectionString"]));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LiveData}/{action=Index}/{id?}");

app.Run();
