using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointementScheduleBoard.Helpers;
using AppointementScheduleBoard.View;
using DataLayer.DataService;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;

namespace AppointementScheduleBoard.ViewModel
{
    public class AffectationViewModel : NavigableViewModelBase
    {

        #region Fields

        
        private ObservableCollection<Stall> _myProperty;
        private Stall _selectedStall;
        private JobTask _selectedJobTask;
        private ObservableCollection<Branch> _branchIdsCollection;
        private ObservableCollection<Technicien> _techniciansCollection;

        #endregion
        #region Properties
        public ObservableCollection<Branch> BranchIdsCollection
        {
            get
            {
                return _branchIdsCollection;
            }

            set
            {
                if (_branchIdsCollection == value)
                {
                    return;
                }

                _branchIdsCollection = value;
                RaisePropertyChanged();
            }
        }
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
        public JobTask SelectedJobTask
        {
            get
            {
                return _selectedJobTask;
            }

            set
            {
                if (_selectedJobTask == value)
                {
                    return;
                }

                _selectedJobTask = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Technicien> TechniciansCollection
        {
            get
            {
                return _techniciansCollection;
            }

            set
            {
                if (_techniciansCollection == value)
                {
                    return;
                }

                _techniciansCollection = value;
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
                        StallsList = new ObservableCollection<Stall>(await Task.Run(() => MainDataService.GetBranchStalls(1)));
                        BranchIdsCollection = new ObservableCollection<Branch>(await Task.Run(() => MainDataService.GetAllBranchs()));
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
        private RelayCommand _affectTechnicianToStallCommand;
        public RelayCommand AffectTechnicianToStallCommand
        {
            get
            {
                return _affectTechnicianToStallCommand
                    ?? (_affectTechnicianToStallCommand = new RelayCommand(async () =>
                    {
                        var techniciansView = new TechniciansListView();
                        await techniciansView.ShowDialogAsync();

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
        private RelayCommand _technicansViewLoadedCommand;
        public RelayCommand TechnicansViewLoadedCommand
        {
            get
            {
                return _technicansViewLoadedCommand
                    ?? (_technicansViewLoadedCommand = new RelayCommand(async () =>
                    {
                        await LoadTechnicians();
                    }));
            }
        }
        private RelayCommand _techniciansViewUnloadedCommand;
        public RelayCommand TechniciansViewUnloadedCommand
        {
            get
            {
                return _techniciansViewUnloadedCommand
                    ?? (_techniciansViewUnloadedCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }
        #endregion
        #region Ctors and methods
        public AffectationViewModel(IFrameNavigationService mainFrameNavigationService, IDataService mainDataService) : base(mainFrameNavigationService, mainDataService)
        {
        }

        private async Task LoadTechnicians()
        {
            await Task.Run(() =>
            {
                TechniciansCollection =
                    new ObservableCollection<Technicien>(
                        MainDataService.GetAllTechnicians((int) MainFrameNavigationService.Parameter));
            });

        }
        #endregion

    }
}
