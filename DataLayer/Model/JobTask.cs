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
        string _bookingNumber;
        private DateTime _bookingDate;
        private string _status;
        private int _statusId;
        string _sevirity;
        private string _jobType;
        private int _taskDuration;

        private int? _incidentId;
        private DateTime? _clockIn;
        private DateTime? _clockOut;
        private int _mechanicsCount;

        private bool _isClientWaiting;
        private DateTime? _pdt;
        private DateTime? _resolvedDate;

        private DateTime? _startTime;
        private DateTime? _endTime;

        private Int32 _id;
        private DateTime _receptionTime;
        private DateTime? _plannedStartTime;
        private DateTime? _actualStartTime;

        private Boolean _timelineViewExpanded;
        private String _jobTaskBackGround;     
        private bool _isJobTaskBliking ;
        #endregion
        #region Properties
        public string BookingNumber
        {
            get
            {
                return _bookingNumber;
            }

            set
            {
                if (value.Equals(_bookingNumber)) return;
                _bookingNumber = value;
                OnPropertyChanged();
            }
        }
        public DateTime BookingDate
        {
            get
            {
                return _bookingDate;
            }

            set
            {
                if (value.Equals(_bookingDate)) return;
                _bookingDate = value;
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
        public string Sevirity
        {
            get
            {
                return _sevirity;
            }

            set
            {
                if (value == _sevirity) return;
                _sevirity = value;
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
        public int TaskDuration
        {
            get
            {
                return _taskDuration;
            }

            set
            {
                if (value == _taskDuration) return;
                _taskDuration = value;
                OnPropertyChanged();
            }
        }

        public int? IncidentId
        {
            get
            {
                return _incidentId;
            }

            set
            {
                if (value == _incidentId) return;
                _incidentId = value;
                OnPropertyChanged();
            }
        }
        public DateTime? ClockIn
        {
            get
            {
                return _clockIn;
            }

            set
            {
                if (value == _clockIn) return;
                _clockIn = value;
                OnPropertyChanged();
            }
        }
        public DateTime? ClockOut
        {
            get
            {
                return _clockOut;
            }

            set
            {
                if (value == _clockOut) return;
                _clockOut = value;
                OnPropertyChanged();
            }
        }
        public int MechanicsCount
        {
            get
            {
                return _mechanicsCount;
            }

            set
            {
                if (value == _mechanicsCount) return;
                _mechanicsCount = value;
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
        public DateTime? PDT
        {
            get
            {
                return _pdt;
            }
            set
            {
                if (value == _pdt) return;
                _pdt = value;
                OnPropertyChanged();
            }
        }
        public DateTime? ResolvedDate
        {
            get
            {
                return _resolvedDate;
            }

            set
            {
                if (value == _resolvedDate) return;
                _resolvedDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime? StartTime
        {
            get
            {
                if (_clockIn == null)
                    return _bookingDate;
                else
                    return _clockIn;
            }

            set
            {
                if (value == _startTime) return;
                _startTime = value;
                OnPropertyChanged();
            }
        }
        public DateTime? EndTime
        {
            get
            {
                if (_mechanicsCount == 0)
                    return _bookingDate.AddMinutes(_taskDuration / 1.7);

                if (_clockIn == null)
                    return _bookingDate.AddMinutes((_taskDuration / 1.7) / _mechanicsCount);
                else if (_resolvedDate == null)
                    return ((DateTime)_clockIn).AddMinutes((_taskDuration / 1.7) / _mechanicsCount);
                else
                    return _resolvedDate;
            }

            set
            {
                if (value == _endTime) return;
                _endTime = value;
                OnPropertyChanged();
            }
        }

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

        public bool TimelineViewExpanded
        {
            get
            {
                return _timelineViewExpanded;
            }

            set
            {
                if (value == _timelineViewExpanded) return;
                _timelineViewExpanded = value;
                OnPropertyChanged();
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
