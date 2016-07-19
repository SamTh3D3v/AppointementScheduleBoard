using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using DataLayer.Enums;

namespace DataLayer.Model
{
    public interface ITimeLineJobTask
    {
        string BookingNumber { get; set; }
        DateTime BookingDate { get; set; }
        string Status { get; set; }
        int StatusId { get; set; }
        string Sevirity { get; set; }
        string JobType { get; set; }
        Decimal TaskDuration { get; set; }

        int? IncidentId { get; set; }
        DateTime? ClockIn { get; set; }
        DateTime? ClockOut { get; set; }

        bool IsClientWaiting { get; set; }
        DateTime? PDT { get; set; }
        DateTime? ResolvedDate { get; set; }

        //Depricated 
        int Id { get; set; }
        //Depricated 
        DateTime ReceptionTime { get; set; }
        //Depricated
        DateTime? PlannedStartTime { get; set; }
        //Depricated 
        DateTime? ActualStartTime { get; set; }


        //calculated Min and max
        DateTime? StartTime { get; set; }
        DateTime? EndTime { get; set; }

        Boolean TimelineViewExpanded { get; set; }
        String JobTaskBackGround { get; set; }
        bool IsJobTaskBliking { get; set; }


        
        


        
        
        



    }
}
