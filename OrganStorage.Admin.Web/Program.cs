using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Services;
using OrganStorage.DAL.Consts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddScoped<IEnvironmentProvider, EnvironmentProvider>();

builder.Services.AddDbContext<AppDbContext>(opts =>
{
	opts.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddCoreAdmin();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.Use(async (context, next) =>
{
	var serviceProvider = context.RequestServices;
	EnvironmentSetter.SetUser(new() { Id = Consts.ADMIN_ID }, serviceProvider);
	await next.Invoke();
});

app.MapRazorPages();

app.MapDefaultControllerRoute();

app.Run();
