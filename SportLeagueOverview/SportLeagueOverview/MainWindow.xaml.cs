using MahApps.Metro.Controls;

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
      // set the content
      this.HamburgerMenuControl.Content = e.ClickedItem;
      // close the pane
      this.HamburgerMenuControl.IsPaneOpen = false;
    }
  }
}
