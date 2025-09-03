using edu_for_you.Models;
using edu_for_you.Policies.Handlers;
using edu_for_you.Policies.Requirements;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Professor", policy =>
        policy.RequireClaim("IsProfessor", "true"));
});

builder.Services.AddSingleton<IAuthorizationHandler, ProfessorHandler>();

// --------------------- MANUALMENTE

builder.Services.AddRazorPages().AddRazorRuntimeCompilation(); // Live Server

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    if (builder.Environment.IsDevelopment())
    {
        options.UseAsyncSeeding(async (context, _, _) => await AppDbContext.SeedTestData(context));
        options.UseSeeding((context, _) => AppDbContext.SeedTestData(context).GetAwaiter().GetResult());
    }
});

// --------------------- MANUALMENTE FIM

// --------------------- COOKIE AUTHENTICATION 

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/Usuario/AccessDenied/";
        options.LoginPath = "/Usuario/Login/";

        // ?? força o cookie ser renovado a cada request (útil quando claims são atualizados)
        options.Events = new CookieAuthenticationEvents
        {
            OnValidatePrincipal = context =>
            {
                context.ShouldRenew = true;
                return Task.CompletedTask;
            }
        };
    });


// --------------------- COOKIE AUTHENTICATION FIM

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
