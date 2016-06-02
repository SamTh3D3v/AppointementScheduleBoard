using System;
using DataLayer.Enums;
using DataLayer.Model;

namespace AppointementScheduleBoard.Helpers
{
    public class HourJobCard:ITimeLineJobTask
    {
        public String HourDesignation { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public StatusEnum Status { get; set; }
        public bool TimelineViewExpanded { get; set; }
    }
}