using clothing_store.Interfaces;
using clothing_store.Interfaces.clothing_store.Interfaces;
using clothing_store.Models;
using clothing_store.Repositories;
using clothing_store.Repositories.clothing_store.Repositories;
using clothing_store.Services;
using clothing_store.Services.clothing_store.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddScoped<IUserEventService, UserEventService>();

builder.Services.AddTransient<IBasketService, BasketService>();
builder.Services.AddTransient<IDiscountService, DiscountService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddTransient<ITaxService, TaxService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IBrandService, BrandService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IPDFService, PDFService>();
builder.Services.AddTransient<INotificationService, NotificationService>();

builder.Services.AddHttpContextAccessor();

// Adding repositories as Transient
builder.Services.AddTransient<IUserEventRepository, UserEventRepository>();
builder.Services.AddTransient<INotificationRepository, NotificationRepository>();
builder.Services.AddTransient<ITaxRepository, TaxRepository>();
builder.Services.AddTransient<IDiscountRepository, DiscountRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddTransient<IBasketRepository,BasketRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IAddressRepository, AddressRepository>();

builder.Services.AddSingleton<IHostedService, CurrencyUpdaterService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.AccessDeniedPath = "/Account/AccessDenied";
            });
builder.Services.AddDbContext<StoreDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30); // Czas trwania sesji
        options.Cookie.HttpOnly = true; // Ochrona przed XSS
        options.Cookie.IsEssential = true; // Wymagane w przypadku polityk RODO
    });



var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
    var seeder = new CurrencySeeder(context);
    seeder.Seed();
    var shippingSeeder = new ShippingMethodSeeder(context);
    await shippingSeeder.SeedAsync();

    var paymentSeeder = new PaymentMethodSeeder(context);
    await paymentSeeder.SeedAsync();
}

app.UseSession();
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

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<VisitLoggingMiddleware>();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
