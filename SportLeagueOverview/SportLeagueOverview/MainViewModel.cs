using ControlzEx.Theming;
using SportLeagueOverview.Core;
using SportLeagueOverview.Core.Common;
using SportLeagueOverview.Core.Entitites;
using SportLeagueOverview.ViewModels;
using System.Windows;

namespace SportLeagueOverview
{
  public class MainViewModel : ViewModelBase<TeamEntity>
  {
    private TeamViewModel m_MannschaftViewModel;
    private PlayerViewModel m_SpielerViewModel;
    private MatchListViewModel m_SpieleViewModel;
    private StatisticsViewModel m_StatistikenViewModel;

    public MainViewModel()
    {
      ThemeManager.Current.ChangeTheme(Application.Current, "Light.Blue");
      DBAccess.Initialize();
    }

    public TeamViewModel MannschaftViewModel
    {
      get
      {
        if (m_MannschaftViewModel == null)
          m_MannschaftViewModel = new TeamViewModel();
        return m_MannschaftViewModel;
      }
    }

    public PlayerViewModel SpielerViewModel
    {
      get
      {
        if (m_SpielerViewModel == null)
          m_SpielerViewModel = new PlayerViewModel();
        return m_SpielerViewModel;
      }
    }

    public MatchListViewModel SpieleViewModel
    {
      get
      {
        if (m_SpieleViewModel == null)
          m_SpieleViewModel = new MatchListViewModel();
        return m_SpieleViewModel;
      }
    }

    public StatisticsViewModel StatistikenViewModel
    {
      get
      {
        if (m_StatistikenViewModel == null)
          m_StatistikenViewModel = new StatisticsViewModel();
        return m_StatistikenViewModel;
      }
    }
  }
}