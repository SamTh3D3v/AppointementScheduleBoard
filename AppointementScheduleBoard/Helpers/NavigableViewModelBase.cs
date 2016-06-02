using DataLayer.DataService;
using GalaSoft.MvvmLight;

namespace AppointementScheduleBoard.Helpers
{
    public class NavigableViewModelBase : ViewModelBase
    {
        #region Fields
        protected IFrameNavigationService MainFrameNavigationService;
        protected IDataService MainDataService;
        #endregion
        #region Ctors and Methods
        public NavigableViewModelBase(IFrameNavigationService mainFrameNavigationService,IDataService mainDataService)
        {
            MainFrameNavigationService = mainFrameNavigationService;
            MainDataService = mainDataService;
        }
        #endregion
    }
}
