using System;
using AppointementScheduleBoard.Helpers;
using DataLayer.DataService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace AppointementScheduleBoard.ViewModel
{

    public class ViewModelLocator
    {
        public static FrameNavigationService MainNavigationService;
        public static IDataService MainSampleDataService;
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, SampleDataService>();
            ////}
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ScheduleBoardViewModel>();
            SimpleIoc.Default.Register<AffectationViewModel>();
            SetupMainNavigationService();
            SetupDataService();
        }
        private static void SetupMainNavigationService()
        {
            MainNavigationService = new FrameNavigationService("MainFrame");

            // VMs
            MainNavigationService.Configure(App.ScheduleBoardViewKey, new Uri("../View/ScheduleBoardView.xaml", UriKind.Relative));
            MainNavigationService.Configure(App.AffectationViewKey, new Uri("../View/AffectationView.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<IFrameNavigationService>(() => MainNavigationService);
        }

        private static void SetupDataService()
        {
            //to use the test data service (local data)
            MainSampleDataService=new SampleDataService();
            //to use the PROD dataservice 
            //MainSampleDataService=new DataService();
            SimpleIoc.Default.Register<IDataService>(()=>MainSampleDataService);
        }

        public MainViewModel MainWindowViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ScheduleBoardViewModel ScheduleBoardViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScheduleBoardViewModel>();
            }
        }
      
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public AffectationViewModel AffectationViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AffectationViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}