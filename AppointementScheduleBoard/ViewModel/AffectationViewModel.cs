using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private bool _isLookingForTechnicians;        
        private TechniciansListView _techniciansListView;
        private ObservableCollection<Technicien> _techniciansList;
        private ObservableCollection<Stall> _myProperty;
        private Stall _selectedStall;
        private JobTask _selectedJobTask;
        private ObservableCollection<Branch> _branchsCollection;
        private ObservableCollection<Technicien> _techniciansFiltredCollection;
        private string _searchText = "";            
        private bool _isPrograssRingActive = false;
        #endregion
        #region Properties
        public bool IsPrograssRingActive
        {
            get
            {
                return _isPrograssRingActive;
            }

            set
            {
                if (_isPrograssRingActive == value)
                {
                    return;
                }

                _isPrograssRingActive = value;
                RaisePropertyChanged();
            }
        }
        public bool IsLookingForTechnicians
        {
            get
            {
                return _isLookingForTechnicians;
            }

            set
            {
                if (_isLookingForTechnicians == value)
                {
                    return;
                }

                _isLookingForTechnicians = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Branch> BranchsCollection
        {
            get
            {
                return _branchsCollection;
            }

            set
            {
                if (_branchsCollection == value)
                {
                    return;
                }

                _branchsCollection = value;
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
        public ObservableCollection<Technicien> TechniciansFiltredCollection
        {
            get
            {
                return _techniciansFiltredCollection;
            }

            set
            {
                if (_techniciansFiltredCollection == value)
                {
                    return;
                }

                _techniciansFiltredCollection = value;
                RaisePropertyChanged();
            }
        }
        public string SearchText
        {
            get
            {
                return _searchText;
            }

            set
            {
                if (_searchText == value)
                {
                    return;
                }

                _searchText = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _searchTechniciansCommand;
        public RelayCommand SearchTechniciansCommand
        {
            get
            {
                return _searchTechniciansCommand
                    ?? (_searchTechniciansCommand = new RelayCommand(async () =>
                    {
                        await Task.Run(() =>
                        {
                            TechniciansFiltredCollection =
                                new ObservableCollection<Technicien>(
                                    _techniciansList.Where(t => t.Name.ToLower().Contains(SearchText.ToLower())));
                        });

                    }));
            }
        }

        private RelayCommand _affectationViewLoadedCommand;
        public RelayCommand AffectationViewLoadedCommand
        {
            get
            {
                return _affectationViewLoadedCommand
                    ?? (_affectationViewLoadedCommand = new RelayCommand(async () =>
                    {
                        IsPrograssRingActive = true;
                        await LaodStalls();
                        BranchsCollection = new ObservableCollection<Branch>(await Task.Run(() => MainDataService.GetAllBranchs()));
                        IsPrograssRingActive = false;
                    }));
            }
        }
        private RelayCommand<int> _removeSelectedTechnicianCommand;
        public RelayCommand<int> RemoveSelectedTechnicianCommand
        {
            get
            {
                return _removeSelectedTechnicianCommand
                    ?? (_removeSelectedTechnicianCommand = new RelayCommand<int>(async (id) =>
                    {
                        MainDataService.ReleaseMechanicFromStall(id);
                        //to avoid refreshing
                        //SelectedStall.Techniciens.Remove(SelectedStall.Techniciens.First(t => t.Id == id));
                        //Or Whatever 
                        await LaodStalls();
                    }));
            }
        }
        private RelayCommand<int> _removeSelectedStallCommand;
        public RelayCommand<int> RemoveSelectedStallCommand
        {
            get
            {
                return _removeSelectedStallCommand
                    ?? (_removeSelectedStallCommand = new RelayCommand<int>(
                    (id) =>
                    {
                        MainDataService.RemoveStall(id);
                        //to avoid refreshing
                        StallsList.Remove(StallsList.First(s => s.Id == id));
                    }));
            }
        }
        private async Task LaodStalls()
        {
            try
            {
                var res =
                    await Task.Run(() => MainDataService.GetBranchStalls((int) MainFrameNavigationService.Parameter));
                StallsList = new ObservableCollection<Stall>(res);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                
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
                        //Create a new Stall
                        SelectedStall = new Stall()
                        {
                            Id = -1, //a new Stall
                            BranchId = (int)MainFrameNavigationService.Parameter,
                            Techniciens = new ObservableCollection<Technicien>(),
                            JobTasksCollection = new ObservableCollection<ITimeLineJobTask>()
                        };

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
                        SelectedStall = null;
                    }));
            }
        }
        private RelayCommand _saveNewStallCommand;
        public RelayCommand SaveNewStallCommand
        {
            get
            {
                return _saveNewStallCommand
                    ?? (_saveNewStallCommand = new RelayCommand(async () =>
                    {
                        if (SelectedStall.Id == -1)
                        {
                            MainDataService.AddStall(SelectedStall);
                        }
                        else
                        {
                            MainDataService.UpdateStall(SelectedStall);
                        }
                        await LaodStalls();
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
                        _techniciansListView = new TechniciansListView();
                        await _techniciansListView.ShowDialogAsync();

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
                        MainFrameNavigationService.NavigateTo(App.ScheduleBoardViewKey, MainFrameNavigationService.Parameter);
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
        private RelayCommand _cancelTechniciansListViewAffectationComandCommand;
        public RelayCommand CancelTechniciansListViewAffectationComand
        {
            get
            {
                return _cancelTechniciansListViewAffectationComandCommand
                    ?? (_cancelTechniciansListViewAffectationComandCommand = new RelayCommand(
                    () =>
                    {
                        _techniciansListView.Close();
                        SearchText = "";
                        _techniciansList = new ObservableCollection<Technicien>();
                        TechniciansFiltredCollection = new ObservableCollection<Technicien>();
                    }));
            }
        }
        private RelayCommand<object> _saveTechnicianListViewAffectationCommand;
        public RelayCommand<object> SaveTechnicianListViewAffectationCommand
        {
            get
            {
                return _saveTechnicianListViewAffectationCommand
                    ?? (_saveTechnicianListViewAffectationCommand = new RelayCommand<object>(async (obj) =>
                    {
                        var selectedTechniciansList = obj as IList;
                        if (selectedTechniciansList != null)
                            foreach (var tech in selectedTechniciansList)
                            {
                                MainDataService.AssignMechanicToStall(SelectedStall.Id, (tech as Technicien).Id);
                            }
                        _techniciansListView.Close();
                        SearchText = "";
                        _techniciansList=new ObservableCollection<Technicien>();
                        TechniciansFiltredCollection=new ObservableCollection<Technicien>();
                        await LaodStalls();
                    }));
            }
        }

        private RelayCommand _refreshTechniciansListCommand;
        public RelayCommand RefreshTechniciansListCommand
        {
            get
            {
                return _refreshTechniciansListCommand
                    ?? (_refreshTechniciansListCommand = new RelayCommand(async () =>
                    {
                        _techniciansList=new ObservableCollection<Technicien>();
                        TechniciansFiltredCollection=new ObservableCollection<Technicien>();
                        await LoadTechnicians();
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
            if (IsLookingForTechnicians)
                return;
           
            IsLookingForTechnicians = true;
            await Task.Run(() =>
            {
                _techniciansList =
                    new ObservableCollection<Technicien>(
                        MainDataService.GetNotAssignedTechnicians((int)MainFrameNavigationService.Parameter));
                TechniciansFiltredCollection = new ObservableCollection<Technicien>(_techniciansList);
            });
            IsLookingForTechnicians = false;

        }
        #endregion

    }
}
