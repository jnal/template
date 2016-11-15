using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Crossover.LBS.API.Contracts.DTO;
using Crossover.LBS.Client.API;
using Crossover.LBS.Client.DTO;
using RestSharp;

namespace Crossover.LBS.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _ipAddress;
        private TaskQueue _taskQueue;
        private List<BackupDto> _scheduleBackup;


        public MainWindow()
        {
            InitializeComponent();

            _ipAddress = BackupHelper.GetLocalIPAddress();

            _scheduleBackup = LBSMachineApi.GetBackupSchedule(_ipAddress);
            dgSchedule.ItemsSource = _scheduleBackup;

            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();

            _taskQueue = new TaskQueue();
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (_scheduleBackup != null)
            {

                // every 10 seconds check if schedule backup will be hit
                foreach (var backupDto in _scheduleBackup.Where(
                            x => x.Schedule.Date == DateTime.Now.Date && x.Schedule.Hour == DateTime.Now.Hour && x.Schedule.Minute == DateTime.Now.Minute)
                        .OrderBy(x => x.Schedule)
                        .ToList()
                )
                {
                    _taskQueue.ScheduleTask(() => BackupHelper.DoBackup(backupDto));
                    _scheduleBackup.Remove(backupDto);
                };
            }
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            var backup = (BackupDto)((Button)e.Source).DataContext;
            _taskQueue.ScheduleTask(() => BackupHelper.DoBackup(backup));

        }

    }
}
