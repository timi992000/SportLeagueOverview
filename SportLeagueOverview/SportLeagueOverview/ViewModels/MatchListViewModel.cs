using SportLeagueOverview.Core;
using SportLeagueOverview.Core.Entitites;
using System.Collections.ObjectModel;

namespace SportLeagueOverview.ViewModels
{
  public class MatchListViewModel : ViewModelBase<MatchEntity>
  {
    private ObservableCollection<MatchViewModel> m_DisplayMatches;
    public MatchListViewModel()
      : base(false)
    {
      DisplayMatches = new ObservableCollection<MatchViewModel>();
      CurrentItems.ForEach(x => DisplayMatches.Add(new MatchViewModel()));
      EntitySpectator.EntitiesChanges += (sender, e) =>
      {
        Execute_Reload(this);
        DisplayMatches.Clear();
        CurrentItems.ForEach(x => DisplayMatches.Add(new MatchViewModel()));
      }; 
      ReloadRequested += (sender, e) =>
      {
        DisplayMatches.Clear();
        CurrentItems.ForEach(x => DisplayMatches.Add(new MatchViewModel()));
      };
      //DisplayMatches.Add(new MatchViewModel());
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
      CurrentItem = new MatchEntity() { IsNew = true };
      //var NewId = DBAccess.GetHighestId(new MatchEntity()) + 1;
      var Window = new MatchDetailWindow(new MatchViewModel(true) { IsNewMatch = true });
      Window.ShowDialog();
      this.Reload.Execute(null);
    }

  }
}

