using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using DataLayer.Model;

namespace AppointementScheduleBoard.View
{
    public partial class ScheduleBoardView : Page
    {
        public ScheduleBoardView()
        {
            InitializeComponent();
            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {            
            if(TimeLineHeader.Items==null) return;
            if(TimeLineHeader.Items.Count==0) return;

            double secondsPerUnit = 10 * (1000 / TimeLineHeader.UnitSize) * 60;
            var timeItemWidth = TimeLineHeader.ActualWidth / TimeLineHeader.Items.Count;
            double currentTimeMarginOffset = ((DateTime.Now - TimeLineHeader.StartDate).TotalSeconds / secondsPerUnit) * timeItemWidth;

            var margin = BorderLine.Margin;
            margin.Left = currentTimeMarginOffset;
            BorderLine.Margin = margin;

            var halfTheViewPortSize = MainSv.ActualWidth /2;
            MainSv.ScrollToHorizontalOffset(currentTimeMarginOffset - halfTheViewPortSize);
        }
    }
}
