using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Shared.Models;

namespace MaterialService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly MaterialDbContext _context;
        public MaterialsController(MaterialDbContext context) { _context = context; }

        // 1. Lấy tất cả dữ liệu[cite: 2]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<Material>>>> GetAll()
        {
            var data = await _context.Materials.ToListAsync();
            return new ApiResponse<List<Material>> { ID = 200, Message = "Thành công", Data = data };
        }

        // 2. Lấy theo ID (Lấy theo mã vật liệu)[cite: 2]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Material>>> GetById(string id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null) return new ApiResponse<Material> { ID = 99, Message = "Không tìm thấy vật tư", Data = null };
            return new ApiResponse<Material> { ID = 200, Message = "Thành công", Data = material };
        }

        // 3. Thêm 1 vật liệu mới vào[cite: 2]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Material>>> Add(Material material)
        {
            _context.Materials.Add(material);
            await _context.SaveChangesAsync();
            return new ApiResponse<Material> { ID = 200, Message = "Thêm thành công", Data = material };
        }

        // 4. Sửa 1 vật liệu đã có[cite: 2]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Material>>> Update(string id, Material material)
        {
            if (id != material.MaterialNo) return new ApiResponse<Material> { ID = 99, Message = "ID không khớp", Data = null };
            _context.Entry(material).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ApiResponse<Material> { ID = 200, Message = "Sửa thành công", Data = material };
        }

        // 5. Xóa vật liệu[cite: 2]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(string id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null)
                return new ApiResponse<bool> { ID = 99, Message = "Không tìm thấy để xóa" };
            
            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();
            return new ApiResponse<bool> { ID = 200, Message = "Xóa thành công", Data = true };
        }
    }
}
