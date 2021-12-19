using SportLeagueOverview.Core.Entitites;
using System;
using System.Windows.Data;
using static System.Net.Mime.MediaTypeNames;

namespace SportLeagueOverview.Core.Extender
{
    [ValueConversion(typeof(EventType), typeof(Image))]
    public class EventTypeToFileNameConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((EventType)value)
            {
                case EventType.YellowCard:
                    return @"\Views\Images\yellowCard.png";
                case EventType.RedCard:
                    return @"\Views\Images\redCard.png";
                case EventType.Goal:
                    return @"\Views\Images\goal.png";
                case EventType.OwnGoal:
                    return @"\Views\Images\owngoal.png";
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
