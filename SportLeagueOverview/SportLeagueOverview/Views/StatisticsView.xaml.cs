using SportLeagueOverview.Core;
using SportLeagueOverview.ViewModels;

namespace SportLeagueOverview.Views
{
  /// <summary>
  /// Interaction logic for StatistikenView.xaml
  /// </summary>
  public partial class StatistikenView : UserControlBase
  {
    public StatistikenView()
    {
      InitializeComponent();
      DataContext = new StatisticsViewModel();
    }
  }
}
