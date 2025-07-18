using System.Windows;

namespace BloodDonationSupportSystem
{
    public partial class BloodTakingInputDialog : Window
    {
        public string BloodTestValue { get; private set; }
        public int VolumeValue { get; private set; }
        public string StatusValue { get; private set; }
        public string BloodTypeIdValue { get; private set; }
        private readonly System.Guid _registrationId;
        public BloodTakingInputDialog(System.Guid registrationId)
        {
            InitializeComponent();
            _registrationId = registrationId;
            StatusTextBox.Text = "CHƯA KIỂM TRA";
            BloodTestTextBox.Text = string.Empty;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BloodTestTextBox.Text))
            {
                MessageBox.Show("Vui lòng nhập kết quả xét nghiệm máu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(VolumeTextBox.Text, out int volume) || volume <= 0)
            {
                MessageBox.Show("Thể tích không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            BloodTestValue = BloodTestTextBox.Text;
            VolumeValue = volume;
            StatusValue = StatusTextBox.Text;
            BloodTypeIdValue = BloodTypeIdTextBox.Text;
            
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