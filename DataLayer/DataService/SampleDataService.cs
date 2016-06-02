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
            var refDateTime = DateTime.Now.Date.AddHours(6);
            //to be reimplemented to return the real data from the EBS
            return new List<Stall>()
            {
                new Stall()
                {
                    Id = Guid.NewGuid(),
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
                            TimelineViewExpanded = true,
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=Guid.NewGuid(),
                                    Name = "Technicien 1"
                                }
                            }
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
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=Guid.NewGuid(),
                                    Name = "Technicien 2"
                                }
                            }
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
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=Guid.NewGuid(),
                                    Name = "Technicien 3"
                                }
                            }
                        }
                    }
                },
                new Stall()
                {
                    Id = Guid.NewGuid(),
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
                            Status = StatusEnum.Booked,
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=Guid.NewGuid(),
                                    Name = "Technicien 1"
                                }
                            }
                        },new JobTask()
                        {
                            Id = Guid.NewGuid(),
                             StartTime = refDateTime.AddHours(1.6),
                            EndTime = refDateTime.AddHours(2),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA10,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.InProgress,
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=Guid.NewGuid(),
                                    Name = "Technicien 2"
                                }
                            }
                        }
                    }
                },
                new Stall()
                {
                    Id = Guid.NewGuid(),
                    StallName = "Stall 2",
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
                            Status = StatusEnum.Booked,
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=Guid.NewGuid(),
                                    Name = "Technicien 1"
                                }
                            }
                        },new JobTask()
                        {
                            Id = Guid.NewGuid(),
                            StartTime = refDateTime.AddHours(1.3),
                            EndTime = refDateTime.AddHours(2.5),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA10,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.InProgress,
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=Guid.NewGuid(),
                                    Name = "Technicien 2"
                                }
                            }
                        },new JobTask()
                        {
                            Id = Guid.NewGuid(),
                            StartTime = refDateTime.AddHours(2.6),
                            EndTime = refDateTime.AddHours(4),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA20,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.Received,
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=Guid.NewGuid(),
                                    Name = "Technicien 3"
                                }
                            }
                        }
                    }
                },
                new Stall()
                {
                    Id = Guid.NewGuid(),
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
                            Status = StatusEnum.Booked,
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=Guid.NewGuid(),
                                    Name = "Technicien 1"
                                }
                            }
                        },new JobTask()
                        {
                            Id = Guid.NewGuid(),
                             StartTime = refDateTime.AddHours(3),
                            EndTime = refDateTime.AddHours(4),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA10,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.InProgress,
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=Guid.NewGuid(),
                                    Name = "Technicien 2"
                                }
                            }
                        }
                    }
                },
                new Stall()
                {
                    Id = Guid.NewGuid(),
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
                            Status = StatusEnum.Booked,
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=Guid.NewGuid(),
                                    Name = "Technicien 1"
                                }
                            }
                        },new JobTask()
                        {
                            Id = Guid.NewGuid(),
                            StartTime = refDateTime.AddHours(2),
                            EndTime = refDateTime.AddHours(2.5),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA10,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.InProgress,
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=Guid.NewGuid(),
                                    Name = "Technicien 2"
                                }
                            }
                        },new JobTask()
                        {
                            Id = Guid.NewGuid(),
                            StartTime = refDateTime.AddHours(5),
                            EndTime = refDateTime.AddHours(6),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA20,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.Received,
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=Guid.NewGuid(),
                                    Name = "Technicien 3"
                                }
                            }
                        }
                    }
                },
                new Stall()
                {
                    Id = Guid.NewGuid(),
                    StallName = "Stall 6",
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
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=Guid.NewGuid(),
                                    Name = "Technicien 1"
                                }
                            }
                        },new JobTask()
                        {
                            Id = Guid.NewGuid(),
                             StartTime = refDateTime.AddHours(3),
                            EndTime = refDateTime.AddHours(4),
                            IsClientWaiting = true,
                            JobType = JobTypesEnum.PMA10,
                            PDT = DateTime.Now,
                            ReceptionTime = DateTime.Now,
                            Status = StatusEnum.InProgress,
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id=Guid.NewGuid(),
                                    Name = "Technicien 2"
                                }
                            }
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
                StartHour = new TimeSpan(6,0,0),
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

        public SampleDataService():base()
        {
            
        }
    }
}
