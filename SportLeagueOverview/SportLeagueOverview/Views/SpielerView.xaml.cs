using SportLeagueOverview.Core;
using SportLeagueOverview.ViewModels;

namespace SportLeagueOverview.Views
{
  /// <summary>
  /// Interaction logic for SpielerView.xaml
  /// </summary>
  public partial class SpielerView : UserControlBase
  {
    public SpielerView()
    {
      InitializeComponent();
      DataContext = new SpielerViewModel();
    }
  }
}
