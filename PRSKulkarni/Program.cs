using Microsoft.EntityFrameworkCore;
using PRSKulkarni.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
.AddJsonOptions(opt =>
 {
     opt.JsonSerializerOptions.ReferenceHandler =
       System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
 });

builder.Services.AddDbContext<PrsDbContext>(
       // lambda
       options => options.UseSqlServer(builder.Configuration.GetConnectionString("PRSConnectionstring"))
       );

var app = builder.Build();

// Configure the HTTP request pipeline.

// allows access to files like pictures and HTML. Defaults to wwwroot
// app.UseCors(CorsOptions.AllowAll);
// allows access to files like pictures and HTML. Deault to wwwroot
app.UseStaticFiles(); 

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
