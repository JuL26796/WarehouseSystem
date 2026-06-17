using MaterialService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<MaterialDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(opt => opt.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// Tự động chèn dữ liệu mẫu nếu DB trống
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MaterialDbContext>();
    context.Database.EnsureCreated();
    if (!context.Materials.Any())
    {
        context.Materials.AddRange(
            new Warehouse.Shared.Models.Material { MaterialNo = "20159", MaterialName = "1.4-1.6MM CREAM NAPPA SKIN FACE RUB FULL GRAIN LEATHER", Unit = "SF" },
            new Warehouse.Shared.Models.Material { MaterialNo = "24333", MaterialName = "54inch 1.2MM 5395 C/ Backer: SD32 RPES-BIEN-BUCK-E-FV-ER434L non-wicking pfas free 60% Polyurethane (PU), 40% Recycled Polyester (rPES)", Unit = "YD" },
            new Warehouse.Shared.Models.Material { MaterialNo = "30338", MaterialName = "58inch Elderberry 17-1605 TPG AJT2012-042 EPM X NON-WICKING PFAS FREE + PU COATING 100% Recycled Polyester (rPES)", Unit = "YD" }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
