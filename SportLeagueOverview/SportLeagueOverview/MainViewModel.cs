using ControlzEx.Theming;
using System.Windows;

namespace SportLeagueOverview
{
    internal class MainViewModel
    {
        public MainViewModel()
        {
            ThemeManager.Current.ChangeTheme(Application.Current, "Light.Blue");
        }
    }
}