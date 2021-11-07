using SportLeagueOverview.Core;

namespace SportLeagueOverview.ViewModels
{
  public class SpielerViewModel : ViewModelBase
  {
    private string m_SpielerName;

    public SpielerViewModel()
    {
    }

    public string SpielerName
    {
      get => m_SpielerName;
      set
      {
        m_SpielerName = value;
        OnPropertyChanged(nameof(SpielerName));
      }
    }

  }
}
