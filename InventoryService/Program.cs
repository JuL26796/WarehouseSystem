using InventoryService;
using Microsoft.EntityFrameworkCore;
using Warehouse.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(opt => opt.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();

    // Ép Entity Framework tự động tạo file SQLite và tạo bảng Stocks nếu chưa có
    context.Database.EnsureCreated();

    // Nếu bảng Stocks đang trống rỗng, chèn vài dữ liệu tồn kho mẫu vào để test
    if (!context.Stocks.Any())
    {
        context.Stocks.AddRange(
            new Stock { MaterialNo = "20159", CurrentQuantity = 150 }, // Khớp với mã vật tư bên MaterialService
            new Stock { MaterialNo = "24333", CurrentQuantity = 85 },
            new Stock { MaterialNo = "30338", CurrentQuantity = 60 }
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
