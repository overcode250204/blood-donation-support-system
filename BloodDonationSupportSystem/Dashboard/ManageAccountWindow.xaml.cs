using BLL.Services;
using DAL.Entities;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BloodDonationSupportSystem.Dashboard
{
    public partial class ManageAccountWindow : Window
    {
        private readonly AccountService _accountService;

        public ObservableCollection<RoleUser> Roles { get; set; } = new ObservableCollection<RoleUser>();
        public ObservableCollection<UserTable> Users { get; set; } = new ObservableCollection<UserTable>();

        private int defaultRoleId;

        public ManageAccountWindow()
        {
            InitializeComponent();
            _accountService = new AccountService();

            DataContext = this;

            Loaded += ManageAccountWindow_Loaded;
        }

        private async void ManageAccountWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                // Clear existing data
                Roles.Clear();
                Users.Clear();

                var roles = await _accountService.GetAllRolesAsync();
                foreach (var r in roles)
                {
                    Roles.Add(r);
                }

                var users = await _accountService.GetAllUsersAsync();
                foreach (var u in users)
                {
                    Users.Add(u);
                }

                if (Roles.Any())
                {
                    defaultRoleId = Roles.First().RoleId;
                }

                Debug.WriteLine($"Loaded {Roles.Count} roles and {Users.Count} users");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var user in Users)
                {
                    if (Roles.Any())
                        defaultRoleId = Roles.First().RoleId;

                    if (string.IsNullOrWhiteSpace(user.Status))
                        user.Status = "Active";

                    if (user.UserId == Guid.Empty)
                        user.UserId = Guid.NewGuid();

                    if (user.CreatedAt == null)
                        user.CreatedAt = DateTime.Now;

                    user.UpdatedAt = DateTime.Now;
                }

                await _accountService.SaveUsersAsync(Users);
                MessageBox.Show("Lưu dữ liệu thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadDataAsync();
        }

        private void UsersDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && UsersDataGrid.SelectedItem is UserTable user)
            {
                var result = MessageBox.Show($"Bạn có chắc muốn xóa tài khoản '{user.FullName}'?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    Users.Remove(user);
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void UsersDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            // Có thể thêm tự động save nếu muốn
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateAccountWindow createWindow = new CreateAccountWindow();
            createWindow.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
