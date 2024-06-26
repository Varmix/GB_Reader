namespace GBReaderDeVlegelaerE.Presenters
{
    public class MainPresenter
    {

        private ReadingPresenter _readingPresenter;

        private StatisticsPresenter _statisticsPresenter;

        private WelcomePresenter _welcomePresenter;
        
        public MainPresenter(ReadingPresenter readingPresenter, StatisticsPresenter statisticsPresenter, WelcomePresenter welcomePresenter)
        {
            _readingPresenter = readingPresenter;
            _statisticsPresenter = statisticsPresenter;
            _welcomePresenter = welcomePresenter;
            welcomePresenter.setMainPresenter(this);
        }

        public ReadingPresenter GetReadingPresenter() => _readingPresenter;

        public StatisticsPresenter GetStatistictsPresenter() => _statisticsPresenter;

        public WelcomePresenter GetWelcomePresenter() => _welcomePresenter;
    }
}