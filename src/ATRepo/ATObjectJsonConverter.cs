using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia.Data.Converters;
using FishyFlip.Lexicon;

namespace ATRepo;

public class ATObjectJsonConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not ATObject atObject)
        {
            return null;
        }

        using JsonDocument jsonDocument = JsonDocument.Parse(atObject.ToJson());
        string prettifiedJson =
            System.Text.Json.JsonSerializer.Serialize(jsonDocument.RootElement, new
                JsonSerializerOptions
                {
                    WriteIndented = true,
                });
        return prettifiedJson;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}