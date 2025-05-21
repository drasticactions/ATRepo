using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using FishyFlip.Lexicon;
using FishyFlip.Lexicon.Com.Atproto.Repo;

namespace ATRepo;

public class PostTemplateSelector : IDataTemplate
{
    [Content]
    public Dictionary<string, IDataTemplate> AvailableTemplates { get; } = new Dictionary<string, IDataTemplate>();

    public Control Build(object? param)
    {
        var key = param?.ToString();
        if (key is null)
        {
            throw new ArgumentNullException(nameof(param));
        }

        var value = this.AvailableTemplates[key];
        var defaultValue = this.AvailableTemplates["Default"];

        return value is null ? defaultValue.Build(param) : value.Build(param);
    }

    public bool Match(object? data)
    {
        var record = data as ATObject;
        var key = record?.Type;

        return data is ATObject
               && !string.IsNullOrEmpty(key)
               && this.AvailableTemplates.ContainsKey(key);
    }
}