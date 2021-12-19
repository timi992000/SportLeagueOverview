using SportLeagueOverview.Core.Entitites;
using SportLeagueOverview.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static System.Net.Mime.MediaTypeNames;

namespace SportLeagueOverview.Core.Extender
{
    [ValueConversion(typeof(MatchState), typeof(Image))]
    public class MatchStateToFileNameConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((MatchState)value)
            {
                case MatchState.Open:
                    return @"\Views\Images\open.png";
                case MatchState.Pending:
                    return @"\Views\Images\pending.png";
                case MatchState.Done:
                    return @"\Views\Images\done.png";
                default:
                    return null;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
