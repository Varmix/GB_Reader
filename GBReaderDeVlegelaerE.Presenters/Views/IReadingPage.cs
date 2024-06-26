using GBReaderDeVlegelaerE.Domains;

namespace GBReaderDeVlegelaerE.Presenters
{
    public interface IReadingPage
    {
        void DisplayPageAndTheChoices(PageViewModel pageViewModel);

        event EventHandler<ChoiceViewModel> OnSwitchPageRequested;

        event EventHandler ComeBackToWelcomePageRequested;

        event EventHandler RestartReadingRequested;
        void ClearErrorMessage();
        void DisplayErrorMessage(string exMessage);
    }
}