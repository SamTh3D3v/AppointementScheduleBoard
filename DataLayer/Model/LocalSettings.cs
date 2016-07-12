using System;
using System.Collections.Generic;
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
    //this settings are from the XML file; 
    public class LocalSettings : INotifyPropertyChanged
    {
        #region Fields
        private double _refreshTimeInMinutes;
        private bool _isClockFormat24;
        private double _unitSize;
        private ViewedStallsEnum _viewedStalls;
        private bool _isShipStatusVisible;
        private bool _isShipClientWaitingVisible;
        private bool _isShipPdtVisible;
        private bool _isShipReceptionTimeVisible;
        private bool _isShipJobtypeVisible;
        private bool _isTimeHeaderVisible;
        private bool _isStallNamesVisible;
        private bool _isTechnicientsNamesVisible;
        private bool _isPlanActualHeaderVisible;
        private bool _isPlanActualMerged;

        private String _irrLateJobVr;
        private String _irrLateJobBooked;
        private String _irrPlannedTimeExeeded;
        private String _pdtExceededInProgress;
        private String _pdtExceededWaittingForInvoice;

        private bool _irrLateJobVrBlink;
        private bool _irrLateJobBookedBlink;
        private bool _irrPlannedTimeExeededBlink;
        private bool _pdtExceededInProgressBlink;
        private bool _pdtExceededWaittingForInvoiceBlink;


        #endregion
        #region Properties
        public Double RefreshTimeInMinutes
        {
            get { return _refreshTimeInMinutes; }
            set
            {
                if (value.Equals(_refreshTimeInMinutes)) return;
                _refreshTimeInMinutes = value;
                OnPropertyChanged();
            }
        }
        public bool IsClockFormat24
        {
            get { return _isClockFormat24; }
            set
            {
                if (value == _isClockFormat24) return;
                _isClockFormat24 = value;
                OnPropertyChanged();
            }
        }
        public Double UnitSize
        {
            get { return _unitSize; }
            set
            {
                if (value.Equals(_unitSize)) return;
                _unitSize = value;
                OnPropertyChanged();
            }
        }
        public ViewedStallsEnum ViewedStalls
        {
            get { return _viewedStalls; }
            set
            {
                if (value == _viewedStalls) return;
                _viewedStalls = value;
                OnPropertyChanged();
            }
        }
        public bool IsShipJobtypeVisible
        {
            get { return _isShipJobtypeVisible; }
            set
            {
                if (value == _isShipJobtypeVisible) return;
                _isShipJobtypeVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsShipReceptionTimeVisible
        {
            get { return _isShipReceptionTimeVisible; }
            set
            {
                if (value == _isShipReceptionTimeVisible) return;
                _isShipReceptionTimeVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsShipPdtVisible
        {
            get { return _isShipPdtVisible; }
            set
            {
                if (value == _isShipPdtVisible) return;
                _isShipPdtVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsShipClientWaitingVisible
        {
            get { return _isShipClientWaitingVisible; }
            set
            {
                if (value == _isShipClientWaitingVisible) return;
                _isShipClientWaitingVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsShipStatusVisible
        {
            get { return _isShipStatusVisible; }
            set
            {
                if (value == _isShipStatusVisible) return;
                _isShipStatusVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsTimeHeaderVisible
        {
            get { return _isTimeHeaderVisible; }
            set
            {
                if (value == _isTimeHeaderVisible) return;
                _isTimeHeaderVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsStallNamesVisible
        {
            get { return _isStallNamesVisible; }
            set
            {
                if (value == _isStallNamesVisible) return;
                _isStallNamesVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsTechnicientsNamesVisible
        {
            get { return _isTechnicientsNamesVisible; }
            set
            {
                if (value == _isTechnicientsNamesVisible) return;
                _isTechnicientsNamesVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsPlanActualHeaderVisible
        {
            get { return _isPlanActualHeaderVisible; }
            set
            {
                if (value == _isPlanActualHeaderVisible) return;
                _isPlanActualHeaderVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsPlanActualMerged
        {
            get { return _isPlanActualMerged; }
            set
            {
                if (value == _isPlanActualMerged) return;
                _isPlanActualMerged = value;
                OnPropertyChanged();
            }
        }

        //Irregularities Colors             
        public String IrrLateJobVr
        {
            get
            {
                return _irrLateJobVr;
            }

            set
            {
                if (_irrLateJobVr == value)
                {
                    return;
                }

                _irrLateJobVr = value;
                OnPropertyChanged();
            }
        }
        public String IrrLateJobBooked
        {
            get
            {
                return _irrLateJobBooked;
            }

            set
            {
                if (_irrLateJobBooked == value)
                {
                    return;
                }

                _irrLateJobBooked = value;
                OnPropertyChanged();
            }
        }
        public String IrrPlannedTimeExeeded
        {
            get
            {
                return _irrPlannedTimeExeeded;
            }

            set
            {
                if (_irrPlannedTimeExeeded == value)
                {
                    return;
                }

                _irrPlannedTimeExeeded = value;
                OnPropertyChanged();
            }
        }
        public String PdtExceededInProgress
        {
            get
            {
                return _pdtExceededInProgress;
            }

            set
            {
                if (_pdtExceededInProgress == value)
                {
                    return;
                }

                _pdtExceededInProgress = value;
                OnPropertyChanged();
            }
        }
        public String PdtExceededWaittingForInvoice
        {
            get
            {
                return _pdtExceededWaittingForInvoice;
            }

            set
            {
                if (_pdtExceededWaittingForInvoice == value)
                {
                    return;
                }

                _pdtExceededWaittingForInvoice = value;
                OnPropertyChanged();
            }
        }

        public bool IrrLateJobVrBlink
        {
            get
            {
                return _irrLateJobVrBlink;
            }

            set
            {
                if (_irrLateJobVrBlink == value)
                {
                    return;
                }

                _irrLateJobVrBlink = value;
                OnPropertyChanged();
            }
        }
        public bool IrrLateJobBookedBlink
        {
            get
            {
                return _irrLateJobBookedBlink;
            }

            set
            {
                if (_irrLateJobBookedBlink == value)
                {
                    return;
                }

                _irrLateJobBookedBlink = value;
                OnPropertyChanged();
            }
        }
        public bool IrrPlannedTimeExeededBlink
        {
            get
            {
                return _irrPlannedTimeExeededBlink;
            }

            set
            {
                if (_irrPlannedTimeExeededBlink == value)
                {
                    return;
                }

                _irrPlannedTimeExeededBlink = value;
                OnPropertyChanged();
            }
        }
        public bool PdtExceededInProgressBlink
        {
            get
            {
                return _pdtExceededInProgressBlink;
            }

            set
            {
                if (_pdtExceededInProgressBlink == value)
                {
                    return;
                }

                _pdtExceededInProgressBlink = value;
                OnPropertyChanged();
            }
        }
        public bool PdtExceededWaittingForInvoiceBlink
        {
            get
            {
                return _pdtExceededWaittingForInvoiceBlink;
            }

            set
            {
                if (_pdtExceededWaittingForInvoiceBlink == value)
                {
                    return;
                }

                _pdtExceededWaittingForInvoiceBlink = value;
                OnPropertyChanged();
            }
        }

        #endregion
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
