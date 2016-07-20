using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using AppointementScheduleBoard.Helpers;
using DataLayer;
using DataLayer.DataService;
using DataLayer.Enums;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace AppointementScheduleBoard.ViewModel
{
    public class ScheduleBoardViewModel : NavigableViewModelBase
    {
        #region Fields  
        private bool _isPrograssRingActive;
        private ObservableCollection<Stall> _sallsCollection;
        private double _timeLineUnitSize = 1000;
        private ObservableCollection<ITimeLineJobTask> _hoursCollection;
        private DateTime _startDateTime;
        private DateTime _endDateTime;
        private DispatcherTimer _dispatcherTimer;
        #endregion
        #region Properties 
        public bool IsPrograssRingActive
        {
            get
            {
                return _isPrograssRingActive;
            }

            set
            {
                if (_isPrograssRingActive == value)
                {
                    return;
                }

                _isPrograssRingActive = value;
                RaisePropertyChanged();
            }
        }
        public double TimeLineUnitSize
        {
            get
            {
                return _timeLineUnitSize;
            }

            set
            {
                if (_timeLineUnitSize == value)
                {
                    return;
                }

                _timeLineUnitSize = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Stall> StallsCollection
        {
            get
            {
                return _sallsCollection;
            }

            set
            {
                if (_sallsCollection == value)
                {
                    return;
                }

                _sallsCollection = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<ITimeLineJobTask> HoursCollection
        {
            get
            {
                return _hoursCollection;
            }

            set
            {
                if (_hoursCollection == value)
                {
                    return;
                }

                _hoursCollection = value;
                RaisePropertyChanged();
            }
        }
        public DateTime StartDateTime
        {
            get
            {
                return _startDateTime;
            }

            set
            {
                if (_startDateTime == value)
                {
                    return;
                }

                _startDateTime = value;
                RaisePropertyChanged();
            }
        }
        public DateTime EndDateTime
        {
            get
            {
                return _endDateTime;
            }

            set
            {
                if (_endDateTime == value)
                {
                    return;
                }

                _endDateTime = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _scheduleBoardLoadedCommand;
        public RelayCommand ScheduleBoardLoadedCommand
        {
            get
            {
                return _scheduleBoardLoadedCommand
                    ?? (_scheduleBoardLoadedCommand = new RelayCommand(async () =>
                    {
                        await ReloadBoard();
                    }));
            }
        }
        private RelayCommand _uniteSizeChangedCommand;
        public RelayCommand UnitSizehangedCommand
        {
            get
            {
                return _uniteSizeChangedCommand
                    ?? (_uniteSizeChangedCommand = new RelayCommand(async () =>
                    {
                        await UpdateHoursCollection();
                    }));
            }
        }
        #endregion
        #region Ctors and methods
        public ScheduleBoardViewModel(IFrameNavigationService mainFrameNavigationService, IDataService mainDataService) : base(mainFrameNavigationService, mainDataService)
        {
            Messenger.Default.Register<NotificationMessage>(this, async m =>
            {
                switch (m.Notification)
                {
                    case "ReloadBoard":
                        await ReloadBoard();
                        break;
                    case "RefreshTimeUpdated":
                        var refTimeSec = MainDataService.GetLocalSettings().RefreshTimeInMinutes * 60;
                        _dispatcherTimer.Interval = new TimeSpan(0, 0, (int)refTimeSec);
                        break;
                    case "ClockFormatChanged":
                        //todo change the clock format
                        break;
                    case "WorkingHoursChanged":
                        await ReloadBoard();
                        break;


                }
            });
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += dispatcherTimer_Tick;
            var refreshTimeInSeconds = MainDataService.GetLocalSettings().RefreshTimeInMinutes * 60;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, (int)refreshTimeInSeconds);
            _dispatcherTimer.Start();
        }
        private async void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            await RefreshBoardPeriodicly();
        }

        private bool hoursMutex = false;
        private bool stallsMutex = false;


        private async Task UpdateHoursCollection()
        {
            if (hoursMutex) return;
            hoursMutex = true;
            
            var list = new List<ITimeLineJobTask>();
            var startDateTime = StartDateTime;
            await Task.Run(() =>
            {
                while (startDateTime < EndDateTime)
                {
                    list.Add(new HourJobCard()
                    {
                        StartTime = startDateTime,
                        EndTime = startDateTime.AddMinutes(10 * (1000 / TimeLineUnitSize)),
                        HourDesignation = startDateTime.ToString("HH:mm")
                    });
                    startDateTime = startDateTime.AddMinutes(10 * (1000 / TimeLineUnitSize));
                }
            });
            HoursCollection = new ObservableCollection<ITimeLineJobTask>(list);
            hoursMutex = false;

        }

        private async Task ReloadBoard()
        {
            if (stallsMutex) return;
            stallsMutex = true;
            IsPrograssRingActive = true;
            //for test purpuses             
            await Task.Run(() => Thread.Sleep(3000));
            StartDateTime = DateTime.Today.Add(MainDataService.GetWorkingHoursSettings().StartHour);
            EndDateTime = DateTime.Today.Add(MainDataService.GetWorkingHoursSettings().EndHour);
            StallsCollection = new ObservableCollection<Stall>(await Task.Run(() => MainDataService.GetBranchStalls((int)MainFrameNavigationService.Parameter)));          
            await UpdateHoursCollection();
            IsPrograssRingActive = false;
            stallsMutex = false;
        }

        private async Task RefreshBoardPeriodicly()
        {
            if (stallsMutex) return;
            stallsMutex = true;
            await Task.Run(() =>
             {
                 var collection = MainDataService.GetBranchStalls((int)MainFrameNavigationService.Parameter);
                 collection.ForEach(s =>
                 {
                     foreach (var jt in s.JobTasksCollection)
                     {
                        //Irregularities
                        if (jt.PlannedStartTime < DateTime.Now)
                         {

                             if (jt.StatusId == (int)StatusEnum.Received)
                             {
                                 jt.JobTaskBackGround = MainDataService.GetLocalSettings().IrrLateJobVr;
                                 jt.IsJobTaskBliking= MainDataService.GetLocalSettings().IrrLateJobVrBlink;
                                 continue;
                             }
                             else if (jt.StatusId == (int)StatusEnum.Booked)
                             {
                                 jt.JobTaskBackGround = MainDataService.GetLocalSettings().IrrLateJobBooked;
                                 jt.IsJobTaskBliking = MainDataService.GetLocalSettings().IrrLateJobBookedBlink;
                                 continue;
                             }
                             else if (jt.StatusId == (int)StatusEnum.Allocated)
                             {
                                 jt.JobTaskBackGround = MainDataService.GetLocalSettings().IrrLateJobBooked;
                                 jt.IsJobTaskBliking = MainDataService.GetLocalSettings().IrrLateJobBookedBlink;
                                 continue;
                             }

                         }

                         if (jt.EndTime < DateTime.Now)
                         {

                             if (jt.StatusId == (int)StatusEnum.InProgress || jt.StatusId == (int)StatusEnum.WaitingForParts
                             || jt.StatusId == (int)StatusEnum.WaitingForQc || jt.StatusId == (int)StatusEnum.WaitingForWashing)
                             {
                                 jt.JobTaskBackGround = MainDataService.GetLocalSettings().PdtExceededInProgress;
                                 jt.IsJobTaskBliking = MainDataService.GetLocalSettings().PdtExceededInProgressBlink;
                                 continue;
                             }
                             else if (jt.StatusId == (int)StatusEnum.WaitingForInvoice)
                             {
                                 jt.JobTaskBackGround = MainDataService.GetLocalSettings().PdtExceededWaittingForInvoice;
                                 jt.IsJobTaskBliking = MainDataService.GetLocalSettings().PdtExceededWaittingForInvoiceBlink;


                                 continue;
                             }

                         }
                         jt.JobTaskBackGround = "#00000000";


                     }
                 });
                 StallsCollection = new ObservableCollection<Stall>(collection);
             });
            stallsMutex = false;

        }
        #endregion       
    }
}