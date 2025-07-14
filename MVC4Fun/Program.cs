using log4net.Config;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.EntityFrameworkCore;
using MVC4Fun.Data;
using System.Globalization;
using System.Net.Mime;

XmlConfigurator.Configure(new FileInfo("log4net.config")); // 設定檔

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieContext") ?? throw new InvalidOperationException("Connection string 'MVC4FunContext' not found.")));

builder.Services.AddControllersWithViews()
    .AddDataAnnotationsLocalization()  /*給Models resources 用*/
    .AddViewLocalization();

builder.Services.AddLocalization(o => o.ResourcesPath = "Resources");

var app = builder.Build();

app.Environment.EnvironmentName = Environments.Production;

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePages(MediaTypeNames.Text.Html,"<h1>Status Code: {0}</h1>");
}

/* route provider語系設定*/
var supportedCultures = new CultureInfo[] {
new("en-US"),
new("zh-TW")
};

app.UseRequestLocalization(o => {
    o.DefaultRequestCulture = new("en-US");
    o.SupportedCultures = supportedCultures;
    o.SupportedUICultures = supportedCultures;
    o.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider());
});
app.UseStaticFiles();
app.MapControllerRoute("culture", "{culture=en-us}/{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();

app.Run();
