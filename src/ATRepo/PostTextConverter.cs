using System.Globalization;
using Avalonia.Data.Converters;
using FishyFlip.Lexicon.App.Bsky.Feed;

namespace ATRepo;

public class PostTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Post post)
        {
            return null;
        }

        return MarkdownConverter.Convert(post.Text, post.Facets);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}