using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SportLeagueOverview.Core
{
  public class ViewModelBase : INotifyPropertyChanged
  {
    public ViewModelBase()
    {
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string PropertyName = null)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
      }
    }

  }
}