using System.Threading.Tasks;
using AppointementScheduleBoard.Helpers;
using DataLayer.DataService;
using DataLayer.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;

namespace AppointementScheduleBoard.ViewModel
{
   
    public class MainViewModel : NavigableViewModelBase
    {
        #region Fields
        private bool _isSettingsFlayoutOpen;
        private bool _showTitleBarProperty;
        private bool _ignoreTaskbarOnMaximizeProperty;
        private LocalSettings _localSettings;
        private ServerSettings _serverSettings;
        #endregion
        #region Properties              
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
                        
                        MainFrameNavigationService.NavigateTo(App.ScheduleBoardViewKey);
                        ShowTitleBarProperty = true;
                        IgnoreTaskbarOnMaximizeProperty=false;
                        await LoadSettings();
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
                        MainFrameNavigationService.NavigateTo(App.AffectationViewKey);
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
                LocalSettings = MainDataService.GetLocalSettings();
                ServerSettings = MainDataService.GetServerSettings();
            });
       
        }
        #endregion
    }
}