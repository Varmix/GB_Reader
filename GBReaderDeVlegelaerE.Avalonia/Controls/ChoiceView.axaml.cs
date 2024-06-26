using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GBReaderDeVlegelaerE.Presenters;

namespace GBReaderDeVlegelaerE.Avalonia.Controls
{
    public partial class ChoiceView : UserControl
    {

        private ChoiceViewModel _choiceViewModel;
        
        public ChoiceView()
        {
            InitializeComponent();
        }
        

        public void SetChoiceViewModel(ChoiceViewModel choiceViewModel)
        {
            _choiceViewModel = choiceViewModel;
            ContentOfChoice.Text = choiceViewModel.ToString();
        }

        private void OnClick_DestinationPage(object? sender, RoutedEventArgs e)
        {
            OnChoiceClicked?.Invoke(this, _choiceViewModel);
        }

        public event EventHandler<ChoiceViewModel> OnChoiceClicked;
    }
}