// <copyright file="MarkdownConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Text;
using FishyFlip.Lexicon.App.Bsky.Richtext;

namespace ATRepo;

/// <summary>
/// Converts a post with facets to markdown.
/// </summary>
public static class MarkdownConverter
{
    /// <summary>
    /// Convert a post with facets to markdown.
    /// </summary>
    /// <param name="text">The post text.</param>
    /// <param name="facets">The facets in the post.</param>
    /// <returns>A markdown representation of the post.</returns>
    public static string Convert(string text, IEnumerable<Facet>? facets)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        if (facets == null || !facets.Any())
        {
            return text;
        }

        // Sort facets by start index in descending order to avoid index shifting
        var sortedFacets = facets.OrderByDescending(f => f.Index.ByteStart).ToList();

        // Convert text to bytes for accurate indexing
        var textBytes = Encoding.UTF8.GetBytes(text);
        var result = text;

        foreach (var facet in sortedFacets)
        {
            if (facet.Features == null || !facet.Features.Any())
            {
                continue;
            }

            foreach (var feature in facet.Features)
            {
                // Get the original segment of text that the facet applies to
                string originalText = string.Empty;
                if (facet.Index.ByteStart >= 0 && facet.Index.ByteEnd <= textBytes.Length)
                {
                    int startCharIndex = Encoding.UTF8.GetCharCount(textBytes, 0, (int)facet.Index.ByteStart);
                    int length = Encoding.UTF8.GetCharCount(textBytes, (int)facet.Index.ByteStart, (int)(facet.Index.ByteEnd - facet.Index.ByteStart));
                    originalText = result.Substring(startCharIndex, length);
                }

                string markdownText = originalText;

                // Apply markdown formatting based on feature type
                if (feature is FishyFlip.Lexicon.App.Bsky.Richtext.Link link)
                {
                    markdownText = $"[{originalText}]({link.Uri})";
                }
                else if (feature is FishyFlip.Lexicon.App.Bsky.Richtext.Mention mention)
                {
                    markdownText = $"[{originalText}]({mention.Did})";
                }
                else if (feature is FishyFlip.Lexicon.App.Bsky.Richtext.Tag tag)
                {
                    markdownText = $"[{originalText}]({tag.TagValue})";
                }

                // Replace the original text with the markdown version
                if (facet.Index.ByteStart >= 0 && facet.Index.ByteEnd <= textBytes.Length)
                {
                    int startCharIndex = Encoding.UTF8.GetCharCount(textBytes, 0, (int)facet.Index.ByteStart);
                    int length = Encoding.UTF8.GetCharCount(textBytes, (int)facet.Index.ByteStart, (int)(facet.Index.ByteEnd - facet.Index.ByteStart));
                    result = result.Remove(startCharIndex, length).Insert(startCharIndex, markdownText);

                    // Since we're modifying the string, we need to recalculate byte positions
                    textBytes = Encoding.UTF8.GetBytes(result);
                }
            }
        }

        return result;
    }
}