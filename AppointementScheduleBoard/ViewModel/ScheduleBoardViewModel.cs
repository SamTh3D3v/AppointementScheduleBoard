using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointementScheduleBoard.Helpers;
using DataLayer;
using DataLayer.DataService;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;

namespace AppointementScheduleBoard.ViewModel
{
    public class ScheduleBoardViewModel: NavigableViewModelBase
    {
        #region Fields        
        private ObservableCollection<Stall> _sallsCollection;
        private double _timeLineUnitSize = 1000;
        private ObservableCollection<ITimeLineJobTask> _hoursCollection;       
        private DateTime _startDateTime;                
        #endregion
        #region Properties 
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
                        StallsCollection=new ObservableCollection<Stall>(await Task.Run(()=>MainDataService.GetBranchStalls((int) MainFrameNavigationService.Parameter)));
                        StartDateTime =  DateTime.Today.Add(MainDataService.GetServerSettings().StartHour);
                        await UpdateHoursCollection();
                    }));
            }
        }
        #endregion
        #region Ctors and methods
        public ScheduleBoardViewModel(IFrameNavigationService mainFrameNavigationService, IDataService mainDataService) : base(mainFrameNavigationService, mainDataService)
        {
        }

        private async Task UpdateHoursCollection()
        {            
            var list=new List<ITimeLineJobTask>();
            var startDateTime = StartDateTime;
            await Task.Run(() =>
            {                
                var endDateTime = DateTime.Now.Date.AddHours(6).AddHours(12);
                while (startDateTime < endDateTime)
                {
                    list.Add(new HourJobCard()
                    {
                        StartTime = startDateTime,
                        EndTime = startDateTime.AddMinutes(10),
                        HourDesignation = startDateTime.ToString("HH:mm")
                    });
                    startDateTime = startDateTime.AddMinutes(10);
                }                
            });
            HoursCollection=new ObservableCollection<ITimeLineJobTask>(list);


        }
        #endregion       
    }
}
