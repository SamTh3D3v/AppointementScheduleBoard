using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class WorkingHoursSettings
    {
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
    }
}
