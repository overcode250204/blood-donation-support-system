using System.Windows;
using DAL.Entities;

namespace BloodDonationSupportSystem
{
    public partial class HealthCheckDialog : Window
    {
        private readonly Guid _registrationId;
        public HealthCheckDialog(Guid registrationId)
        {
            InitializeComponent();
            _registrationId = registrationId;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(HeightTextBox.Text, out double height) || height <= 0)
            {
                MessageBox.Show("Chiều cao không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!double.TryParse(WeightTextBox.Text, out double weight) || weight <= 0)
            {
                MessageBox.Show("Cân nặng không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var context = new BlooddonationsupportsystemContext();
            var reg = context.DonationRegistrations.Find(_registrationId);
            if (reg == null)
            {
                MessageBox.Show("Không tìm thấy đơn đăng ký!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var healthCheck = new HealthCheck
            {
                HealthCheckId = Guid.NewGuid(),
                Height = height,
                Weight = weight,
                HealthStatus = HealthStatusTextBox.Text,
                Note = NoteTextBox.Text,
                DonationRegistrationId = _registrationId
            };
            context.HealthChecks.Add(healthCheck);
            reg.Status = "ĐÃ HIẾN";
            context.SaveChanges();
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