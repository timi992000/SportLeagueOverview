using System.Windows.Controls;
using System.Windows.Media;

namespace SportLeagueOverview.Core.Controls
{
  public class NumericBox : TextBox
  {
    public NumericBox()
    {
      HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
      Width = 40;
      TextChanged += (sender, e) =>
      {
        if (sender is TextBox tmpTextBox)
        {
          if (!int.TryParse(tmpTextBox.Text, out int _))
            BorderBrush = Brushes.Red;
          else
            tmpTextBox.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#FFABADB3");
        }

      };
    }

  }
}
