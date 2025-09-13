using AspNetCoreGeneratedDocument;
using Business.BrandServise;
using Business.CategoryServise;
using Business.FileUploudServise;
using Business.OrderService;
using Business.ProductServise;
using Business.SMSService;
using Business.UserService;
using DataAccess.Data;
using DataAccess.Models;
using DataAccess.Repositories.BrandRepository;
using DataAccess.Repositories.Categoriesrepository;
using DataAccess.Repositories.Categoryrepository;
using DataAccess.Repositories.OrderItemRepository;
using DataAccess.Repositories.OrderRepository;
using DataAccess.Repositories.ProductRepsitory;
using DataAccess.Repositories.UserRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.Configure<KavenegarInfoViewModel>(builder.Configuration.GetSection(key: "KavenegarInfo"));
builder.Services.AddScoped<ISMSService, SMSService>();

builder.Services.AddDbContext<LifitDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<BrandServise>();

builder.Services.AddScoped< ICategoryRepository, CategoriesRepository>();
builder.Services.AddScoped<CategoryServise>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductServise>();
builder.Services.AddScoped<IfileUploudServise, FileUploudServise>();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();
builder.Services.AddScoped<OrderService>();


builder.Services.AddIdentity<User, Role>(Options =>
{
    Options.Password.RequireDigit = false;
    Options.Password.RequireLowercase = false;
    Options.Password.RequireNonAlphanumeric = false;
    Options.Password.RequireUppercase = false;
    Options.Password.RequiredLength = 8;
    Options.Password.RequiredUniqueChars = 0;

    //LookOutSetting

    Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    Options.Lockout.MaxFailedAccessAttempts = 5;
    Options.Lockout.AllowedForNewUsers = true;
    Options.User.RequireUniqueEmail = true;



})
    .AddEntityFrameworkStores<LifitDbContext>()
    .AddSignInManager<SignInManager<User>>()
    .AddDefaultTokenProviders();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();



app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
