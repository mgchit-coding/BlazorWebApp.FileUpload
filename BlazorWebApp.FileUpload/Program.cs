using BlazorWebApp.FileUpload.Components;
using BlazorWebApp.FileUpload.Model;
using BlazorWebApp.FileUpload.Services;
using DotNet8.DbService.Models;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
}, ServiceLifetime.Transient, ServiceLifetime.Transient);
var builderCustomSetting = new ConfigurationBuilder();
builderCustomSetting
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("customsetting.json", optional: false, reloadOnChange:true);
IConfiguration configCustomSetting = builderCustomSetting.Build();

builder.Services.Configure<CustomSettingModel>(configCustomSetting.GetSection("CustomSetting"));

builder.Services.AddScoped<FileUploadService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
