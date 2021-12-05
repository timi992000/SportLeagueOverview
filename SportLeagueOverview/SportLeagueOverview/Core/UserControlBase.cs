using MahApps.Metro.Controls;
using SportLeagueOverview.Core.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace SportLeagueOverview.Core
{
  public class UserControlBase : UserControl
  {
    public UserControlBase()
    {
      Loaded += UserControlBase_Loaded;
    }

    private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
    {
      __UpdateControls();

      Loaded -= UserControlBase_Loaded;
    }

    public void __UpdateControls()
    {
      //Update Controls and Set special things to it so u dont need to do it at all xaml controls
      __SetRulesForAllTextBoxes();
      __SetRulesForAllNumericBoxes();
      __SetRulesForAllLabels();
      __SetRulesForAllDatePickers();
      __SetRulesForAllDataGrids();
    }

    private void __SetRulesForAllTextBoxes()
    {
      var TextBoxes = FindVisualChildren<TextBox>(this).ToList();
      if (TextBoxes.Any())
      {
        foreach (var TextBox in TextBoxes)
        {
          TextBoxHelper.SetClearTextButton(TextBox, false);
          TextBox.TextChanged += (sender, eventArgs) =>
          {
            if (sender is System.Windows.Controls.TextBox ChangedTextBox)
            {
              if (ChangedTextBox.Text != null && ChangedTextBox.Text.Length > 0)
                TextBoxHelper.SetClearTextButton(ChangedTextBox, true);
              else
                TextBoxHelper.SetClearTextButton(ChangedTextBox, false);
            }
          };
          TextBoxHelper.SetSelectAllOnFocus(TextBox, true);
          //TextBoxHelper.SetAutoWatermark(TextBox, true);
          TextBox.MaxHeight = 22;
          TextBox.Height = 22;
          TextBox.VerticalContentAlignment = VerticalAlignment.Center;
          TextBox.VerticalAlignment = VerticalAlignment.Top;
        }
      }
    }

    private void __SetRulesForAllNumericBoxes()
    {
      var NumericBoxes = FindVisualChildren<NumericBox>(this).ToList();
      if (NumericBoxes.Any())
      {
        foreach (var NumericTextBox in NumericBoxes)
        {
          TextBoxHelper.SetClearTextButton(NumericTextBox, false);
          NumericTextBox.TextChanged += (sender, eventArgs) =>
          {
            if (sender is NumericBox CastedNumericBox)
            {
              if (CastedNumericBox.Text != null && CastedNumericBox.Text.Length > 0)
                TextBoxHelper.SetClearTextButton(CastedNumericBox, true);
              else
                TextBoxHelper.SetClearTextButton(CastedNumericBox, false);
            }
          };
          TextBoxHelper.SetSelectAllOnFocus(NumericTextBox, true);
          NumericTextBox.MaxHeight = 22;
          NumericTextBox.Height = 22;
          HorizontalAlignment = HorizontalAlignment.Stretch;
          NumericTextBox.VerticalContentAlignment = VerticalAlignment.Center;
          NumericTextBox.VerticalAlignment = VerticalAlignment.Top;
        }
      }
    }

    private void __SetRulesForAllLabels()
    {
      var AllLabels = FindVisualChildren<Label>(this).ToList();
      if (AllLabels.Any())
      {
        foreach (var Label in AllLabels)
        {
          Label.Height = 30;
          Label.VerticalAlignment = VerticalAlignment.Top;
        }
      }
    }

    private void __SetRulesForAllDataGrids()
    {
      var AllDataGrids = FindVisualChildren<DataGrid>(this).ToList();
      if (AllDataGrids.Any())
      {
        foreach (var tmpDataGrid in AllDataGrids)
        {
          tmpDataGrid.IsReadOnly = true;
          tmpDataGrid.SelectionMode = DataGridSelectionMode.Single;
          tmpDataGrid.AutoGenerateColumns = true;
          tmpDataGrid.ContextMenu = new ContextMenu();

          var DeleteMenuItem = new MenuItem ();
          DeleteMenuItem.SetBinding(MenuItem.CommandProperty, "Delete");
          DeleteMenuItem.Header = "Delete";

          var ReloadMenuItem = new MenuItem();
          ReloadMenuItem.SetBinding(MenuItem.CommandProperty, "Reload");
          ReloadMenuItem.Header = "Reload";

          var NewMenuItem = new MenuItem();
          NewMenuItem.SetBinding(MenuItem.CommandProperty, "New");
          NewMenuItem.Header = "New";

          tmpDataGrid.ContextMenu.Items.Add(DeleteMenuItem);
          tmpDataGrid.ContextMenu.Items.Add(ReloadMenuItem);
          tmpDataGrid.ContextMenu.Items.Add(NewMenuItem);
        }
      }
    }

    private void __SetRulesForAllDatePickers()
    {
      var AllDatePicker = FindVisualChildren<DatePicker>(this).ToList();
      if (AllDatePicker.Any())
      {
        foreach (var DatePicker in AllDatePicker)
        {
          DatePicker.Height = 30;
          DatePicker.MaxHeight = 30;
          DatePicker.VerticalAlignment = VerticalAlignment.Top;
        }
      }
    }

    public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
    {
      if (depObj != null)
      {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
        {
          DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
          if (child != null && child is T)
          {
            yield return (T)child;
          }

          foreach (T childOfChild in FindVisualChildren<T>(child))
          {
            yield return childOfChild;
          }
        }
      }
    }

  }
}
