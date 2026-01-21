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
                policy.WithOrigins("http://localhost:3000", "http://localhost:3001")
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

        // Swagger (خليه دايمًا شغال عندك)
        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapOpenApi();

        // ✅ مهم: هنشيل الـ HTTPS Redirect عشان الفرونت بيكلم http://localhost:5240
        // app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("AllowReact");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}