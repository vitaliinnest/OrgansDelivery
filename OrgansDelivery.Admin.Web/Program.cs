using OrganStorage.BL.Extensions;
using OrganStorage.DAL.Extensions;
using OrganStorage.Web.Common.Extensions;
using OrganStorage.Web.Common.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDAL(builder.Configuration);
builder.Services.RegisterBL(builder.Configuration);
builder.Services.RegisterWeb(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseMiddleware<ErrorLoggingMiddleware>();
//app.UseMiddleware<TenantMiddleware>();
//app.UseMiddleware<UserMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
