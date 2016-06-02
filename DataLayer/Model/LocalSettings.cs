using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Annotations;
using DataLayer.Enums;

namespace DataLayer.Model
{
    //this settings are from the XML file; 
    public class LocalSettings:INotifyPropertyChanged
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

        #endregion
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
