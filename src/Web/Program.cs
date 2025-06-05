using ApplicationCore.Entities.OrderAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Logging;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.Services;
using Web.Services;
using Microsoft.eShopWeb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add Entity Framework services and specify an in-memory database for dev/testing
builder.Services.AddDbContext<CatalogContext>(options =>
{
    try
    {
        options.UseInMemoryDatabase("Microsoft.eShopOnWeb.CatalogDb");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while configuring the in-memory database: {ex.Message}");
        throw;
    }
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

// Add application services
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICatalogService, CachedCatalogService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<CatalogService>();
builder.Services.Configure<CatalogSettings>(builder.Configuration);
builder.Services.AddSingleton<IUriComposer>(new UriComposer(builder.Configuration.Get<CatalogSettings>()));

builder.Services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

// Configure MVC with endpoint routing enabled
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
app.UseMvc();

app.Run("http://0.0.0.0:5106");
