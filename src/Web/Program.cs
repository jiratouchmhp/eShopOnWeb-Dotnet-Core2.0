using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Identity;
using ApplicationCore.Interfaces;
using Infrastructure.Logging;
using Microsoft.eShopWeb.Services;
using Web.Services;
using ApplicationCore.Services;
using Infrastructure.Services;
using Microsoft.eShopWeb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<CatalogContext>(c =>
{
    c.UseInMemoryDatabase("Catalog");
});

// Add Identity DbContext
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseInMemoryDatabase("Identity"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.LoginPath = "/Account/Signin";
    options.LogoutPath = "/Account/Signout";
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICatalogService, CachedCatalogService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<CatalogService>();
builder.Services.Configure<CatalogSettings>(builder.Configuration);
builder.Services.AddSingleton<IUriComposer>(new UriComposer(builder.Configuration.Get<CatalogSettings>()!));

builder.Services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

// Add MVC services
builder.Services.AddControllersWithViews();

// Add support for serving Blazor WebAssembly
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Catalog/Error");
}

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Index}/{id?}");

// Map fallback to Blazor app for admin routes
app.MapFallbackToFile("/admin/{*path:nonfile}", "index.html");
app.MapRazorPages();

app.Run();
