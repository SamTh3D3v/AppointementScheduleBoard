using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using AppointementScheduleBoard.Helpers;
using DataLayer.DataService;
using DataLayer.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;

namespace AppointementScheduleBoard.ViewModel
{
   
    public class MainViewModel : NavigableViewModelBase
    {
        #region Fields
        private Branch _selectedBranch;
        private ObservableCollection<Branch> _branchCollection;
        private bool _isSettingsFlayoutOpen;
        private bool _showTitleBarProperty;
        private bool _ignoreTaskbarOnMaximizeProperty;
        private LocalSettings _localSettings;
        private ServerSettings _serverSettings;       
        private WorkingHoursSettings _workingHoursSettings ;           
        private ObservableCollection<Stall> _stallsCollection;                
        #endregion
        #region Properties  
        public Branch SelectedBranch
        {
            get
            {
                return _selectedBranch;
            }

            set
            {
                if (_selectedBranch == value)
                {
                    return;
                }

                _selectedBranch = value;
                RaisePropertyChanged();
            }
        }
        public WorkingHoursSettings WorkingHoursSettings
        {
            get
            {
                return _workingHoursSettings;
            }

            set
            {
                if (_workingHoursSettings == value)
                {
                    return;
                }

                _workingHoursSettings = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Branch> BranchCollection
        {
            get
            {
                return _branchCollection;
            }

            set
            {
                if (_branchCollection == value)
                {
                    return;
                }

                _branchCollection = value;
                RaisePropertyChanged();
            }
        }            
        public LocalSettings LocalSettings
        {
            get
            {
                return _localSettings;
            }

            set
            {
                if (_localSettings == value)
                {
                    return;
                }

                _localSettings = value;
                RaisePropertyChanged();
            }
        }     
        public ServerSettings ServerSettings
        {
            get
            {
                return _serverSettings;
            }

            set
            {
                if (_serverSettings == value)
                {
                    return;
                }

                _serverSettings = value;
                RaisePropertyChanged();
            }
        }
        public bool IsSettingsFlayoutOpen
        {
            get
            {
                return _isSettingsFlayoutOpen;
            }

            set
            {
                if (_isSettingsFlayoutOpen == value)
                {
                    return;
                }

                _isSettingsFlayoutOpen = value;
                RaisePropertyChanged();
            }
        }       
        public bool ShowTitleBarProperty
        {
            get
            {
                return  _showTitleBarProperty;
            }

            set
            {
                if ( _showTitleBarProperty == value)
                {
                    return;
                }

                 _showTitleBarProperty = value;
                RaisePropertyChanged();
            }
        }
        public bool IgnoreTaskbarOnMaximizeProperty
        {
            get
            {
                return _ignoreTaskbarOnMaximizeProperty;
            }

            set
            {
                if (_ignoreTaskbarOnMaximizeProperty == value)
                {
                    return;
                }

                _ignoreTaskbarOnMaximizeProperty = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Stall> StallsCollection
        {
            get
            {
                return _stallsCollection;
            }

            set
            {
                if (_stallsCollection == value)
                {
                    return;
                }

                _stallsCollection = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        private RelayCommand _mainWindowLoadedCommand;
        public RelayCommand MainWindowLoadedCommand
        {
            get
            {
                return _mainWindowLoadedCommand
                    ?? (_mainWindowLoadedCommand = new RelayCommand(async () =>
                    {                                                
                        ShowTitleBarProperty = true;
                        IgnoreTaskbarOnMaximizeProperty=false;
                        await LoadSettings();
                        MainFrameNavigationService.NavigateTo(App.ScheduleBoardViewKey, SelectedBranch.Id);
                    }));
            }
        }
        private RelayCommand _selectedBranchChangedCommand;
        public RelayCommand SelectedBranchChangedCommand
        {
            get
            {
                return _selectedBranchChangedCommand
                    ?? (_selectedBranchChangedCommand = new RelayCommand(
                    () =>
                    {                        
                        MainFrameNavigationService.NavigateTo(App.ScheduleBoardViewKey, SelectedBranch.Id);
                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ReloadBoard"));
                    }));
            }
        }

        private RelayCommand _mainWindowUnloadedCommand;
        public RelayCommand MainWindowUnloadedCommand
        {
            get
            {
                return _mainWindowUnloadedCommand
                    ?? (_mainWindowUnloadedCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }
        private RelayCommand _openTheSettingsFlayoutCommand;
        public RelayCommand OpenTheSettingsFlayoutCommand
        {
            get
            {
                return _openTheSettingsFlayoutCommand
                    ?? (_openTheSettingsFlayoutCommand = new RelayCommand(
                    () =>
                    {
                        IsSettingsFlayoutOpen = !IsSettingsFlayoutOpen;
                    }));
            }   
        }
        private RelayCommand<object> _moveToFullScreenCommand;
        public RelayCommand<object> MoveToFullScreenCommand
        {
            get
            {
                return _moveToFullScreenCommand
                    ?? (_moveToFullScreenCommand = new RelayCommand<object>(
                    (win) =>
                    {
                        IgnoreTaskbarOnMaximizeProperty = !IgnoreTaskbarOnMaximizeProperty;
                        ShowTitleBarProperty = !ShowTitleBarProperty;
                        //(win as MetroWindow).InvalidateVisual();
                    }));
            }
        }
        private RelayCommand _openAssignViewCommand;
        public RelayCommand OpenAssignViewCommand
        {
            get
            {
                return _openAssignViewCommand
                    ?? (_openAssignViewCommand = new RelayCommand(
                    () =>
                    {
                        IsSettingsFlayoutOpen = false;
                        MainFrameNavigationService.NavigateTo(App.AffectationViewKey, SelectedBranch.Id);
                    }));
            }
        }
        private RelayCommand _refreshTimeChangedCommand;    
        public RelayCommand RefreshTimeChangedCommand
        {
            get
            {
                return _refreshTimeChangedCommand
                    ?? (_refreshTimeChangedCommand = new RelayCommand(
                    () =>
                    {
                        MainDataService.UpdateLocalSettings(LocalSettings);
                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("RefreshTimeUpdated"));
                    }));
            }
        }
        private RelayCommand _clockFormateChangedChangedCommand;
        public RelayCommand ClockFormateChangedChangedCommand
        {
            get
            {
                return _clockFormateChangedChangedCommand
                    ?? (_clockFormateChangedChangedCommand = new RelayCommand(
                    () =>
                    {
                        MainDataService.UpdateLocalSettings(LocalSettings);
                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ClockFormatChanged"));
                    }));
            }
        }
        private RelayCommand _irregSettingsChangedCommand;
        public RelayCommand IrregSettingsChangedCommand
        {
            get
            {
                return _irregSettingsChangedCommand
                    ?? (_irregSettingsChangedCommand = new RelayCommand(
                    () =>
                    {
                        MainDataService.UpdateLocalSettings(LocalSettings); 
                    }));
            }
        }
        private RelayCommand _workingHoursSettingsChangedCommand;
        public RelayCommand WorkingHoursSettingsChangedCommand
        {
            get
            {
                return _workingHoursSettingsChangedCommand
                    ?? (_workingHoursSettingsChangedCommand = new RelayCommand(
                    () =>
                    {
                        MainDataService.UpdateWorkingHoursSettings(WorkingHoursSettings);
                        Messenger.Default.Send<NotificationMessage>(new NotificationMessage("WorkingHoursChanged"));
                    }));
            }
        }


        #endregion
        #region Ctors and methods   
        public MainViewModel(IFrameNavigationService mainFrameNavigationService,IDataService mainDataService) : base(mainFrameNavigationService, mainDataService)
        {            
        }

        private async Task LoadSettings()
        {
            await Task.Run(() =>
            {
                BranchCollection = new ObservableCollection<Branch>(MainDataService.GetAllBranchs());
                LocalSettings = MainDataService.GetLocalSettings();
                ServerSettings = MainDataService.GetServerSettings();
                WorkingHoursSettings = MainDataService.GetWorkingHoursSettings();
                SelectedBranch = BranchCollection?.First();
                StallsCollection = new ObservableCollection<Stall>(MainDataService.GetBranchStalls(SelectedBranch.Id));
            });

        }
        #endregion
    }
}