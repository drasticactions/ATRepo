// <copyright file="PostTemplateSelector.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using FishyFlip.Lexicon;
using FishyFlip.Lexicon.Com.Atproto.Repo;

namespace ATRepo;

/// <summary>
/// Selects the appropriate template for a post based on its type.
/// </summary>
public class PostTemplateSelector : IDataTemplate
{
    /// <summary>
    /// Gets the available templates for posts.
    /// </summary>
    [Content]
    public Dictionary<string, IDataTemplate> AvailableTemplates { get; } = new Dictionary<string, IDataTemplate>();

    /// <summary>
    /// Builds the control for the specified parameter.
    /// </summary>
    /// <param name="param">Object Parameters.</param>
    /// <returns>Control.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
    public Control? Build(object? param)
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

    /// <summary>
    /// Matches the specified data against the available templates.
    /// </summary>
    /// <param name="data">Object.</param>
    /// <returns>Bool if match.</returns>
    public bool Match(object? data)
    {
        var record = data as ATObject;
        var key = record?.Type;

        return data is ATObject
               && !string.IsNullOrEmpty(key)
               && this.AvailableTemplates.ContainsKey(key);
    }
}