using System.Windows;
using System.Collections.Generic;
using DAL.Entities;
using BLL.Services;

namespace BloodDonationSupportSystem
{
    public partial class DonationListDialog : Window
    {
        private BloodDonationScheduleService _service;
        private List<DonationRegistration> _pending;
        public DonationListDialog()
        {
            InitializeComponent();
            var context = new DAL.Entities.BlooddonationsupportsystemContext();
            var scheduleRepo = new DAL.Repositories.BloodDonationScheduleRepository(context);
            var regRepo = new DAL.Repositories.BloodDonationRegistrationRepository(context);
            _service = new BloodDonationScheduleService(scheduleRepo, regRepo);
            LoadPending();
        }
        private void LoadPending()
        {
            _pending = _service.GetPendingRegistrations();
            PendingListView.ItemsSource = _pending;
        }
        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = PendingListView.SelectedItem as DonationRegistration;
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn một đơn để chấp nhận!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var dialog = new HealthCheckDialog(selected.DonationRegistrationId);
            dialog.Owner = this;
            if (dialog.ShowDialog() == true)
            {
                MessageBox.Show("Đã chấp nhận đơn và tạo phiếu kiểm tra sức khỏe!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadPending();
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = PendingListView.SelectedItem as DonationRegistration;
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn một đơn để hủy!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _service.CancelRegistration(selected.DonationRegistrationId);
            MessageBox.Show("Đã hủy đơn thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadPending();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} 