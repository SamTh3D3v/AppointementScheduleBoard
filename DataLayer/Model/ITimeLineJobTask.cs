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
        int Id { get; set; }
        string JobType { get; set; }
        DateTime ReceptionTime { get; set; }
        DateTime PDT { get; set; }
        bool IsClientWaiting { get; set; }
        string Status { get; set; }
        DateTime PlannedStartTime { get; set; }
        DateTime ActualStartTime { get; set; }
        DateTime? StartTime { get; set; }
        DateTime? EndTime { get; set; }
        Boolean TimelineViewExpanded { get; set; }
    }
}
