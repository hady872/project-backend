using BloodLink.Data;
using Microsoft.EntityFrameworkCore;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<BloodLinkContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReact", policy =>
                policy.WithOrigins("http://localhost:3000")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
            );
        });

        // Add services to the container.
        builder.Services.AddControllers();

        // Swagger / OpenAPI
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapOpenApi();

        app.UseHttpsRedirection();

        app.UseCors("AllowReact");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}