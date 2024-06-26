using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GBReaderDeVlegelaerE.Avalonia.Controls;
using GBReaderDeVlegelaerE.Presenters;

namespace GBReaderDeVlegelaerE.Avalonia.Pages
{
    public partial class StatisticsPage : UserControl, IStatisticsView
    {
        public StatisticsPage()
        {
            InitializeComponent();
        }
        

        private void ComeBackToWelcomePage_OnClick(object? sender, RoutedEventArgs e)
        {
            ComeBackToWelcomePageRequested?.Invoke(sender, e);
            StatsContainer.Children.Clear();
        }

        public void DisplayAStatView(UserSessionViewModel userSessionViewModel)
        {
            var statElement = new UserSessionView();
            statElement.SetUserSessionViewModel(userSessionViewModel);
            StatsContainer.Children.Add(statElement);
        }

        public event EventHandler ComeBackToWelcomePageRequested;
    }
}