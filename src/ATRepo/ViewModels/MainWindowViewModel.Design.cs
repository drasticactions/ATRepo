// <copyright file="MainWindowViewModel.Design.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using FishyFlip.Lexicon;
using FishyFlip.Lexicon.App.Bsky.Feed;

namespace ATRepo.ViewModels;

/// <summary>
/// Design-time ViewModel for the main window.
/// </summary>
public class MainWindowViewModel_Design : MainWindowViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel_Design"/> class.
    /// </summary>
    public MainWindowViewModel_Design()
    {
        this.ATObjects.Add(Post.FromJson("{\"$type\":\"app.bsky.feed.post\",\"text\":\"I can\\u2019t wait for next weeks Guys\\u002B episode so I can watch them in reverse order.\",\"langs\":[\"en\"],\"createdAt\":\"2025-05-16T16:24:58.344Z\"}"));

        this.SelectedATObject = this.ATObjects[0];
    }
}