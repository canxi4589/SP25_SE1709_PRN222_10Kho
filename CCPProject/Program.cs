using CCP.Repositori.Repository;
using CCP.Service;
using CCP.Repositori.Repository;
using CCP.Service.AppointmentService;
using CCP.Service.EmailService;
using CCP.Service.ExpertService;
using CCPProject.Components;
using CCPProject.Extension;
using HCP.Repository.DatabaseExtension;
using MudBlazor.Services;
using CCP.Service.Integration.BlobStorage;
using CCP.Service.BackgroundServices;


var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpClient();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
builder.Services.AddScoped<IMeasurementService, MeasurementService>();
builder.Services.AddScoped<IExpertService, ExpertService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
builder.Services.AddBlobService(config);
builder.Services.AddDatabaseConfig(config);
builder.Services.AddIdentityService(config);
builder.Services.AddHostedService<AppointmentStatusUpdateService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStatusCodePagesWithRedirects("/error1?code={0}");
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorPages();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
await app.AddAutoMigrateAndSeedDatabase();

app.Run();


