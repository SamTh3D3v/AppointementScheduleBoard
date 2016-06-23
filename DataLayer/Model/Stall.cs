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
        private int _id;
        private int _branchId;
        private string _stallName;
        private string _stallDescription;
        private string _isActive;
        private ObservableCollection<ITimeLineJobTask> _jobTasksCollection;
        private ObservableCollection<Technicien> _techniciens;
        #endregion
        #region Properties

        public int Id
        {
            get { return _id; }
            set
            {
                if (value.Equals(_id)) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public int BranchId
        {
            get
            {
                return _branchId;
            }

            set
            {
                if (_branchId == value)
                {
                    return;
                }

                _branchId = value;
                OnPropertyChanged();
            }
        }

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

        public string StallDescription
        {
            get
            {
                return _stallDescription;
            }

            set
            {
                if (_stallDescription == value)
                {
                    return;
                }

                _stallDescription = value;
                OnPropertyChanged();
            }
        }

        public string IsActive
        {
            get
            {
                return _isActive;
            }

            set
            {
                if (_isActive == value)
                {
                    return;
                }

                _isActive = value;
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
