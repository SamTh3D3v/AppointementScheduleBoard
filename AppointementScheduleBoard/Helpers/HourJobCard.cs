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
        public DateTime? ResolvedDate { get; set; }
        public int Id { get; set; }
        public string Sevirity { get; set; }
        public string JobType { get; set; }
        int ITimeLineJobTask.TaskDuration { get; set; }
        public decimal TaskDuration { get; set; }
        public int? IncidentId { get; set; }
        public DateTime? ClockIn { get; set; }
        public DateTime? ClockOut { get; set; }
        public int MechanicsCount { get; set; }
        public DateTime ReceptionTime { get; set; }
        public DateTime? PDT { get; set; }
        public bool IsClientWaiting { get; set; }     
        public string BookingNumber { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public DateTime? PlannedStartTime { get; set; }
        public bool TimelineViewExpanded { get; set; }
        public String JobTaskBackGround { get; set; }
        public bool IsJobTaskBliking { get; set; }
    }
}