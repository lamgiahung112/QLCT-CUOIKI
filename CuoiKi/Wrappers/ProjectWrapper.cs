using CuoiKi.Models;
using System.Windows.Media;

namespace CuoiKi.Wrappers
{
    public class ProjectWrapper
    {
        private readonly Project _project;
        public ProjectWrapper(Project project)
        {
            _project = project;
            PercentDone = 0;
            TootipString = "";
            BarColor = new SolidColorBrush(Colors.Gray);
        }
        public string ID => _project.ID;
        public string Name => _project.Name;
        public string Description => _project.Description;
        public int PercentDone { get; set; }
        public string TootipString { get; set; }
        public SolidColorBrush BarColor { get; set; }
        public void InitializeUI(int percentDone)
        {
            PercentDone = percentDone;
            if (percentDone < 30)
            {
                BarColor = Brushes.Red;
            }
            else if (percentDone < 50)
            {
                BarColor = new SolidColorBrush(Color.FromRgb(255, 172, 0));
            }
            else if (percentDone < 80)
            {
                BarColor = new SolidColorBrush(Color.FromRgb(249, 255, 85));
            }
            else
            {
                BarColor = new SolidColorBrush(Color.FromRgb(0, 255, 25));
            }
        }
    }
}
