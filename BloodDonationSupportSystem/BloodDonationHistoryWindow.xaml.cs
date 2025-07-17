using System.Windows;
using System.Collections.Generic;
using DAL.Entities;
using BLL.Services;

namespace BloodDonationSupportSystem
{
    public partial class BloodDonationHistoryWindow : Window
    {
        private UserTable _user;
        private BloodDonationScheduleService _service;
        private List<DonationRegistration> _history;
        public BloodDonationHistoryWindow(UserTable user, BloodDonationScheduleService service)
        {
            InitializeComponent();
            _user = user;
            _service = service;
            LoadHistory();
        }
        private void LoadHistory()
        {
            _history = _service.GetUserHistory(_user.UserId);
            HistoryListView.ItemsSource = _history;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = HistoryListView.SelectedItem as DonationRegistration;
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn một đăng ký để hủy!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (selected.Status != "CHƯA HIẾN")
            {
                MessageBox.Show("Chỉ có thể hủy đăng ký ở trạng thái 'CHƯA HIẾN'!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _service.CancelRegistration(selected.DonationRegistrationId);
            MessageBox.Show("Đã hủy đăng ký thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadHistory();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} 