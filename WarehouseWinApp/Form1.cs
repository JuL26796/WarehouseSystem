using System.Net.Http.Json;
using Warehouse.Shared.Models;

namespace WarehouseWinApp
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _http = new HttpClient();
        string apiUrl = "https://localhost:7055/api/Materials";
        string stockApiUrl = "https://localhost:7071/api/Inventory/stock";
        string updateStockApiUrl = "https://localhost:7071/api/Inventory/update-quantity";
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                btnLoad.Enabled = false;
                btnLoad.Text = "Đang tải dữ liệu...";

                // Đọc dữ liệu ra theo đúng khuôn mẫu ApiResponse
                var response = await _http.GetFromJsonAsync<ApiResponse<List<Material>>>(apiUrl);

                // 2. Bóc tách dữ liệu theo quy định định dạng bắt buộc (ID, Message, Data)
                if (response != null && response.ID == 200) // ID = 200 tức là Backend xử lý thành công
                {
                    // Đổ mớ danh sách vật tư nằm trong thuộc tính .Data vào bảng Grid
                    dgvMaterials.DataSource = response.Data;

                    // Tự động căn chỉnh độ rộng các cột cho đẹp mắt
                    dgvMaterials.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    MessageBox.Show(response.Message, "Hệ thống kho", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // ID = 99 hoặc các mã lỗi khác, hiển thị thông báo lỗi từ dịch vụ trả về
                    MessageBox.Show(response?.Message ?? "Lỗi không xác định từ dịch vụ", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Bắt các lỗi mất kết nối mạng, sai Port, hoặc Backend chưa chạy
                MessageBox.Show($"Không thể kết nối đến WebService!\nHãy chắc chắn API đang chạy.\nChi tiết: {ex.Message}",
                                "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Trả lại trạng thái ban đầu cho nút bấm
                btnLoad.Enabled = true;
                btnLoad.Text = "Tải Danh mục Vật tư";
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var newMaterial = new Material
            {
                MaterialNo = txtMaterialNo.Text.Trim(),
                MaterialName = txtMaterialName.Text.Trim(),
                Unit = txtUnit.Text.Trim()
            };

            if (string.IsNullOrEmpty(newMaterial.MaterialNo))
            {
                MessageBox.Show("Vui lòng nhập Mã vật tư!"); return;
            }

            var responseMessage = await _http.PostAsJsonAsync(apiUrl, newMaterial);
            var result = await responseMessage.Content.ReadFromJsonAsync<ApiResponse<Material>>();

            if (result != null && result.ID == 200)
            {
                MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLoad_Click(sender, e); // Tải lại bảng dữ liệu
            }
            else
            {
                MessageBox.Show(result?.Message ?? "Lỗi khi thêm mới", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            var updatedMaterial = new Material
            {
                MaterialNo = txtMaterialNo.Text.Trim(),
                MaterialName = txtMaterialName.Text.Trim(),
                Unit = txtUnit.Text.Trim()
            };

            var responseMessage = await _http.PutAsJsonAsync($"{apiUrl}/{updatedMaterial.MaterialNo}", updatedMaterial);
            var result = await responseMessage.Content.ReadFromJsonAsync<ApiResponse<Material>>();

            if (result != null && result.ID == 200)
            {
                MessageBox.Show(result.Message, "Cập nhật thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLoad_Click(sender, e);
            }
            else
            {
                MessageBox.Show(result?.Message ?? "Lỗi khi cập nhật", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtMaterialNo.Text.Trim();
            if (string.IsNullOrEmpty(id)) return;

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa vật tư {id}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;

            var responseMessage = await _http.DeleteAsync($"{apiUrl}/{id}");
            var result = await responseMessage.Content.ReadFromJsonAsync<ApiResponse<bool>>();

            if (result != null && result.ID == 200)
            {
                MessageBox.Show(result.Message, "Đã xóa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLoad_Click(sender, e);
            }
            else
            {
                MessageBox.Show(result?.Message ?? "Lỗi khi xóa", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvMaterials_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMaterials.Rows[e.RowIndex];
                txtMaterialNo.Text = row.Cells["MaterialNo"].Value?.ToString();
                txtMaterialName.Text = row.Cells["MaterialName"].Value?.ToString();
                txtUnit.Text = row.Cells["Unit"].Value?.ToString();
            }
        }

        private async void btnLoadStock_Click(object sender, EventArgs e)
        {
            try
            {
                btnLoadStock.Text = "Đang tải...";

                // Gọi sang InventoryService lấy danh sách Stock
                var response = await _http.GetFromJsonAsync<ApiResponse<List<Stock>>>(stockApiUrl);

                if (response != null && response.ID == 200)
                {
                    dgvStock.DataSource = response.Data;
                    dgvStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    MessageBox.Show(response.Message, "Tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(response?.Message ?? "Không thể lấy dữ liệu tồn kho", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối dịch vụ tồn kho: {ex.Message}", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLoadStock.Text = "Xem Tồn Kho";
            }
        }

        private async void btnSaveStock_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy dữ liệu từ ô Mã vật tư và ô Số lượng tồn kho trên giao diện
                string matNo = txtMaterialNo.Text.Trim();
                string qtyText = txtStockQty.Text.Trim();

                // Kiểm tra điều kiện nhập liệu
                if (string.IsNullOrEmpty(matNo))
                {
                    MessageBox.Show("Vui lòng nhập Mã vật tư vào ô trống hoặc chọn một dòng từ danh sách!",
                                    "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(qtyText, out int qty) || qty < 0)
                {
                    MessageBox.Show("Vui lòng nhập số lượng tồn kho hợp lệ (phải là số nguyên từ 0 trở lên)!",
                                    "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Đóng gói dữ liệu vào Model Stock từ tầng Shared
                var stockDto = new Stock
                {
                    MaterialNo = matNo,
                    CurrentQuantity = qty
                };

                // 3. Bắn lệnh PUT sang InventoryService (Cổng 7071)
                // Backend nhận được, nếu chưa có mã này trong DB -> Tự động thêm mới. Nếu có rồi -> Cập nhật.
                var responseMessage = await _http.PutAsJsonAsync(updateStockApiUrl, stockDto);
                var result = await responseMessage.Content.ReadFromJsonAsync<ApiResponse<Stock>>();

                if (result != null && result.ID == 200)
                {
                    // Hiển thị thông báo thành công (Backend trả về câu chữ rất rõ ràng)
                    MessageBox.Show(result.Message, "Hệ thống kho", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 4. ÉP BẢNG TỒN KHO TẢI LẠI NGAY LẬP TỨC để dòng vật tư mới xuất hiện trên màn hình
                    btnLoadStock_Click(sender, e);

                    // Xóa sạch ô nhập số lượng để tiện thao tác mã tiếp theo
                    txtStockQty.Clear();
                }
                else
                {
                    MessageBox.Show(result?.Message ?? "Không thể xử lý dữ liệu tồn kho", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối đến Dịch vụ Tồn kho (Cổng 7071): {ex.Message}", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStock.Rows[e.RowIndex];
                // Điền ngược mã vật tư lên ô txtMaterialNo chung
                txtMaterialNo.Text = row.Cells["MaterialNo"].Value?.ToString();
                // Điền số lượng hiện tại lên ô txtStockQty để sửa
                txtStockQty.Text = row.Cells["CurrentQuantity"].Value?.ToString();
            }
        }
    }
}
