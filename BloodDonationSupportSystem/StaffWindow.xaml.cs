using System.Windows;

namespace BloodDonationSupportSystem
{
    public partial class StaffWindow : Window
    {
        public StaffWindow()
        {
            InitializeComponent();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }
        private void ManageDonationsButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new ManageDonationsWindow();
            win.Owner = this;
            win.ShowDialog();
        }
        private void ManageSchedulesButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new ManageSchedulesWindow();
            win.Owner = this;
            win.ShowDialog();
        }
        private void ManageInventoryButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new ManageInventoryWindow();
            win.Owner = this;
            win.ShowDialog();
        }
    }
} 