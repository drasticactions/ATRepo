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

public partial class MainWindowViewModel : ObservableObject
{
    /// <summary>
    /// Gets the collection of ATObjects.
    /// </summary>
    public ObservableCollection<ATObject> ATObjects { get; } = new();

    [ObservableProperty] private bool isBusy = false;

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
        using var stream = await result[0].OpenReadAsync();
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
}