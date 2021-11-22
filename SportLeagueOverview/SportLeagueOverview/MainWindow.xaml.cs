using MahApps.Metro.Controls;
using SportLeagueOverview.Core.Controls;
using SportLeagueOverview.Views;
using System;

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
      HamburgerMenuControl.Content = new OwnGlyphItem { Label = $"Willkommen {Environment.UserName} bei der Mannschaftsverwaltung", Tag = new DefaultView() };
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
