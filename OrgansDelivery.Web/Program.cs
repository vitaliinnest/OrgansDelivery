using OrgansDelivery.DAL.Extensions;
using OrgansDelivery.BL.Extensions;
using OrgansDelivery.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.RegisterDAL(builder.Configuration);
    builder.Services.RegisterBL(builder.Configuration);
    builder.Services.RegisterWeb(builder.Configuration);
}
