using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Shared.Models;

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryDbContext _context;
        public InventoryController(InventoryDbContext context) { _context = context; }

        [HttpGet("stock")]
        public async Task<ActionResult<ApiResponse<List<Stock>>>> GetAllStock()
        {
            var data = await _context.Stocks.ToListAsync<Stock>();
            return new ApiResponse<List<Stock>> { ID = 200, Message = "Lấy tồn kho thành công", Data = data };
        }

        // URL gọi API này sẽ là: PUT https://localhost:7071/api/Inventory/update-quantity
        [HttpPut("update-quantity")]
        public async Task<ActionResult<ApiResponse<Stock>>> UpdateQuantity([FromBody] Stock updateDto)
        {
            try
            {
                // 1. Tìm xem vật tư này đã có trong bảng Tồn kho chưa
                var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.MaterialNo == updateDto.MaterialNo);

                if (stock == null)
                {
                    // Nếu chưa có (vật tư mới tinh chưa từng nhập kho), tiến hành tạo mới dòng tồn kho
                    stock = new Stock
                    {
                        MaterialNo = updateDto.MaterialNo,
                        CurrentQuantity = updateDto.CurrentQuantity
                    };
                    _context.Stocks.Add(stock);
                }
                else
                {
                    // Nếu đã có rồi, gán thẳng số lượng mới được cập nhật từ giao diện sang
                    stock.CurrentQuantity = updateDto.CurrentQuantity;
                    _context.Stocks.Update(stock);
                }

                await _context.SaveChangesAsync();

                return Ok(new ApiResponse<Stock>
                {
                    ID = 200,
                    Message = $"Đã cập nhật số lượng cho vật tư {updateDto.MaterialNo} thành {updateDto.CurrentQuantity}!",
                    Data = stock
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<Stock> { ID = 500, Message = $"Lỗi Backend: {ex.Message}" });
            }
        }
    }
}
