using System.Windows;
using DAL.Entities;
using BLL.Services;

namespace BloodDonationSupportSystem
{
    public partial class MemberWindow : Window
    {
        private UserTable _user;
        private AuthenticationService _authService;
        private UserService _userService;
        public MemberWindow(UserTable user, UserService userService)
          
        {
            InitializeComponent();
            _user = user;
            _userService = userService;
        }
        public MemberWindow() : this(null, null) { }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            var accountWindow = new AccountWindow(_user, _userService);
            accountWindow.Owner = this;
            accountWindow.ShowDialog();
        }

        private void RegisterBloodButton_Click(object sender, RoutedEventArgs e)
        {
            var scheduleWindow = new BloodDonationSupportSystem.BloodDonationScheduleWindow(_user);
            scheduleWindow.Owner = this;
            scheduleWindow.ShowDialog();
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            var context = new DAL.Entities.BlooddonationsupportsystemContext();
            var scheduleRepo = new DAL.Repositories.BloodDonationScheduleRepository(context);
            var regRepo = new DAL.Repositories.BloodDonationRegistrationRepository(context);
            var service = new BLL.Services.BloodDonationScheduleService(scheduleRepo, regRepo);
            var historyWindow = new BloodDonationHistoryWindow(_user, service);
            historyWindow.Owner = this;
            historyWindow.ShowDialog();
        }
    }
} 