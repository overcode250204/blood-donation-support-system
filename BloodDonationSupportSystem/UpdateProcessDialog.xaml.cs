using System.Windows;
using DAL.Entities;
using System.Linq;

namespace BloodDonationSupportSystem
{
    public partial class UpdateProcessDialog : Window
    {
        private readonly System.Guid _processId;
        public UpdateProcessDialog(System.Guid processId)
        {
            InitializeComponent();
            _processId = processId;
            LoadProcess();
        }
        private void LoadProcess()
        {
            var context = new BlooddonationsupportsystemContext();
            var process = context.DonationProcesses.FirstOrDefault(p => p.DonationProcessId == _processId);
            if (process != null)
            {
                BloodTestTextBox.Text = process.BloodTest;
                BloodTypeIdTextBox.Text = process.BloodTypeId;
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("Bạn có chắc chắn muốn cập nhật trạng thái blood test và nhóm máu?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm != MessageBoxResult.Yes) return;
            var context = new BlooddonationsupportsystemContext();
            var process = context.DonationProcesses.FirstOrDefault(p => p.DonationProcessId == _processId);
            if (process == null) { MessageBox.Show("Không tìm thấy quy trình!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error); this.DialogResult = false; this.Close(); return; }
            // Kiểm tra nhập thiếu
            if (string.IsNullOrWhiteSpace(BloodTestTextBox.Text))
            {
                MessageBox.Show("Vui lòng nhập trạng thái blood test!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(BloodTypeIdTextBox.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhóm máu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (process.VolumeMl <= 0)
            {
                MessageBox.Show("Thể tích máu không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Kiểm tra nếu đã từng ĐẠT trước đó thì không cộng volume nữa
            bool wasPassed = process.BloodTest != null && process.BloodTest.Trim().ToUpper() == "ĐÃ ĐẠT";
            bool willBePassed = BloodTestTextBox.Text.Trim().ToUpper() == "ĐÃ ĐẠT";
            process.BloodTest = BloodTestTextBox.Text;
            process.BloodTypeId = BloodTypeIdTextBox.Text;
            if (!wasPassed && willBePassed)
            {
                var inventory = context.BloodInventories.FirstOrDefault(b => b.BloodTypeId == process.BloodTypeId);
                if (inventory != null)
                {
                    inventory.TotalVolumeMl = (inventory.TotalVolumeMl ?? 0) + process.VolumeMl;
                }
                else
                {
                    context.BloodInventories.Add(new BloodInventory
                    {
                        BloodTypeId = process.BloodTypeId,
                        TotalVolumeMl = process.VolumeMl
                    });
                }
            }
            context.SaveChanges();
            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
            this.Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
} 