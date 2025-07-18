using BLL.Services;
using DAL.DTOs;
using DAL.Repositories;
using DAL.Entities;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
// LiveChartsCore
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting; // cho SolidColorPaint
using SkiaSharp; // cho SKColors
using ClosedXML.Excel;
using Microsoft.Win32;  // cho SaveFileDialog
namespace BloodDonationSupportSystem.Dashboard
{
    public partial class DashboardWindow : Window
    {
        private readonly DashboardService _dashboardService;

        public DashboardWindow()
        {
            InitializeComponent();

            var optionsBuilder = new DbContextOptionsBuilder<BlooddonationsupportsystemContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=blooddonationsupportsystem;Trusted_Connection=True;");

            var context = new BlooddonationsupportsystemContext(optionsBuilder.Options);
            var dashboardRepo = new DashboardRepository(context);
            _dashboardService = new DashboardService(dashboardRepo);

            Loaded += DashboardWindow_Loaded;
        }

        private async void DashboardWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadDashboardAsync();
        }

        private async Task LoadDashboardAsync()
        {
            try
            {
                DashboardStatsDto stats = await _dashboardService.GetDashboardStatsAsync();

                UserCountText.Text = stats.TotalUsers.ToString();

                DonationStatusList.Items.Clear();
                foreach (var item in stats.DonationStatusCounts)
                    DonationStatusList.Items.Add($"{item.Key}: {item.Value}");

                // PieChart data
                DonationStatusChart.Series = stats.DonationStatusCounts.Select(kv =>
                    new PieSeries<int>
                    {
                        Values = new[] { kv.Value },
                        Name = kv.Key,
                        DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                        DataLabelsSize = 16,
                        // DataLabelsPosition không được hỗ trợ nên bỏ
                    }).ToArray();

                BloodInventoryList.Items.Clear();
                foreach (var item in stats.BloodInventory)
                    BloodInventoryList.Items.Add($"{item.Key}: {item.Value} ml");

                EmergencyList.Items.Clear();
                foreach (var item in stats.EmergencyRequests)
                    EmergencyList.Items.Add($"{item.Key}: {item.Value}");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu dashboard: " + ex.Message);
            }
        }

        // Export file
        private async Task ExportDashboardToExcel()
        {
            try
            {
                // Lấy dữ liệu Dashboard
                DashboardStatsDto stats = await _dashboardService.GetDashboardStatsAsync();

                // Tạo workbook mới
                using var workbook = new XLWorkbook();
                var ws = workbook.Worksheets.Add("Dashboard");

                // Định dạng chung
                var titleStyle = ws.Style;
                titleStyle.Font.Bold = true;
                titleStyle.Font.FontSize = 14;
                titleStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                int currentRow = 1;

                // Tiêu đề chính
                ws.Cell(currentRow, 1).Value = "Admin Dashboard Export";
                ws.Cell(currentRow, 1).Style.Font.Bold = true;
                ws.Cell(currentRow, 1).Style.Font.FontSize = 18;
                currentRow += 2;

                // Tổng số người dùng
                ws.Cell(currentRow, 1).Value = "Tổng số người dùng:";
                ws.Cell(currentRow, 2).Value = stats.TotalUsers;
                ws.Range(currentRow, 1, currentRow, 2).Style.Font.Bold = true;
                currentRow += 2;

                // Đăng ký hiến máu
                ws.Cell(currentRow, 1).Value = "Đăng ký hiến máu:";
                ws.Cell(currentRow, 1).Style.Font.Bold = true;
                currentRow++;

                ws.Cell(currentRow, 1).Value = "Trạng thái";
                ws.Cell(currentRow, 2).Value = "Số lượng";
                ws.Range(currentRow, 1, currentRow, 2).Style.Font.Bold = true;
                ws.Range(currentRow, 1, currentRow, 2).Style.Fill.BackgroundColor = XLColor.LightBlue;
                currentRow++;

                foreach (var item in stats.DonationStatusCounts)
                {
                    ws.Cell(currentRow, 1).Value = item.Key;
                    ws.Cell(currentRow, 2).Value = item.Value;
                    currentRow++;
                }
                currentRow++;

                // Tồn kho máu
                ws.Cell(currentRow, 1).Value = "Tồn kho máu:";
                ws.Cell(currentRow, 1).Style.Font.Bold = true;
                currentRow++;

                ws.Cell(currentRow, 1).Value = "Nhóm máu";
                ws.Cell(currentRow, 2).Value = "Số lượng (ml)";
                ws.Range(currentRow, 1, currentRow, 2).Style.Font.Bold = true;
                ws.Range(currentRow, 1, currentRow, 2).Style.Fill.BackgroundColor = XLColor.LightGreen;
                currentRow++;

                foreach (var item in stats.BloodInventory)
                {
                    ws.Cell(currentRow, 1).Value = item.Key;
                    ws.Cell(currentRow, 2).Value = item.Value;
                    currentRow++;
                }
                currentRow++;

                // Yêu cầu khẩn cấp
                ws.Cell(currentRow, 1).Value = "Yêu cầu khẩn cấp:";
                ws.Cell(currentRow, 1).Style.Font.Bold = true;
                currentRow++;

                ws.Cell(currentRow, 1).Value = "Mức độ";
                ws.Cell(currentRow, 2).Value = "Số lượng";
                ws.Range(currentRow, 1, currentRow, 2).Style.Font.Bold = true;
                ws.Range(currentRow, 1, currentRow, 2).Style.Fill.BackgroundColor = XLColor.LightCoral;
                currentRow++;

                foreach (var item in stats.EmergencyRequests)
                {
                    ws.Cell(currentRow, 1).Value = item.Key;
                    ws.Cell(currentRow, 2).Value = item.Value;
                    currentRow++;
                }

                // Tự động điều chỉnh kích thước cột
                ws.Columns().AdjustToContents();

                // Mở hộp thoại lưu file
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = "DashboardExport.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Export Excel thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi export Excel: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ExportExcelButton_Click(object sender, RoutedEventArgs e)
        {
            await ExportDashboardToExcel();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

}
