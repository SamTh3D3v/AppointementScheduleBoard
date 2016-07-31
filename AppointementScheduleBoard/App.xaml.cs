using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace AppointementScheduleBoard
{
    public partial class App : Application
    {
        #region Views keys
        public static String ScheduleBoardViewKey = "ScheduleBoardView";
        public static String AffectationViewKey = "AffectationView";
        #endregion

        public ResourceDictionary ThemeDictionary => Resources.MergedDictionaries[0];
        public void ChangeTheme(Uri uri)
        {
            ThemeDictionary.MergedDictionaries.RemoveAt(0);
            ThemeDictionary.MergedDictionaries.Insert(0,new ResourceDictionary() { Source = uri });
        }


        public App():base()
        {
            DispatcherHelper.Initialize();
            Application.Current.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(DomainUnhandlerEceptionHandler);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr");

        }

        public async void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var errorMessage = $"An exception occurred: {e.Exception.Message}";
            var window = (Application.Current.MainWindow as MetroWindow);
            if (window == null)
            {
                Debug.WriteLine(errorMessage);
                e.Handled = true;
                return;
            }
            await (window.ShowMessageAsync("Something went wrong, Details :", errorMessage));
            e.Handled = true;

        }

        public async void DomainUnhandlerEceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            var errorMessage = $"An exception occurred: {args.ExceptionObject.ToString()}";
            var controller = await ((Application.Current.MainWindow as MetroWindow).ShowMessageAsync("Something went wrong, Details :", errorMessage));
        }
    }
}
