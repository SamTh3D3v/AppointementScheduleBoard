using System;

namespace DataLayer.Model
{
    //this Settings are from the EBS
    public class ServerSettings
    {
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
    }
}