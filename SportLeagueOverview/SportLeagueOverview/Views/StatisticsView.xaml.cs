using SportLeagueOverview.Core;
using SportLeagueOverview.ViewModels;

namespace SportLeagueOverview.Views
{
  /// <summary>
  /// Interaction logic for StatisticsView.xaml
  /// </summary>
  public partial class StatisticsView : UserControlBase
  {
    public StatisticsView()
    {
      InitializeComponent();
      DataContext = new StatisticsViewModel();
    }
  }
}
