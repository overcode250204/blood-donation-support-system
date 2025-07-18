using System.Windows;
using System.Collections.Generic;
using DAL.Entities;
using BLL.Services;

namespace BloodDonationSupportSystem
{
    public partial class BloodTakingDialog : Window
    {
        private RegistrationService _registrationService;
        private ProcessService _processService;
        private List<DonationRegistration> _passed;
        public BloodTakingDialog()
        {
            InitializeComponent();
            var context = new DAL.Entities.BlooddonationsupportsystemContext();
            _registrationService = new RegistrationService(context);
            _processService = new ProcessService(context);
            LoadPassed();
        }
        private void LoadPassed()
        {
            _passed = _registrationService.GetRegistrationsHealthCheckPassed();
            PassedListView.ItemsSource = _passed;
        }
        private void InputProcessInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = PassedListView.SelectedItem as DonationRegistration;
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn một đơn để nhập thông tin lấy máu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var dialog = new BloodTakingInputDialog(selected.DonationRegistrationId);
            dialog.Owner = this;
            if (dialog.ShowDialog() == true)
            {
                _processService.AddProcess(selected.DonationRegistrationId, dialog.BloodTestValue, dialog.VolumeValue, dialog.StatusValue, dialog.BloodTypeIdValue);
                MessageBox.Show("Đã lưu thông tin lấy máu thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadPassed();
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} 