using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Annotations;

namespace DataLayer.Model
{
    public class Stall : INotifyPropertyChanged
    {
        #region Fields        
        private Guid _id;
        private string _stallName;
        private ObservableCollection<ITimeLineJobTask> _jobTasksCollection;
        #endregion
        #region Properties
        public string StallName
        {
            get
            {
                return _stallName;
            }

            set
            {
                if (_stallName == value)
                {
                    return;
                }

                _stallName = value;
                OnPropertyChanged();
            }
        }

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

        public ObservableCollection<ITimeLineJobTask> JobTasksCollection 
        {
            get { return _jobTasksCollection; }
            set
            {
                if (Equals(value, _jobTasksCollection)) return;
                _jobTasksCollection = value;
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
