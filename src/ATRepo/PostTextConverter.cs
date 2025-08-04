// <copyright file="PostTextConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Globalization;
using Avalonia.Data.Converters;
using FishyFlip.Lexicon.App.Bsky.Feed;

namespace ATRepo;

/// <summary>
/// Converts a post to its text representation.
/// </summary>
public class PostTextConverter : IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Post post)
        {
            return null;
        }

        return MarkdownConverter.Convert(post.Text ?? string.Empty, post.Facets);
    }

    /// <inheritdoc/>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}