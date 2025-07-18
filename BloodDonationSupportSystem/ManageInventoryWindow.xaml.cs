using System.Windows;
using System.Collections.Generic;
using DAL.Entities;
using System.Linq;

namespace BloodDonationSupportSystem
{
    public partial class ManageInventoryWindow : Window
    {
        public ManageInventoryWindow()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            var context = new BlooddonationsupportsystemContext();
            List<BloodInventory> inventories = context.BloodInventories.ToList();
            List<DonationProcess> processes = context.DonationProcesses.ToList();
            BloodInventoryListView.ItemsSource = inventories;
            ProcessListView.ItemsSource = processes;
        }
        private void ProcessListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selected = ProcessListView.SelectedItem as DAL.Entities.DonationProcess;
            if (selected == null) return;
            var dialog = new UpdateProcessDialog(selected.DonationProcessId);
            dialog.Owner = this;
            if (dialog.ShowDialog() == true)
            {
                LoadData();
            }
        }
    }
} 