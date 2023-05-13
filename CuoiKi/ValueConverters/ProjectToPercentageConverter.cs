using CuoiKi.Controllers;
using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace CuoiKi.ValueConverters
{
    public class ProjectToPercentageConverter : IValueConverter
    {
        private DbController _dbController;
        public ProjectToPercentageConverter()
        {
            _dbController = new DbController();
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Project project)
            {
                List<Task>? tasks = new List<Task>();
                tasks = _dbController.GetAllTaskOfProject(project.ID);
                if (tasks is null) return 0;
                if (tasks.Count == 0) return 0;
                int percentDone = (tasks.Where(t => t.Status == Constants.TaskStatus.Done).Count() * 100 / tasks.Count);
                return percentDone;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
