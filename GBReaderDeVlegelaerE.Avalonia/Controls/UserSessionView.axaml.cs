using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GBReaderDeVlegelaerE.Presenters;

namespace GBReaderDeVlegelaerE.Avalonia.Controls
{
    public partial class UserSessionView : UserControl
    {
        public UserSessionView()
        {
            InitializeComponent();
        }
        
        public void SetUserSessionViewModel(UserSessionViewModel userSessionViewModel)
        {
            IsbnStat.Text =  $"{userSessionViewModel.GetIsbn()}";
            LastPageStat.Text = $"{userSessionViewModel.LastPage()}";
            StartReadingStat.Text = $"{userSessionViewModel.GetStartReading()}";
            LastUpdateStat.Text = $"{userSessionViewModel.GetLastUpdate()}";
        }
    }
}