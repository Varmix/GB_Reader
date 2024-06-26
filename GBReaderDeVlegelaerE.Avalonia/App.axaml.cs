using System;
using System.Collections.Generic;
using System.Data.Common;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GBReaderDeVlegelaerE.Avalonia.Pages;
using GBReaderDeVlegelaerE.Domains;
using GBReaderDeVlegelaerE.Infrastuctures.database;
using GBReaderDeVlegelaerE.Infrastuctures.file;
using GBReaderDeVlegelaerE.Presenters;
using GBReaderDeVlegelaerE.Repositories;

namespace GBReaderDeVlegelaerE.Avalonia
{
    public partial class App : Application
    {

        private MainWindow _mainWindow;
        private IGameBookRepository _bookStorage;
        private LibraryBook _libraryBook;
        private ReadingPresenter _readingPresenter;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                
                //Déclaration de la LibraryBook

                //SqlBookStorageFactory factory = new SqlBookStorageFactory();
                //Déclaration des vues
                 _mainWindow = new MainWindow();
                 _mainWindow.Opened += MainWindowOnOpened;
                 _mainWindow.Closed += MainWindowOnClosed;
                 desktop.MainWindow = _mainWindow;

            }

            base.OnFrameworkInitializationCompleted();
        }

        private void MainWindowOnClosed(object? sender, EventArgs e)
        {
            _readingPresenter.SaveSessionReadings();
        }

        private void MainWindowOnOpened(object? sender, EventArgs e)
        {
            _libraryBook = new ();
 
            try
            {
                BookStorageFactory factory = new (
                    "MySql.Data.MySqlClient",
                    "server=192.168.128.13;port=3306;database=in21b10054;uid=in21b10054;password=0054"
                );

                _bookStorage = factory.NewStorage(_libraryBook);
            }
            catch (Exception ex) when (ex is ProviderNotFoundException or UnableToConnectException or InvalidConnectionStringException)
            {
                _mainWindow.displayErrorMessage("Impossible d'établir une connexion avec la base de données");
                return;
            }
            
            CreateViewsAndWhatever();
        }

        private void CreateViewsAndWhatever()
        {
            
            //Déclaration des objets métiers
            Book gameBook = new Book();

            ReadingStatistics readingStatistics;
            
            
            IUserSessionRepository userSessionRepository = new UserSessionRepository();
            
            var welcomePage = new Pages.WelcomePage();
            WelcomePresenter welcomePresenter = new WelcomePresenter(welcomePage, _bookStorage, _libraryBook, _mainWindow);
            welcomePresenter.GetEachBookFromDb();
            
            try
            {
                readingStatistics = userSessionRepository.Load();
            }
            catch (GameBookStorageException)
            {
                readingStatistics = new ReadingStatistics();
                welcomePresenter.ErrorJson();
            }


            var readingPage = new ReadingPage();
            _readingPresenter = new ReadingPresenter(readingPage, gameBook, readingStatistics, userSessionRepository,  _mainWindow);
            
            var statisticsPage = new StatisticsPage();
            StatisticsPresenter statisticsPresenter = new StatisticsPresenter(statisticsPage, readingStatistics, _mainWindow);
            

            MainPresenter mainPresenter = new MainPresenter(_readingPresenter, statisticsPresenter, welcomePresenter);


            //Enregistrement des pages
            _mainWindow.RegisterPage("WelcomePage", welcomePage);
            _mainWindow.RegisterPage("ReadingPage", readingPage);
            _mainWindow.RegisterPage("StatsPage", statisticsPage);
            //Affichage de l'écran d'accueil
            _mainWindow.GoTo("WelcomePage");
        }
    }
}