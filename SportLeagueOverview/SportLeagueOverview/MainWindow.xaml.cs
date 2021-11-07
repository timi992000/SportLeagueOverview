using MahApps.Metro.Controls;

namespace SportLeagueOverview
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : MetroWindow
  {
    public MainWindow()
    {
      InitializeComponent();
      DataContext = new MainViewModel();
    }
  }
}
