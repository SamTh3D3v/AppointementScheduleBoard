﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using AppointementScheduleBoard.Helpers;
using DataLayer;
using DataLayer.DataService;
using DataLayer.Enums;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace AppointementScheduleBoard.ViewModel
{
    public class ScheduleBoardViewModel : NavigableViewModelBase
    {
        #region Fields  
        private ObservableCollection<FilteredStall> _filteredStallsCollection;
        private bool _isPrograssRingActive;
        private ObservableCollection<Stall> _sallsCollection;
        private double _timeLineUnitSize = 1000;
        private ObservableCollection<ITimeLineJobTask> _hoursCollection;
        private DateTime _startDateTime;
        private DateTime _endDateTime;
        private DispatcherTimer _dispatcherTimer;
        #endregion
        #region Properties         
        public ObservableCollection<FilteredStall> FilteredStallsCollection
        {
            get
            {
                return _filteredStallsCollection;
            }

            set
            {
                if (_filteredStallsCollection == value)
                {
                    return;
                }

                _filteredStallsCollection = value;
                RaisePropertyChanged();
            }
        }
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
        private RelayCommand<object> _stallsFilterUpdatedCommand;
        public RelayCommand<object> StallsFilterUpdatedCommand
        {
            get
            {
                return _stallsFilterUpdatedCommand
                    ?? (_stallsFilterUpdatedCommand = new RelayCommand<object>(
                    (obj) =>
                    {
                        var collection = (ObservableCollection<FilteredStall>)obj;
                        FilteredStallsCollection = collection;
                        //todo


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
                        var refTimeSec = MainDataService.GetLocalSettings().RefreshTimeInSeconds;
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
            Messenger.Default.Register<Object>(this, "SetFilteredStallsCollection", (obj) =>
            {
                var collection = (ObservableCollection<FilteredStall>)obj;
                FilteredStallsCollection = collection;
            });
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += dispatcherTimer_Tick;
            var res = MainDataService.GetLocalSettings();
            var refreshTimeInSeconds = MainDataService.GetLocalSettings().RefreshTimeInSeconds;
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
                list.Add(new HourJobCard()
                {
                    StartTime = startDateTime,
                    EndTime = startDateTime.AddMinutes(10 * (1000 / TimeLineUnitSize)),
                    HourDesignation = startDateTime.ToString("HH:mm")
                });
                startDateTime = startDateTime.AddMinutes(10 * (1000 / TimeLineUnitSize));
            });
            HoursCollection = new ObservableCollection<ITimeLineJobTask>(list);
            hoursMutex = false;

        }

        private async Task ReloadBoard()
        {
            try
            {
                if (stallsMutex) return;
                stallsMutex = true;
                IsPrograssRingActive = true;



                //for a better user exeperiance         
                await Task.Run(() => Thread.Sleep(1000));
                StartDateTime = DateTime.Today.Add(MainDataService.GetWorkingHoursSettings().StartHour);
                EndDateTime = DateTime.Today.Add(MainDataService.GetWorkingHoursSettings().EndHour);
                await Task.Run(() =>
                {
                    var collection = MainDataService.GetBranchStalls((int) MainFrameNavigationService.Parameter);
                    StallsCollection = new ObservableCollection<Stall>(collection);
                    FilteredStallsCollection =
                        new ObservableCollection<FilteredStall>(collection.Select(s => new FilteredStall()
                        {
                            Stall = s,
                            IsSelected = true
                        }));
                });
               
                


                await UpdateHoursCollection();
                IsPrograssRingActive = false;
                stallsMutex = false;
            }
            catch (Exception ex)
            {

                var errorMessage = $"An exception occurred: {ex.Message}";
                var controller = await ((Application.Current.MainWindow as MetroWindow).ShowMessageAsync("Something went wrong, Details :", errorMessage));
                (Application.Current.MainWindow as MetroWindow).Close();
            }
        }

        private async Task RefreshBoardPeriodicly()
        {
            try
            {
                if (stallsMutex) return;
                stallsMutex = true;
                await Task.Run(() =>
                 {
                     var collection = MainDataService.GetBranchStalls((int)MainFrameNavigationService.Parameter);
                     var resCollection = new List<Stall>();
                     collection.ForEach(s =>
                     {
                         if (FilteredStallsCollection.First(st => st.Stall.Id == s.Id).IsSelected)
                         {
                             resCollection.Add(s);
                         }
                     });
                     resCollection.ForEach(s =>
                     {
                         foreach (var jt in s.JobTasksCollection)
                         {
                             //Irregularities
                             if (jt.PlannedStartTime < DateTime.Now)
                             {

                                 if (jt.StatusId == (int)StatusEnum.Received)
                                 {
                                     jt.JobTaskBackGround = MainDataService.GetLocalSettings().IrrLateJobVr;
                                     jt.IsJobTaskBliking = MainDataService.GetLocalSettings().IrrLateJobVrBlink;
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
                     StallsCollection = new ObservableCollection<Stall>(resCollection);
                 });
                stallsMutex = false;
            }
            catch (Exception ex)
            {

                var errorMessage = $"An exception occurred: {ex.Message}";
                var controller = await ((Application.Current.MainWindow as MetroWindow).ShowMessageAsync("Something went wrong, Details :", errorMessage));
                (Application.Current.MainWindow as MetroWindow).Close();
            }

        }
        #endregion       
    }
}