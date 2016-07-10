﻿using System;
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
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace AppointementScheduleBoard.ViewModel
{
    public class ScheduleBoardViewModel: NavigableViewModelBase
    {
        #region Fields  
        private bool _isPrograssRingActive;
        private ObservableCollection<Stall> _sallsCollection;
        private double _timeLineUnitSize = 1000;
        private ObservableCollection<ITimeLineJobTask> _hoursCollection;       
        private DateTime _startDateTime;
        private DateTime _endDateTime;
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

                }
            });
            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }
        private async void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            await RefreshBoardPeriodicly();
        }

        private async Task UpdateHoursCollection()
        {                         
            var list=new List<ITimeLineJobTask>();
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
                    startDateTime = startDateTime.AddMinutes(10*(1000/TimeLineUnitSize));
                }                
            });
            HoursCollection=new ObservableCollection<ITimeLineJobTask>(list);


        }

        private async Task ReloadBoard()
        {
            IsPrograssRingActive = true;
            //for test purpuses             
            await Task.Run(()=>Thread.Sleep(3000));
            StallsCollection = new ObservableCollection<Stall>(await Task.Run(() => MainDataService.GetBranchStalls((int)MainFrameNavigationService.Parameter)));
            StartDateTime = DateTime.Today.Add(MainDataService.GetServerSettings().StartHour);
            EndDateTime = DateTime.Today.Add(MainDataService.GetServerSettings().EndHour);

            await UpdateHoursCollection();
            IsPrograssRingActive = false;
        }

        private async Task RefreshBoardPeriodicly()
        {
            StallsCollection = new ObservableCollection<Stall>(await Task.Run(() => MainDataService.GetBranchStalls((int)MainFrameNavigationService.Parameter)));
        }
        #endregion       
    }
}