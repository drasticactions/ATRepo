// <copyright file="MainWindow.axaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using ATRepo.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Classic.Avalonia.Theme;

namespace ATRepo;

public partial class MainWindow : ClassicWindow
{
    public MainWindow()
    {
        this.InitializeComponent();
        this.DataContext = new MainWindowViewModel();
    }
}