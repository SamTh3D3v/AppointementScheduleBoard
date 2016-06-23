﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DataLayer.Enums;
using DataLayer.Model;

namespace DataLayer.DataService 
{    
    public class SampleDataService: DataService
    {               

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

        public override List<Branch> GetAllBranchs()
        {
            return new List<Branch>()
            {
                new Branch()
                {
                  Id  = 1,
                 Name = "Branch ID 1"
                } , new Branch()
                {
                  Id  = 1,
                 Name = "Branch ID 2"
                } , new Branch()
                {
                  Id  = 1,
                 Name = "Branch ID 3"
                } , new Branch()
                {
                  Id  = 1,
                 Name = "Branch ID 4"
                }                 
            };

       
        }

        public override List<Technicien> GetAllTechnicians(int BRANCH_ID)
        {
            switch (BRANCH_ID)
            {
                case 1:
                    return new List<Technicien>()
                    {
                        new Technicien()
                        {
                            Id = 1,
                            Name = "Technician 1"
                        },new Technicien()
                        {
                            Id = 1,
                            Name = "Technician 2"
                        },new Technicien()
                        {
                            Id = 1,
                            Name = "Technician 3"
                        },new Technicien()
                        {
                            Id = 1,
                            Name = "Technician 4"
                        },
                    };
                    break;
                default:
                    return null;
                    break;
            }
        }

