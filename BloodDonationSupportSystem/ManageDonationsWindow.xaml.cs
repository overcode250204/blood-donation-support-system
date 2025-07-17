using System.Windows;

namespace BloodDonationSupportSystem
{
    public partial class ManageDonationsWindow : Window
    {
        public ManageDonationsWindow()
        {
            InitializeComponent();
        }

        private void DonationListButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new DonationListDialog();
            dialog.Owner = this;
            dialog.ShowDialog();
        }
        private void HealthCheckButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new HealthCheckDialog();
            dialog.Owner = this;
            dialog.ShowDialog();
        }
        private void BloodTakingButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new BloodTakingDialog();
            dialog.Owner = this;
            dialog.ShowDialog();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} 