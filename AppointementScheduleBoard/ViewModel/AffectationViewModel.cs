using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointementScheduleBoard.Helpers;
using DataLayer.DataService;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;

namespace AppointementScheduleBoard.ViewModel
{
    public class AffectationViewModel:NavigableViewModelBase
    {

        #region Fields
        private ObservableCollection<Stall> _myProperty;
        private Stall _selectedStall;
        #endregion
        #region Properties
        public ObservableCollection<Stall> StallsList
        {
            get
            {
                return _myProperty;
            }

            set
            {
                if (_myProperty == value)
                {
                    return;
                }

                _myProperty = value;
                RaisePropertyChanged();
            }
        }
        public Stall SelectedStall
        {
            get
            {
                return _selectedStall;
            }

            set
            {
                if (_selectedStall == value)
                {
                    return;
                }

                _selectedStall = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _affectationViewLoadedCommand;
        public RelayCommand AffectationViewLoadedCommand
        {
            get
            {
                return _affectationViewLoadedCommand
                    ?? (_affectationViewLoadedCommand = new RelayCommand(async () =>
                    {
                        StallsList=new ObservableCollection<Stall>(await Task.Run(()=>MainDataService.GetStallsCollection()));
                    }));
            }
        }
        private RelayCommand _addNewStallCommand;
        public RelayCommand AddNewStallCommand
        {
            get
            {
                return _addNewStallCommand
                    ?? (_addNewStallCommand = new RelayCommand(
                    () =>   
                    {

                    }));
            }
        }
        private RelayCommand _cancelNewStallCommand;
        public RelayCommand CancelNewStallCommand
        {
            get
            {
                return _cancelNewStallCommand
                    ?? (_cancelNewStallCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }
        private RelayCommand _saveNewStallCommand;
        public RelayCommand SaveNewStallCommand
        {
            get
            {
                return _saveNewStallCommand
                    ?? (_saveNewStallCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }
        private RelayCommand _goBackCommand;
        public RelayCommand GoBackCommand
        {
            get
            {
                return _goBackCommand
                    ?? (_goBackCommand = new RelayCommand(
                    () =>
                    {
                        MainFrameNavigationService.NavigateTo(App.ScheduleBoardViewKey);
                    }));
            }
        }
        #endregion
        #region Ctors and methods
        public AffectationViewModel(IFrameNavigationService mainFrameNavigationService, IDataService mainDataService) : base(mainFrameNavigationService, mainDataService)
        {
        }
        #endregion

    }
}
