﻿using CuoiKi.ViewModels;
using System.Windows.Controls;

namespace CuoiKi.UI.Staff
{
    /// <summary>
    /// Interaction logic for WorkSessionForm.xaml
    /// </summary>
    public partial class WorkSessionForm : Page
    {
        public WorkSessionForm()
        {
            InitializeComponent();
            this.DataContext = new WorkSessionPageViewModel();
        }

        private void dteSelectedMonth_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            dteSelectedMonth.DisplayMode = CalendarMode.Year;
        }
    }
}
