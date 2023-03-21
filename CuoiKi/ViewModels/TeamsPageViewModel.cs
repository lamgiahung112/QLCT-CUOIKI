using CuoiKi.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.ViewModels
{
    public class TeamsPageViewModel : ViewModelBase
    {
        private KpiController _controller;

        public TeamsPageViewModel()
        {
            _controller = new KpiController();
        }
     
    }
}
