using SportLeagueOverview.Core;
using System;

namespace SportLeagueOverview.ViewModels
{
  public class SpielerViewModel : ViewModelBase
  {
    private string m_SpielerName;
    private string m_RückenNummer;
    private DateTime m_Geburtsdatum;

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

    public string RückenNummer
    {
      get => m_RückenNummer;
      set
      {
        m_RückenNummer = value;
        OnPropertyChanged(nameof(RückenNummer));
      }
    }

    public DateTime Geburtsdatum
    {
      get => m_Geburtsdatum;
      set
      {
        m_Geburtsdatum = value;
        OnPropertyChanged(nameof(Geburtsdatum));
      }
    }

  }
}
