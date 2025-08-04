// <copyright file="MainWindow.axaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using ATRepo.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Classic.Avalonia.Theme;

namespace ATRepo;

/// <summary>
/// Main window for the application.
/// </summary>
public partial class MainWindow : ClassicWindow
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        this.InitializeComponent();
        this.DataContext = new MainWindowViewModel();
    }
}