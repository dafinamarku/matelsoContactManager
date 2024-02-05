
using Microsoft.EntityFrameworkCore;
using ModelLayer.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(
    x=>x.UseNpgsql(builder.Configuration.GetConnectionString("Contact_Manager_Db")));

builder.Services.AddControllers();


var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
    options.DocumentTitle = "Contact API";
    options.HeadContent = "Contact API";
});  

app.Run();
