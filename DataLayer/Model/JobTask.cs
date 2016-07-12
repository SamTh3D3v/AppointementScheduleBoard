using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using DataLayer.Annotations;
using DataLayer.Enums;

namespace DataLayer.Model
{
    public class JobTask:INotifyPropertyChanged, ITimeLineJobTask
    {
        #region Fields
        private string _jobType;
        private DateTime _receptionTime;
        private DateTime _pdt;
        private bool _isClientWaiting;
        private string _status;
        private Int32 _id;
        private DateTime? _actualStartTime;
        private DateTime? _plannedStartTime;
        private int _statusId;
        private String _jobTaskBackGround;     
        private bool _isJobTaskBliking ;            

        #endregion
        #region Properties

        public Int32 Id
        {
            get { return _id; }
            set
            {
                if (value.Equals(_id)) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public string JobType
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
        public string Status
        {
            get { return _status; }
            set
            {
                if (value == _status) return;
                _status = value;
                OnPropertyChanged();
            }
        }

        public int StatusId
        {
            get { return _statusId; }
            set
            {
                if (value == _statusId) return;
                _statusId = value;
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

        public String JobTaskBackGround
        {
            get
            {
                return _jobTaskBackGround;
            }

            set
            {
                if (_jobTaskBackGround == value)
                {
                    return;
                }

                _jobTaskBackGround = value;
                OnPropertyChanged();
            }
        }
        public bool IsJobTaskBliking
        {
            get
            {
                return _isJobTaskBliking;
            }

            set
            {
                if (_isJobTaskBliking == value)
                {
                    return;
                }

                _isJobTaskBliking = value;
                OnPropertyChanged();
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
