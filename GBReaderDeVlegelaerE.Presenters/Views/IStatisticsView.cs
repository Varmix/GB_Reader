namespace GBReaderDeVlegelaerE.Presenters
{
    public interface IStatisticsView
    {
        void DisplayAStatView(UserSessionViewModel userSessionViewModel);

        event EventHandler ComeBackToWelcomePageRequested;
    }
}