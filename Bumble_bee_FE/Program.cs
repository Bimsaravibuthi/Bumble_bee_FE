var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("CookieAuth")
.AddCookie("CookieAuth", options =>
{
    options.LoginPath = "/Login/Login";
    options.Cookie.Name = "CookieAuth";
    options.AccessDeniedPath = "/Home/AccessDenied";
    options.SlidingExpiration = true;
    //options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminUsersOnly", policy => policy
       .RequireClaim("LOGGED", "True")
       );

    //options.AddPolicy("SuperAdminOnly", policy => policy
    //   .RequireClaim("User_Email")
    //   .RequireClaim("NormalUser", "True")
    //   .RequireClaim("Admin", "True")
    //   );
});

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseCookiePolicy();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Dashboard}/{id?}");

app.Run();
