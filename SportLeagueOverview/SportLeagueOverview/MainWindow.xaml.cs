using MahApps.Metro.Controls;
using SportLeagueOverview.Core.Controls;

namespace SportLeagueOverview
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : MetroWindow
  {
    public MainWindow()
    {
      InitializeComponent();
      DataContext = new MainViewModel();
    }

    private void HamburgerMenuControl_OnItemClick(object sender, ItemClickEventArgs e)
    {
      if (DataContext is MainViewModel VM)
      {
        HamburgerMenuControl.Content = e.ClickedItem;
        var SelectedGlyphItem = (OwnGlyphItem)e.ClickedItem;
      }
      HamburgerMenuControl.MouseLeave += (sender, e) =>
        {
          HamburgerMenuControl.IsPaneOpen = false;
        };
    }
  }
}
