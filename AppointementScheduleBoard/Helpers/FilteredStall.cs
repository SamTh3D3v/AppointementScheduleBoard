using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Annotations;
using DataLayer.Model;

namespace AppointementScheduleBoard.Helpers
{
    public class FilteredStall:INotifyPropertyChanged
    {
        private bool _isSelected;
        private Stall _stall;

        public bool IsSelected
        {
            get { return _isSelected; }

            set
            {
                if (_isSelected == value)
                {
                    return;
                }

                _isSelected = value;
                OnPropertyChanged();
            }
        }                  
        public Stall Stall
        {
            get
            {
                return _stall;
            }

            set
            {
                if (_stall == value)
                {
                    return;
                }

                _stall = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
