using SportLeagueOverview.Core;
using SportLeagueOverview.Core.Common;
using SportLeagueOverview.Core.Entitites;
using System.Collections.ObjectModel;

namespace SportLeagueOverview.ViewModels
{
  public class MatchListViewModel : ViewModelBase<MatchEntity>
  {
    private ObservableCollection<MatchViewModel> m_DisplayMatches;
    public MatchListViewModel()
    {
      DisplayMatches = new ObservableCollection<MatchViewModel>();
      CurrentItems.ForEach(x => DisplayMatches.Add(new MatchViewModel(x)));
      DisplayMatches.Add(new MatchViewModel(null));
    }

    public ObservableCollection<MatchViewModel> DisplayMatches
    {
      get
      {
        return m_DisplayMatches;
      }
      set
      {
        m_DisplayMatches = value;
        OnPropertyChanged(nameof(DisplayMatches));
      }

    }

    public override void Execute_New(object sender)
    {
      var Window = new MatchDetailWindow(new MatchViewModel(new MatchEntity()) { IsNew= true});
      Window.ShowDialog();
      this.Reload.Execute(null);
    }

  }
}

