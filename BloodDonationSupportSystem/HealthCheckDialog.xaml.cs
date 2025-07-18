using System.Windows;
using System.Collections.Generic;
using DAL.Entities;
using BLL.Services;

namespace BloodDonationSupportSystem
{
    public partial class HealthCheckDialog : Window
    {
        private BloodDonationScheduleService _service;
        private List<DonationRegistration> _accepted;
        private HealthCheckService _healthCheckService;
        public HealthCheckDialog()
        {
            InitializeComponent();
            var context = new DAL.Entities.BlooddonationsupportsystemContext();
            var scheduleRepo = new DAL.Repositories.BloodDonationScheduleRepository(context);
            var regRepo = new DAL.Repositories.BloodDonationRegistrationRepository(context);
            _service = new BloodDonationScheduleService(scheduleRepo, regRepo);
            _healthCheckService = new HealthCheckService(context);
            LoadAccepted();
        }
        private void LoadAccepted()
        {
            _accepted = _healthCheckService.GetAcceptedRegistrationsWithoutHealthCheck();
            AcceptedListView.ItemsSource = _accepted;
        }
        private void InputHealthInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = AcceptedListView.SelectedItem as DonationRegistration;
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn một đơn để nhập thông tin sức khỏe!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var dialog = new HealthCheckInputDialog();
            dialog.Owner = this;
            if (dialog.ShowDialog() == true)
            {
                _healthCheckService.AddHealthCheck(selected.DonationRegistrationId, dialog.HeightValue, dialog.WeightValue, dialog.HealthStatusValue, dialog.NoteValue);
                MessageBox.Show("Đã lưu thông tin sức khỏe thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadAccepted();
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} 