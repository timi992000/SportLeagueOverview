using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
      __SetRulesForAllLabels();
      __SetRulesForALlDatePickers();
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
          TextBox.MaxHeight = 30;
          TextBox.VerticalAlignment = VerticalAlignment.Top;
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

    private void __SetRulesForALlDatePickers()
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
