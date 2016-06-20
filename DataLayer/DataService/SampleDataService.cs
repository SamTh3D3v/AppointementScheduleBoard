using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DataLayer.Enums;
using DataLayer.Model;

namespace DataLayer.DataService 
{    
    public class SampleDataService: DataService
    {       
        public override List<Stall> GetStallsCollection()
        {
            
            var refDateTime = DateTime.Today.Add(GetServerSettings().StartHour); ;
            //to be reimplemented to return the real data from the EBS
            return new List<Stall>()
            {
                new Stall()
                {
                    Id = 1,
                    BranchId = "Branch ID 1",
                    StallName = "Stall 1",
                    JobTasksCollection =new ObservableCollection<ITimeLineJobTask>()
                    {
                        new JobTask()
                        {                            
                            Id = Guid.NewGuid(),
                            StartTime = refDateTime,
                            EndTime = refDateTime.AddHours(.5),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA05,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.Booked,
                            TimelineViewExpanded = true
                            
                        },new JobTask()
                        {
                            Id = Guid.NewGuid(),
                            StartTime = refDateTime.AddHours(.5),
                            EndTime = refDateTime.AddHours(.8),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA10,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.InProgress,
                            
                        },new JobTask()
                        {
                            Id = Guid.NewGuid(),
                            StartTime = refDateTime.AddHours(1),
                            EndTime = refDateTime.AddHours(2),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA20,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.Received,
                           
                        }
                    }
                    ,
                    Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=1,
                                    Name = "Technicien 1"
                                }
                            }
                },
                new Stall()
                {
                    Id = 2,
                    BranchId = "Branch ID 2",
                    StallName = "Stall 2",
                    JobTasksCollection =new ObservableCollection<ITimeLineJobTask>()
                    {
                        new JobTask()
                        {                            
                            Id = Guid.NewGuid(),
                             StartTime = refDateTime.AddHours(.8),
                            EndTime = refDateTime.AddHours(1.5),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA05,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.Booked
                           
                        },new JobTask()
                        {
                            Id = Guid.NewGuid(),
                             StartTime = refDateTime.AddHours(1.6),
                            EndTime = refDateTime.AddHours(2),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA10,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.InProgress
                           
                        }
                        
                       
                    },
                     Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=2,
                                    Name = "Technicien 2"
                                }
                            }
                },
                new Stall()
                {
                    Id = 3,
                    BranchId = "Branch ID 3",
                    StallName = "Stall 3",
                    JobTasksCollection =new ObservableCollection<ITimeLineJobTask>()
                    {
                        new JobTask()
                        {
                            Id = Guid.NewGuid(),
                            StartTime = refDateTime.AddHours(.2),
                            EndTime = refDateTime.AddHours(1.3),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA05,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.Booked
                            
                        },new JobTask()
                        {
                            Id = Guid.NewGuid(),
                            StartTime = refDateTime.AddHours(1.3),
                            EndTime = refDateTime.AddHours(2.5),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA10,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.InProgress
                        },new JobTask()
                        {
                            Id = Guid.NewGuid(),
                            StartTime = refDateTime.AddHours(2.6),
                            EndTime = refDateTime.AddHours(4),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA20,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.Received                           
                        }

                    },
                    Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=3,
                                    Name = "Technicien 3"
                                },
                        new Technicien()
                                {
                                    Id=4,
                                    Name = "Technicien 4"
                                }
                            }
                },
                new Stall()
                {
                    Id = 4,
                    BranchId = "Branch ID 1",
                    StallName = "Stall 4",
                    JobTasksCollection =new ObservableCollection<ITimeLineJobTask>()
                    {
                        new JobTask()
                        {
                            Id = Guid.NewGuid(),
                             StartTime = refDateTime,
                            EndTime = refDateTime.AddHours(.5),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA05,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.Booked
                            
                        },new JobTask()
                        {
                            Id = Guid.NewGuid(),
                             StartTime = refDateTime.AddHours(3),
                            EndTime = refDateTime.AddHours(4),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA10,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.InProgress
                           
                        }
                    },
                     Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=5,
                                    Name = "Technicien 5"
                                }
                            }
                },
                new Stall()
                {
                    Id = 5,
                    BranchId = "Branch ID 1",
                    StallName = "Stall 5",
                    JobTasksCollection =new ObservableCollection<ITimeLineJobTask>()
                    {
                        new JobTask()
                        {
                            Id = Guid.NewGuid(),
                            StartTime = refDateTime,
                            EndTime = refDateTime.AddHours(.5),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA05,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.Booked
                            
                        },new JobTask()
                        {
                            Id = Guid.NewGuid(),
                            StartTime = refDateTime.AddHours(2),
                            EndTime = refDateTime.AddHours(2.5),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA10,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.InProgress
                        },new JobTask()
                        {
                            Id = Guid.NewGuid(),
                            StartTime = refDateTime.AddHours(5),
                            EndTime = refDateTime.AddHours(6),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA20,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.Received
                            
                        }
                    },
                    Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=6,
                                    Name = "Technicien 6"
                                }
                            }
                },
                new Stall()
                {
                    Id = 6,
                    StallName = "Stall 6",
                    BranchId = "Branch ID 2",
                    JobTasksCollection =new ObservableCollection<ITimeLineJobTask>()
                    {
                        new JobTask()
                        {
                            Id = Guid.NewGuid(),
                             StartTime = refDateTime,
                            EndTime = refDateTime.AddHours(.5),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA05,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.Booked,
                           
                        },new JobTask()
                        {
                            Id = Guid.NewGuid(),
                             StartTime = refDateTime.AddHours(3),
                            EndTime = refDateTime.AddHours(4),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA10,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.InProgress
                           
                        }
                    },
                    Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=8,
                                    Name = "Technicien 8"
                                },
                                new Technicien()
                                {
                                    Id=9,
                                    Name = "Technicien 9"
                                }
                            }
                }

            };
        }

        public override ServerSettings GetServerSettings()
        {
            //This Data needs to be get from the EBS
            return new ServerSettings()
            {
                StartHour = new TimeSpan(1,0,0),
                EndHour = new TimeSpan(18,0,0)
            };
        }

        public override LocalSettings GetLocalSettings()
        {
            //Get those settings from the XML file
            return new LocalSettings()
            {
                IsClockFormat24 = true,
                RefreshTimeInMinutes = 0.1,
                UnitSize = 100,
                IsShipClientWaitingVisible = true,
                IsShipJobtypeVisible = true,
                IsShipPdtVisible = true,
                IsShipReceptionTimeVisible = true,
                IsShipStatusVisible = true,

                IsPlanActualHeaderVisible = true,
                IsPlanActualMerged = false,
                IsStallNamesVisible = true,
                IsTechnicientsNamesVisible = true,
                IsTimeHeaderVisible = true
            };
        }

        public static List<string> GetBranchIdsCollection()
        {
            return new List<string>()
            {
                "Branch ID 1",
                "Branch ID 2",
                "Branch ID 3",
                "Branch ID 4",
            };

       
        }
    }
}
