using GalaSoft.MvvmLight.Views;

namespace AppointementScheduleBoard.Helpers
{
    public interface IFrameNavigationService : INavigationService
    {
        object Parameter { get; }
    }
}
