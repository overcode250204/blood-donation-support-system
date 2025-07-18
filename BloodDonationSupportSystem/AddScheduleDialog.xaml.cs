using System;
using System.Windows;
using DAL.Entities;
using BLL.Services;

namespace BloodDonationSupportSystem
{
    public partial class AddScheduleDialog : Window
    {
        public AddScheduleDialog()
        {
            InitializeComponent();
        }
        public Guid StaffId { get; set; } = Guid.Empty;
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AddressHospitalTextBox.Text))
            {
                MessageBox.Show("Vui lòng nhập tên bệnh viện!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (DonationDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày hiến máu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(StartTimeTextBox.Text) || string.IsNullOrWhiteSpace(EndTimeTextBox.Text))
            {
                MessageBox.Show("Vui lòng nhập giờ bắt đầu và kết thúc!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!TimeOnly.TryParse(StartTimeTextBox.Text, out var startTime) || !TimeOnly.TryParse(EndTimeTextBox.Text, out var endTime))
            {
                MessageBox.Show("Định dạng giờ không hợp lệ! (hh:mm)", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(AmountRegistrationTextBox.Text) || !int.TryParse(AmountRegistrationTextBox.Text, out int amount) || amount < 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng người đăng ký hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var context = new BlooddonationsupportsystemContext();
            var service = new BloodDonationScheduleService(null, null);
            if (!service.AddSchedule(context, AddressHospitalTextBox.Text, DateOnly.FromDateTime(DonationDatePicker.SelectedDate.Value), startTime, endTime, out string error, StaffId, amount))
            {
                MessageBox.Show(error, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Đã thêm lịch hiến máu thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
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