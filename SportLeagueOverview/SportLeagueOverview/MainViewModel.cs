using ControlzEx.Theming;
using SportLeagueOverview.Core;
using SportLeagueOverview.Core.Common;
using SportLeagueOverview.Core.Entitites;
using SportLeagueOverview.ViewModels;
using System.Linq;
using System.Windows;

namespace SportLeagueOverview
{
  public class MainViewModel : ViewModelBase
  {
    private MannschaftViewModel m_MannschaftViewModel;
    private SpielerViewModel m_SpielerViewModel;
    private SpieleViewModel m_SpieleViewModel;
    private StatistikenViewModel m_StatistikenViewModel;

    public MainViewModel()
    {
      ThemeManager.Current.ChangeTheme(Application.Current, "Light.Blue");
      DatenbankHelfer.Initialize();
    }

    public MannschaftViewModel MannschaftViewModel
    {
      get
      {
        if (m_MannschaftViewModel == null)
          m_MannschaftViewModel = new MannschaftViewModel();
        return m_MannschaftViewModel;
      }
    }

    public SpielerViewModel SpielerViewModel
    {
      get
      {
        if (m_SpielerViewModel == null)
          m_SpielerViewModel = new SpielerViewModel();
        return m_SpielerViewModel;
      }
    }

    public SpieleViewModel SpieleViewModel
    {
      get
      {
        if (m_SpieleViewModel == null)
          m_SpieleViewModel = new SpieleViewModel();
        return m_SpieleViewModel;
      }
    }

    public StatistikenViewModel StatistikenViewModel
    {
      get
      {
        if (m_StatistikenViewModel == null)
          m_StatistikenViewModel = new StatistikenViewModel();
        return m_StatistikenViewModel;
      }
    }
  }
}