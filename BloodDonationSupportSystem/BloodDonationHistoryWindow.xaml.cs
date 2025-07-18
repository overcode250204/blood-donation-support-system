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
        private class DonationHistoryViewModel
        {
            public string AddressHospital { get; set; }
            public string DisplayDate { get; set; }
            public string RegistrationDate { get; set; }
            public string Status { get; set; }
            public string DateCompleteDonation { get; set; }
        }
        private void LoadHistory()
        {
            _history = _service.GetUserHistory(_user.UserId);
            var viewModels = new List<DonationHistoryViewModel>();
            foreach (var item in _history)
            {
                var address = item.BloodDonationSchedule?.AddressHospital ?? "";
                var ngayHien = item.BloodDonationSchedule?.DonationDate;
                string displayDate = ngayHien != null && ngayHien.Value != default(DateOnly)
                    ? ngayHien.Value.ToString("dd/MM/yyyy")
                    : item.RegistrationDate.ToString("dd/MM/yyyy");
                string registrationDate = item.RegistrationDate.ToString("dd/MM/yyyy");
                string status = item.Status ?? "";
                string dateComplete = (status == "ĐÃ HIẾN" && item.DateCompleteDonation != null)
                    ? item.DateCompleteDonation.Value.ToString("dd/MM/yyyy")
                    : "";
                viewModels.Add(new DonationHistoryViewModel
                {
                    AddressHospital = address,
                    DisplayDate = displayDate,
                    RegistrationDate = registrationDate,
                    Status = status,
                    DateCompleteDonation = dateComplete
                });
            }
            HistoryListView.ItemsSource = viewModels;
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