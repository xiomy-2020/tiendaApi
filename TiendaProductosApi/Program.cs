using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using TiendaProductosApi.Data;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options=>
            options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));

builder.Services.AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition=
            System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddCors(options =>
{
        options.AddPolicy("ReactPolicty", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
    );
});


var app = builder.Build();

// Configuración de ruta estática para la carpeta que contiene las imágenes
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.ContentRootPath, "images/imagenes")), // Ruta de la carpeta de imágenes
    RequestPath = "/images" // Ruta URL para acceder a las imágenes
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("ReactPolicty");

app.Run();




