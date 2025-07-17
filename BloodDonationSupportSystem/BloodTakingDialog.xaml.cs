using System.Windows;

namespace BloodDonationSupportSystem
{
    public partial class BloodTakingDialog : Window
    {
        public BloodTakingDialog()
        {
            InitializeComponent();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} 