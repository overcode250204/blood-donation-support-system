using System.Windows;
using DAL.Entities;
using BLL.Services;

namespace BloodDonationSupportSystem
{
    public partial class AccountDialogWindow : Window
    {
        private UserTable _user;

        private UserService _userService;
        public AccountDialogWindow(UserTable user, UserService userService)
        {
            InitializeComponent();
            _user = user;
         
            _userService = userService;
            FullNameTextBox.Text = _user.FullName;
            PhoneNumberTextBox.Text = _user.PhoneNumber;
            AddressTextBox.Text = _user.Address;
            if (_user.DateOfBirth != null)
                DateOfBirthPicker.SelectedDate = _user.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue);
        }
        public AccountDialogWindow() : this( null, null) { }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Cập nhật thông tin vào _user
            _user.FullName = FullNameTextBox.Text;
            _user.Address = AddressTextBox.Text;
            if (DateOfBirthPicker.SelectedDate != null)
                _user.DateOfBirth = DateOnly.FromDateTime(DateOfBirthPicker.SelectedDate.Value);
            _userService?.UpdateUser(_user);
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} 