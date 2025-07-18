using System.Windows;
using System.Collections.Generic;
using DAL.Entities;
using System.Linq;

namespace BloodDonationSupportSystem
{
    public partial class ManageSchedulesWindow : Window
    {
        private List<BloodDonationSchedule> _schedules;
        public ManageSchedulesWindow()
        {
            InitializeComponent();
            LoadSchedules();
        }
        private void LoadSchedules()
        {
            var context = new BlooddonationsupportsystemContext();
            _schedules = context.BloodDonationSchedules.ToList();
            ScheduleListView.ItemsSource = _schedules;
        }
        private void AddScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddScheduleDialog();
            dialog.Owner = this;
            if (dialog.ShowDialog() == true)
            {
                LoadSchedules();
            }
        }
    }
} 