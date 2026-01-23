// Program.cs
using BloodLink.Data;
using Microsoft.EntityFrameworkCore;
// تأكد من وجود هذه المكتبات إذا لم تكن موجودة تلقائياً
using Microsoft.Extensions.DependencyInjection; 

var builder = WebApplication.CreateBuilder(args);

// 1. Database Connection (Modified to SQLite)
builder.Services.AddDbContext<BloodLinkContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// 2. Optimized CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.SetIsOriginAllowed(origin => true)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    }); 
});

builder.Services.AddControllers();

// 3. Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Always show Swagger for development ease
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BloodLink API V1");
});

// تفعيل الـ CORS والـ Routing والـ Authorization بالترتيب الصحيح
app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();