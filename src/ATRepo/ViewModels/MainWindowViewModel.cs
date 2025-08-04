// <copyright file="MainWindowViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishyFlip.Lexicon;
using FishyFlip.Tools;

namespace ATRepo.ViewModels;

/// <summary>
/// ViewModel for the main window.
/// </summary>
public partial class MainWindowViewModel : ObservableObject
{
    /// <summary>
    /// Gets or sets the selected ATObject.
    /// </summary>
    [ObservableProperty]
    private ATObject? selectedATObject;

    /// <summary>
    /// Gets or sets a value indicating whether the ViewModel is busy.
    /// </summary>
    [ObservableProperty]
    private bool isBusy = false;

    /// <summary>
    /// Gets the collection of ATObjects.
    /// </summary>
    public ObservableCollection<ATObject> ATObjects { get; } = new();

    [RelayCommand]
    private void Exit()
    {
    }

    [RelayCommand(CanExecute = nameof(CanOpenRepoFile))]
    private async Task OpenRepoFileAsync(CancellationToken token)
    {
        var mainWindow = this.GetMainWindow();
        if (mainWindow is null)
        {
            return;
        }

        var options = new FilePickerOpenOptions
        {
            AllowMultiple = false,
            FileTypeFilter = new List<FilePickerFileType>
            {
                new FilePickerFileType("ATRepo File")
                {
                    Patterns = new List<string> { "*.repo" },
                },
            },
        };
        var result = await mainWindow.StorageProvider.OpenFilePickerAsync(options);
        if (result.Count == 0)
        {
            return;
        }

        this.ATObjects.Clear();
        await using var stream = await result[0].OpenReadAsync();
        var repoFile = CarDecoder.DecodeRepoAsync(stream);
        await foreach (var item in repoFile)
        {
            this.ATObjects.Add(item);
        }
    }

    private MainWindow? GetMainWindow()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            return desktop.MainWindow as MainWindow;
        }

        return null;
    }

    private bool CanOpenRepoFile() => true;

    partial void OnSelectedATObjectChanged(ATObject? value)
    {
    }
}