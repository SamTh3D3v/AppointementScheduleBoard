using System;
using System.Windows.Media;
using DataLayer.Enums;
using DataLayer.Model;

namespace AppointementScheduleBoard.Helpers
{
    public class HourJobCard:ITimeLineJobTask
    {
        public String HourDesignation { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int Id { get; set; }
        public string JobType { get; set; }
        public DateTime ReceptionTime { get; set; }
        public DateTime PDT { get; set; }
        public bool IsClientWaiting { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public DateTime? PlannedStartTime { get; set; }
        public bool TimelineViewExpanded { get; set; }
        public String JobTaskBackGround { get; set; }
    }
}