using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointementScheduleBoard.Helpers;
using DataLayer.DataService;
using GalaSoft.MvvmLight.Command;

namespace AppointementScheduleBoard.ViewModel
{
    public class AffectationViewModel:NavigableViewModelBase
    {
        #region Fields

        #endregion
        #region Properties

        #endregion
        #region Commands
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
        public AffectationViewModel(IFrameNavigationService mainFrameNavigationService, IDataService mainDataService) : base(mainFrameNavigationService, mainDataService)
        {
        }
        #endregion

    }
}
