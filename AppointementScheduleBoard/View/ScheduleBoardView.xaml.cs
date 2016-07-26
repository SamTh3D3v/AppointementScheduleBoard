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
using GalaSoft.MvvmLight.Messaging;

namespace AppointementScheduleBoard.View
{
    public partial class ScheduleBoardView : Page
    {
        private readonly DispatcherTimer _dispatcherTimer;
        public ScheduleBoardView()
        {
            InitializeComponent();
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += dispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Start();
            Messenger.Default.Register<bool>(this, "CenterView", o =>
            {
                if (o) _dispatcherTimer.Start();
                else
                {
                    _dispatcherTimer.Stop();
                    MainSv.ScrollToLeftEnd();

                }
            });
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (TimeLineHeader.Items == null) return;
            if (TimeLineHeader.Items.Count == 0) return;

            double secondsPerUnit = 10 * (1000 / TimeLineHeader.UnitSize) * 60;
            var timeItemWidth = TimeLineHeader.ActualWidth / TimeLineHeader.Items.Count;
            double currentTimeMarginOffset = ((DateTime.Now - TimeLineHeader.StartDate).TotalSeconds / secondsPerUnit) * timeItemWidth;

            var margin = BorderLine.Margin;
            margin.Left = currentTimeMarginOffset;
            BorderLine.Margin = margin;

            var halfTheViewPortSize = MainSv.ActualWidth / 2;
            MainSv.ScrollToHorizontalOffset(currentTimeMarginOffset - halfTheViewPortSize);
        }
    }
}
