using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Annotations;
using DataLayer.Enums;

namespace DataLayer.Model
{
    public class JobTask:INotifyPropertyChanged, ITimeLineJobTask
    {
        #region Fields
        private JobTypesEnum _jobType;
        private DateTime _receptionTime;
        private DateTime _pdt;
        private bool _isClientWaiting;
        private StatusEnum _status;
        private Guid _id;
        private DateTime? _actualStartTime;
        private DateTime? _plannedStartTime;
        private ObservableCollection<Technicien> _techniciens;

        #endregion
        #region Properties

        public Guid Id
        {
            get { return _id; }
            set
            {
                if (value.Equals(_id)) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public JobTypesEnum JobType
        {
            get { return _jobType; }
            set
            {
                if (value == _jobType) return;
                _jobType = value;
                OnPropertyChanged();
            }
        }

        public DateTime ReceptionTime
        {
            get { return _receptionTime; }
            set
            {
                if (value.Equals(_receptionTime)) return;
                _receptionTime = value;
                OnPropertyChanged();
            }
        }

        public DateTime PDT
        {
            get { return _pdt; }
            set
            {
                if (value.Equals(_pdt)) return;
                _pdt = value;
                OnPropertyChanged();
            }
        }
        public bool IsClientWaiting
        {
            get { return _isClientWaiting; }
            set
            {
                if (value == _isClientWaiting) return;
                _isClientWaiting = value;
                OnPropertyChanged();
            }
        }
        public StatusEnum Status
        {
            get { return _status; }
            set
            {
                if (value == _status) return;
                _status = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Technicien> Techniciens
        {
            get { return _techniciens; }
            set
            {
                if (Equals(value, _techniciens)) return;
                _techniciens = value;
                OnPropertyChanged();
            }
        }        
        public DateTime? PlannedStartTime
        {
            get
            {
                return _plannedStartTime;
            }

            set
            {
                if (_plannedStartTime == value)
                {
                    return;
                }

                _plannedStartTime = value;
                OnPropertyChanged();
            }
        }                     
        public DateTime? ActualStartTime
        {
            get
            {
                return _actualStartTime;
            }

            set
            {
                if (_actualStartTime == value)
                {
                    return;
                }

                _actualStartTime = value;
                OnPropertyChanged();
            }
        }
        public DateTime? StartTime
        {
            get { return ActualStartTime ?? PlannedStartTime; }
            set
            {
                if (ActualStartTime==null)
                {
                    PlannedStartTime = value;
                }
                else
                {
                    ActualStartTime = value;
                }
            }
        }
        public DateTime? EndTime { get; set; }
        public bool TimelineViewExpanded { get; set; }
        #endregion  
        #region PropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion      
    }
}
