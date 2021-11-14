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
        OwnGlyphItem SelectedGlyphItem = (OwnGlyphItem)e.ClickedItem;
        switch (SelectedGlyphItem.Label)
        {
          default:
          case "Mannschaft":
            HamburgerMenuControl.DataContext = VM.MannschaftViewModel;
            break;
          case "Spiele":
            HamburgerMenuControl.DataContext = VM.SpieleViewModel;
            break;
          case "Statistiken":
            HamburgerMenuControl.DataContext = VM.StatistikenViewModel;
            break;
          case "Spieler":
            HamburgerMenuControl.DataContext = VM.SpielerViewModel;
            break;
        }
      }
      HamburgerMenuControl.IsPaneOpen = false;
    }
  }
}
