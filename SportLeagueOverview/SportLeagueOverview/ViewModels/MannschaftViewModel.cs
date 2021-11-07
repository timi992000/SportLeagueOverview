using SportLeagueOverview.Core;

namespace SportLeagueOverview.ViewModels
{
  public class MannschaftViewModel : ViewModelBase
  {
    private string m_MannschaftsName;

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
  }
}
