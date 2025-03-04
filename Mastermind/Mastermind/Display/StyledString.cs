using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mastermind.Display;

public class StyledString
{
    public StyledString()
    {
    }

    public StyledString(string value, ConsoleColor? foreground = ConsoleColor.White, ConsoleColor? background = ConsoleColor.Black, double? center = null) : this()
    {
        Value = value;
        ForegroundColor = foreground;
        BackgroundColor = background;
        CenterPosition = center;
    }

    public ConsoleColor? ForegroundColor { get; set; }
    public ConsoleColor? BackgroundColor { get; set; }
    public string? Value { get; set; }
    public double? CenterPosition { get; set; }

    public static JsonSerializerOptions DefaultOptions => new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false,
        Converters = { new JsonStringEnumConverter() }
    };

    public string ToJson()
    {
        return JsonSerializer.Serialize(this, DefaultOptions) + ";";
    }

    public static StyledString FromJson(string json)
    {
        return JsonSerializer.Deserialize<StyledString>(json, DefaultOptions);
    }
}