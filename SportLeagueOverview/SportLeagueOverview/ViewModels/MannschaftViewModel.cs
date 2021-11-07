using SportLeagueOverview.Core;

namespace SportLeagueOverview.ViewModels
{
  public class MannschaftViewModel : ViewModelBase
  {
    private string m_MannschaftsName;
    private string m_GründungsJahr;

    public MannschaftViewModel()
    {
    }

    public string MannschaftsName
    {
      get => m_MannschaftsName;
      set
      {
        m_MannschaftsName = value;
        OnPropertyChanged(nameof(MannschaftsName));
      }
    }

    public string GründungsJahr
    {
      get => m_GründungsJahr;
      set
      {
        m_GründungsJahr = value;
        OnPropertyChanged(nameof(GründungsJahr));
      }
    }
  }
}
