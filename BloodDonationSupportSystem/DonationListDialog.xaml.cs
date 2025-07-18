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
        private RegistrationService _registrationService;
        public DonationListDialog()
        {
            InitializeComponent();
            var context = new DAL.Entities.BlooddonationsupportsystemContext();
            var scheduleRepo = new DAL.Repositories.BloodDonationScheduleRepository(context);
            var regRepo = new DAL.Repositories.BloodDonationRegistrationRepository(context);
            _service = new BloodDonationScheduleService(scheduleRepo, regRepo);
            _registrationService = new RegistrationService(context);
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
            var result = MessageBox.Show($"Bạn có chắc chắn muốn chấp nhận đơn của {selected.Donor.FullName}?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _registrationService.AcceptRegistration(selected.DonationRegistrationId);
                MessageBox.Show("Đã chấp nhận đơn thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
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