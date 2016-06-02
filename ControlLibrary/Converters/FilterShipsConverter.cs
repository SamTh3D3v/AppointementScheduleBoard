using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ControlLibrary.Helpers;
using DataLayer.Enums;
using DataLayer.Model;

namespace ControlLibrary.Converters
{
    public class FilterShipsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool) values[1])
                parameter = TimeLineFilter.Both;
            switch ((TimeLineFilter)parameter)
            {
                case TimeLineFilter.Planned:
                    var plannedTasks= 
              ((ObservableCollection<ITimeLineJobTask >)values[0]).Where(
                  j => j.Status == StatusEnum.Booked || j.Status == StatusEnum.Received).ToList();
                    return new ObservableCollection<ITimeLineJobTask>(plannedTasks);
                case TimeLineFilter.Actual:
                    var actualTasks =
              ((ObservableCollection<ITimeLineJobTask>)values[0]).Where(
                  j => j.Status != StatusEnum.Booked && j.Status != StatusEnum.Received).ToList();
                    return new ObservableCollection<ITimeLineJobTask>(actualTasks);
                default:
                    return values[0];

            }

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
