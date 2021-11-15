using SportLeagueOverview.Core;
using SportLeagueOverview.ViewModels;

namespace SportLeagueOverview.Views
{
  /// <summary>
  /// Interaction logic for SpieleView.xaml
  /// </summary>
  public partial class SpieleView : UserControlBase
  {
    public SpieleView()
    {
      InitializeComponent();
      DataContext = new SpieleViewModel();
    }
  }
}
