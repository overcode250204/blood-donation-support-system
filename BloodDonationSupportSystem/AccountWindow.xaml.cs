using System;
using System.Windows;
using DAL.Entities;
using BLL.Services;

namespace BloodDonationSupportSystem
{
    public partial class AccountWindow : Window
    {
        private UserTable _user;
        private AuthenticationService _authService;
        private UserService _userService;
        public AccountWindow(UserTable user,  UserService userService)
        {
            InitializeComponent();
            _user = user;
          
            _userService = userService;
            ShowUserInfo();
        }

        private void ShowUserInfo()
        {
            FullNameText.Text = _user.FullName;
            PhoneNumberText.Text = _user.PhoneNumber;
            AddressText.Text = _user.Address;
            DateOfBirthText.Text = _user.DateOfBirth?.ToString("dd/MM/yyyy") ?? "";
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AccountDialogWindow(_user, _userService);
            dialog.Owner = this;
            var result = dialog.ShowDialog();
            if (result == true)
            {
               
                ShowUserInfo();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} 