using System.Net.Http.Json;
using Warehouse.Shared.Models;

namespace WarehouseMobileApp
{
    public partial class MainPage : ContentPage
    {
        private static HttpClient GetInsecureHttpClient()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    // Luôn luôn trả về true để ép Android chấp nhận kết nối HTTPS tới máy tính của bạn
                    return true;
                }
            };
            return new HttpClient(handler);
        }

        private readonly HttpClient _http = GetInsecureHttpClient();
        private string baseApiUrl = "http://10.0.2.2:5128";
        private string currentMaterialNo = "";
        private int currentQty = 0;

        public MainPage()
        {
            InitializeComponent();

            barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
            {
                Formats = ZXing.Net.Maui.BarcodeFormat.QrCode | ZXing.Net.Maui.BarcodeFormat.Code128
            };
        }

        // 1. HÀM CHẠY KHI CAMERA QUÉT TRÚNG MÃ VẠCH THẬT
        private async void OnBarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {
            var firstBarcode = e.Results.FirstOrDefault();
            if (firstBarcode == null) return;

            Dispatcher.Dispatch(() =>
            {
                barcodeReader.IsDetecting = false;
            });

            currentMaterialNo = firstBarcode.Value;
            await FetchStockData();
        }

        // HÀM GỌI API LẤY DỮ LIỆU CHUNG
        private async Task FetchStockData()
        {
            try
            {
                string url = $"{baseApiUrl}/api/Inventory/stock";
                var response = await _http.GetFromJsonAsync<ApiResponse<List<Stock>>>(url);
                var matchedStock = response?.Data?.FirstOrDefault(x => x.MaterialNo == currentMaterialNo);

                Dispatcher.Dispatch(() =>
                {
                    lblStatus.Text = $"MÃ VẬT TƯ: {currentMaterialNo}";
                    if (matchedStock != null)
                    {
                        currentQty = matchedStock.CurrentQuantity;
                        lblQty.Text = $"Tồn kho: {currentQty} cái";
                    }
                    else
                    {
                        currentQty = 0;
                        lblQty.Text = "Chưa có tồn kho (0 cái)";
                    }
                    lblQty.IsVisible = true;
                });
            }
            catch (Exception ex)
            {
                Dispatcher.Dispatch(() =>
                {
                    lblStatus.Text = $"Lỗi kết nối API: {ex.Message}";
                });
            }
        }

        // 2. HÀM XỬ LÝ NÚT NHẬP KHO (+1)
        private async void OnAddStockClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaterialNo)) return;

            try
            {
                currentQty += 1;
                var updateDto = new Stock { MaterialNo = currentMaterialNo, CurrentQuantity = currentQty };

                string updateUrl = $"{baseApiUrl}/api/Inventory/update-quantity";
                var response = await _http.PutAsJsonAsync(updateUrl, updateDto);

                if (response.IsSuccessStatusCode)
                {
                    lblQty.Text = $"Tồn kho: {currentQty} cái";
                    await this.DisplayAlertAsync("Thành công", $"Đã nhập kho +1 cho mã {currentMaterialNo}", "OK");
                }
            }
            catch (Exception ex)
            {
                await this.DisplayAlertAsync("Lỗi", ex.Message, "OK");
            }
        }

        private async void OnRemoveStockClicked(object sender, EventArgs e)
        {
            // Nếu chưa quét hoặc chưa giả lập mã vật tư thì không làm gì cả
            if (string.IsNullOrEmpty(currentMaterialNo)) return;

            // NGHIỆP VỤ: Kiểm tra chặn xuất âm kho nếu số lượng đã bằng 0
            if (currentQty <= 0)
            {
                await this.DisplayAlertAsync("Thao tác thất bại", "Số lượng vật tư trong kho đã bằng 0, không thể xuất thêm!", "OK");
                return;
            }

            try
            {
                currentQty -= 1; // Giảm số lượng tồn kho đi 1 đơn vị
                var updateDto = new Stock { MaterialNo = currentMaterialNo, CurrentQuantity = currentQty };

                // Gọi API PUT đồng bộ sang cổng 5128 của InventoryService
                string updateUrl = $"{baseApiUrl}/api/Inventory/update-quantity";
                var response = await _http.PutAsJsonAsync(updateUrl, updateDto);

                if (response.IsSuccessStatusCode)
                {
                    lblQty.Text = $"Tồn kho: {currentQty} cái";
                    await this.DisplayAlertAsync("Thành công", $"Đã xuất kho -1 cho mã vật tư {currentMaterialNo}", "OK");
                }
                else
                {
                    // Nếu API trả về lỗi, hoàn tác lại số lượng trên giao diện
                    currentQty += 1;
                    await this.DisplayAlertAsync("Thất bại", "Không thể cập nhật số lượng lên hệ thống!", "OK");
                }
            }
            catch (Exception ex)
            {
                // Nếu sập mạng hoặc lỗi API, hoàn tác lại số lượng
                currentQty += 1;
                await this.DisplayAlertAsync("Lỗi kết nối", ex.Message, "OK");
            }
        }

        // 3. HÀM XỬ LÝ NÚT QUÉT LẠI
        private void OnResetClicked(object sender, EventArgs e)
        {
            barcodeReader.IsDetecting = true;
            lblStatus.Text = "ĐANG CHỜ QUÉT MÃ VẬT TƯ...";
            lblQty.IsVisible = false;
            currentMaterialNo = "";
        }

        // 4. HÀM XỬ LÝ NÚT GIẢ LẬP QUÉT MÃ (NÚT TÍM)
        private async void OnSimulateScanClicked(object sender, EventArgs e)
        {
            currentMaterialNo = "24022";
            lblStatus.Text = $"MÃ VẬT TƯ (GIẢ LẬP): {currentMaterialNo}";
            await FetchStockData();
        }
    }
    
}