        public override List<Stall> GetBranchStalls(int BRANCH_ID)
        {
            var refDateTime = DateTime.Today.Add(GetServerSettings().StartHour);
            switch (BRANCH_ID)
            {
                case 1:
                    return new List<Stall>()
                    {
                        new Stall()
                        {
                            Id = 1,
                            BranchId = 1,
                            StallName = "Stall 1",
                            JobTasksCollection = new ObservableCollection<ITimeLineJobTask>()
                            {
                                new JobTask()
                                {
                                    Id = 1,
                                    StartTime = refDateTime,
                                    EndTime = refDateTime.AddHours(.5),
                                    IsClientWaiting = true,
                                    JobType = "PMA05",
                                    PDT = DateTime.Now,
                                    ReceptionTime = DateTime.Now,
                                    Status = "Booked",
                                    TimelineViewExpanded = true

                                },
                                new JobTask()
                                {
                                    Id = 2,
                                    StartTime = refDateTime.AddHours(.5),
                                    EndTime = refDateTime.AddHours(.8),
                                    IsClientWaiting = true,
                                    JobType = "PMA10",
                                    PDT = DateTime.Now,
                                    ReceptionTime = DateTime.Now,
                                    Status = "InProgress",

                                },
                                new JobTask()
                                {
                                    Id = 3,
                                    StartTime = refDateTime.AddHours(1),
                                    EndTime = refDateTime.AddHours(2),
                                    IsClientWaiting = true,
                                    JobType = "PMA20",
                                    PDT = DateTime.Now,
                                    ReceptionTime = DateTime.Now,
                                    Status = "Received",

                                }
                            }
                            ,
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id = 1,
                                    Name = "Technicien 1"
                                }
                            }
                        },
                        new Stall()
                        {
                            Id = 2,
                            BranchId = 1,
                            StallName = "Stall 2",
                            JobTasksCollection = new ObservableCollection<ITimeLineJobTask>()
                            {
                                new JobTask()
                                {
                                    Id = 4,
                                    StartTime = refDateTime.AddHours(.8),
                                    EndTime = refDateTime.AddHours(1.5),
                                    IsClientWaiting = true,
                                    JobType = "PMA05",
                                    PDT = DateTime.Now,
                                    ReceptionTime = DateTime.Now,
                                    Status = "Booked"

                                },
                                new JobTask()
                                {
                                    Id = 5,
                                    StartTime = refDateTime.AddHours(1.6),
                                    EndTime = refDateTime.AddHours(2),
                                    IsClientWaiting = true,
                                    JobType = "PMA10",
                                    PDT = DateTime.Now,
                                    ReceptionTime = DateTime.Now,
                                    Status = "InProgress"

                                }


                            },
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id = 2,
                                    Name = "Technicien 2"
                                }
                            }
                        },
                        new Stall()
                        {
                            Id = 3,
                            BranchId = 1,
                            StallName = "Stall 3",
                            JobTasksCollection = new ObservableCollection<ITimeLineJobTask>()
                            {
                                new JobTask()
                                {
                                    Id = 6,
                                    StartTime = refDateTime.AddHours(.2),
                                    EndTime = refDateTime.AddHours(1.3),
                                    IsClientWaiting = true,
                                    JobType = "PMA05",
                                    PDT = DateTime.Now,
                                    ReceptionTime = DateTime.Now,
                                    Status = "Booked"

                                },
                                new JobTask()
                                {
                                    Id = 7,
                                    StartTime = refDateTime.AddHours(1.3),
                                    EndTime = refDateTime.AddHours(2.5),
                                    IsClientWaiting = true,
                                    JobType = "PMA10",
                                    PDT = DateTime.Now,
                                    ReceptionTime = DateTime.Now,
                                    Status = "InProgress"
                                },
                                new JobTask()
                                {
                                    Id = 8,
                                    StartTime = refDateTime.AddHours(2.6),
                                    EndTime = refDateTime.AddHours(4),
                                    IsClientWaiting = true,
                                    JobType = "PMA20",
                                    PDT = DateTime.Now,
                                    ReceptionTime = DateTime.Now,
                                    Status = "Received"
                                }

                            },
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id = 3,
                                    Name = "Technicien 3"
                                },
                                new Technicien()
                                {
                                    Id = 4,
                                    Name = "Technicien 4"
                                }
                            }
                        },
                        new Stall()
                        {
                            Id = 4,
                            BranchId = 1,
                            StallName = "Stall 4",
                            JobTasksCollection = new ObservableCollection<ITimeLineJobTask>()
                            {
                                new JobTask()
                                {
                                    Id = 9,
                                    StartTime = refDateTime,
                                    EndTime = refDateTime.AddHours(.5),
                                    IsClientWaiting = true,
                                    JobType = "PMA05",
                                    PDT = DateTime.Now,
                                    ReceptionTime = DateTime.Now,
                                    Status = "Booked"

                                },
                                new JobTask()
                                {
                                    Id = 10,
                                    StartTime = refDateTime.AddHours(3),
                                    EndTime = refDateTime.AddHours(4),
                                    IsClientWaiting = true,
                                    JobType = "PMA10",
                                    PDT = DateTime.Now,
                                    ReceptionTime = DateTime.Now,
                                    Status = "InProgress"

                                }
                            },
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id = 5,
                                    Name = "Technicien 5"
                                }
                            }
                        },
                        new Stall()
                        {
                            Id = 5,
                            BranchId = 1,
                            StallName = "Stall 5",
                            JobTasksCollection = new ObservableCollection<ITimeLineJobTask>()
                            {
                                new JobTask()
                                {
                                    Id = 11,
                                    StartTime = refDateTime,
                                    EndTime = refDateTime.AddHours(.5),
                                    IsClientWaiting = true,
                                    JobType = "PMA05",
                                    PDT = DateTime.Now,
                                    ReceptionTime = DateTime.Now,
                                    Status = "Booked"

                                },
                                new JobTask()
                                {
                                    Id = 12,
                                    StartTime = refDateTime.AddHours(2),
                                    EndTime = refDateTime.AddHours(2.5),
                                    IsClientWaiting = true,
                                    JobType = "PMA10",
                                    PDT = DateTime.Now,
                                    ReceptionTime = DateTime.Now,
                                    Status = "InProgress"
                                },
                                new JobTask()
                                {
                                    Id = 13,
                                    StartTime = refDateTime.AddHours(5),
                                    EndTime = refDateTime.AddHours(6),
                                    IsClientWaiting = true,
                                    JobType = "PMA20",
                                    PDT = DateTime.Now,
                                    ReceptionTime = DateTime.Now,
                                    Status = "Received"

                                }
                            },
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id = 6,
                                    Name = "Technicien 6"
                                }
                            }
                        },
                        new Stall()
                        {
                            Id = 6,
                            StallName = "Stall 6",
                            BranchId = 1,
                            JobTasksCollection = new ObservableCollection<ITimeLineJobTask>()
                            {
                                new JobTask()
                                {
                                    Id = 14,
                                    StartTime = refDateTime,
                                    EndTime = refDateTime.AddHours(.5),
                                    IsClientWaiting = true,
                                    JobType = "PMA05",
                                    PDT = DateTime.Now,
                                    ReceptionTime = DateTime.Now,
                                    Status = "Booked",

                                },
                                new JobTask()
                                {
                                    Id = 15,
                                    StartTime = refDateTime.AddHours(3),
                                    EndTime = refDateTime.AddHours(4),
                                    IsClientWaiting = true,
                                    JobType = "PMA10",
                                    PDT = DateTime.Now,
                                    ReceptionTime = DateTime.Now,
                                    Status = "InProgress"

                                }
                            },
                            Techniciens = new ObservableCollection<Technicien>()
                            {
                                new Technicien()
                                {
                                    Id = 8,
                                    Name = "Technicien 8"
                                },
                                new Technicien()
                                {
                                    Id = 9,
                                    Name = "Technicien 9"
                                }
                            }
                        }
                    };
                    

                    break;
                default:
                    return null;
                    break;

            }
        }
    }
}
