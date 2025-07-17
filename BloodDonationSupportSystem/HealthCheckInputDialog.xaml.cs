using System.Windows;

namespace BloodDonationSupportSystem
{
    public partial class HealthCheckInputDialog : Window
    {
        public double HeightValue { get; private set; }
        public double WeightValue { get; private set; }
        public string HealthStatusValue { get; private set; }
        public string NoteValue { get; private set; }
        public HealthCheckInputDialog()
        {
            InitializeComponent();
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
            HeightValue = height;
            WeightValue = weight;
            HealthStatusValue = HealthStatusTextBox.Text;
            NoteValue = NoteTextBox.Text;
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