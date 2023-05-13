using CuoiKi.Controllers;
using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace CuoiKi.ValueConverters
{
    public class ProjectToColorConverter : IValueConverter
    {
        private DbController _dbController;
        public ProjectToColorConverter()
        {
            _dbController = new DbController();
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Project project)
            {
                List<Task>? tasks = new List<Task>();
                tasks = _dbController.GetAllTaskOfProject(project.ID);
                if (tasks.Count == 0) return 0;
                int percentDone = (tasks.Where(t => t.Status == Constants.TaskStatus.Done).Count() * 100 / tasks.Count);
                if (percentDone < 30)
                {
                    return Brushes.Red;
                }
                else if (percentDone < 50)
                {
                    return new SolidColorBrush(Color.FromRgb(255, 172, 0));
                }
                else if (percentDone < 80)
                {
                    return new SolidColorBrush(Color.FromRgb(249, 255, 85));
                }
                else
                {
                    return new SolidColorBrush(Color.FromRgb(0, 255, 25));
                }
            }
            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
