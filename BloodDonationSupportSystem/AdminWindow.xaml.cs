using BloodDonationSupportSystem.Dashboard;
using System.Windows;

namespace BloodDonationSupportSystem
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }

        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            new DashboardWindow().Show();
            Close();
        }

        private void AccountManagementButton_Click(object sender, RoutedEventArgs e)
        {
            new ManageAccountWindow().Show();
            Close();
        }
    }
} 