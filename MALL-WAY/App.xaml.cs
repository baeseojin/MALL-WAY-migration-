using System;
using System.Windows;

namespace MallWay
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                var settings = KioskSettings.Load();
                DisplaySettings.Current.SetLanguage(false);
                DisplaySettings.Current.SetLowVision(false);
                var repository = new MapRepository(settings.DatabasePath);
                repository.Initialize();
                var routeService = new RouteService(repository);

                MainWindow = new MainWindow
                {
                    DataContext = new MainViewModel(settings, repository, routeService)
                };
                MainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("키오스크를 시작할 수 없습니다.\n" + ex.Message, "시작 오류",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown(1);
            }
        }
    }
}
