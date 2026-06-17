using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Shared.Models
{
    // Định dạng chuẩn: ID, message, data
    public class ApiResponse<T>
    {
        public int ID { get; set; } // Thành công = 200, Thất bại = 99
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }

    // Mô hình Vật tư dùng chung
    public class Material
    {
        public string MaterialNo { get; set; } = string.Empty;
        public string MaterialName { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
    }

    // Mô hình Tồn kho dùng chung
    public class Stock
    {
        public string MaterialNo { get; set; } = string.Empty;
        public int CurrentQuantity { get; set; }
    }
}
