using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
// github 2
 /* Yetkilendirme ayarları */
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
});

/* Oturum ayarları */
builder.Services.AddSession(options =>
    {
        // Oturum yapılandırması
        options.IdleTimeout = TimeSpan.FromMinutes(20);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.Cookie.Name = "CookieSessionV1";
    });

/* Kimlik doğrulama ayarları */
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        
    })
    .AddCookie("Cookies", options =>
    {
        options.Cookie.Name = "user";
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Home/Index";
        options.AccessDeniedPath = "/Home/Index";
        options.ExpireTimeSpan=TimeSpan.FromDays(2);

    });


var app = builder.Build();
app.UseSession();  // Hafızada yer ac etkinleştir
app.UseAuthentication();
app.UseAuthorization();

 

app.UseStaticFiles();
app.UseStatusCodePagesWithReExecute("/Error/Error", "?statusCode={0}");



app.MapDefaultControllerRoute();
app.Run();