using GBReaderDeVlegelaerE.Domains;
using GBReaderDeVlegelaerE.Presenters.routes;

namespace GBReaderDeVlegelaerE.Presenters
{
    public class StatisticsPresenter
    {

        private IStatisticsView _statisticsView;

        private ReadingStatistics _readingStatistics;

        private IBrowseToViews _routeur;
        public StatisticsPresenter(IStatisticsView statisticsPage, ReadingStatistics readingStatistics, IBrowseToViews routeur)
        {
            _statisticsView = statisticsPage;
            _readingStatistics = readingStatistics;
            _routeur = routeur;
            _statisticsView.ComeBackToWelcomePageRequested += SwitchToWelcomePage;
        }

        private void SwitchToWelcomePage(object? sender, EventArgs e)
        {
            _routeur.GoTo("WelcomePage");
        }


        public void DisplayStatistics()
        {
            IDictionary<string?, UserSession> userReadingSessions = _readingStatistics.UserReadings;
            foreach (var userReadingSession in userReadingSessions)
            {
                var userSessionViewModel = new UserSessionViewModel(userReadingSession.Key, userReadingSession.Value);
                _statisticsView.DisplayAStatView(userSessionViewModel);
            }
        }
    }
}