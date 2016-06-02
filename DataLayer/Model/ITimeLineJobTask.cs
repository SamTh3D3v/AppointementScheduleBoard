using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Enums;

namespace DataLayer.Model
{
    public interface ITimeLineJobTask
    {
        DateTime? StartTime { get; set; }
        DateTime? EndTime { get; set; }
        StatusEnum Status { get; set; }
        Boolean TimelineViewExpanded { get; set; }
    }
}
