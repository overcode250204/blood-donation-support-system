using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DAL.Entities;
using BLL.Services;
using DAL.Repositories;

namespace BloodDonationSupportSystem
{
    public partial class BloodDonationScheduleWindow : Window
    {
        private UserTable _user;
        private List<BloodDonationSchedule> _schedules;
        private BloodDonationScheduleService _service;
        public BloodDonationScheduleWindow(UserTable user)
        {
            InitializeComponent();
            _user = user;
            var context = new DAL.Entities.BlooddonationsupportsystemContext();
            var scheduleRepo = new BloodDonationScheduleRepository(context);
            var regRepo = new BloodDonationRegistrationRepository(context);
            _service = new BloodDonationScheduleService(scheduleRepo, regRepo);
            LoadSchedules();
        }

        private void LoadSchedules()
        {
            _schedules = _service.GetFutureSchedules();
            ScheduleListView.ItemsSource = _schedules;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = ScheduleListView.SelectedItem as BloodDonationSchedule;
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn một lịch để đăng ký!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string reason;
            if (!_service.CanRegister(_user.UserId, selected.BloodDonationScheduleId, selected.DonationDate, out reason))
            {
                MessageBox.Show(reason, "Không thể đăng ký", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _service.Register(_user.UserId, selected.BloodDonationScheduleId, selected.DonationDate);
            MessageBox.Show($"Đăng ký thành công cho lịch tại {selected.AddressHospital} ngày {selected.DonationDate:dd/MM/yyyy}", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} 