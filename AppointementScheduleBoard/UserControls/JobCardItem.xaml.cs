using System;
using System.Collections.Generic;
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

namespace AppointementScheduleBoard.UserControls
{
    
    public partial class JobCardItem : UserControl
    {
        public Brush ShipBrush
        {
            get { return (Brush)GetValue(ShipBrushProperty); }
            set { SetValue(ShipBrushProperty, value); }
        }


        public static readonly DependencyProperty ShipBrushProperty =
            DependencyProperty.Register("ShipBrush", typeof(Brush), typeof(JobCardItem), new UIPropertyMetadata(null));


        public JobCardItem()
        {
            InitializeComponent();            
        }
    }
}
