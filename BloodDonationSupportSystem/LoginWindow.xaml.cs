    using System.Windows;
    using DAL.Entities;
    using DAL.Repositories;
    using BLL.Services;

    namespace BloodDonationSupportSystem
    {
        public partial class LoginWindow : Window
        {
            private readonly AuthenticationService _authService;
            private readonly UserService _userService;

            public LoginWindow()
            {
                InitializeComponent();
                var context = new BlooddonationsupportsystemContext();
                var userRepo = new UserTableRepository(context);
                _authService = new AuthenticationService(userRepo);
                _userService = new UserService(userRepo);
            }

            private void LoginButton_Click(object sender, RoutedEventArgs e)
            {
                string phoneNumber = PhoneNumberTextBox.Text;
                string password = PasswordBox.Password;
                var user = _authService.Login(phoneNumber, password);
                if (user == null)
                {
                    MessageBox.Show("Sai số điện thoại hoặc mật khẩu!", "Lỗi đăng nhập", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                switch (user.RoleId)
                {
                    case 2:
                        new MemberWindow(user, _userService).Show();
                        break;
                    case 3:
                        new StaffWindow().Show();
                        break;
                    case 4:
                        new AdminWindow().Show();
                        break;
                    default:
                        MessageBox.Show("Tài khoản không có quyền truy cập!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                }
                this.Close();
            }
        }
    } 