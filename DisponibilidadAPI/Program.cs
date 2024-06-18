
using DisponibilidadAPI.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var filePath = Path.Combine(builder.Environment.ContentRootPath, "Data", "data.json");
        var json = File.ReadAllText(filePath);
        var data = JsonSerializer.Deserialize<CombinacionDataJson>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        }
        });


        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<List<Empleado>>(data.empleados);
        builder.Services.AddSingleton<List<Equipo>>(data.equipos);


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
