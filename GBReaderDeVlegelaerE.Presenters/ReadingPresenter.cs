using GBReaderDeVlegelaerE.Domains;
using GBReaderDeVlegelaerE.Presenters.routes;
using GBReaderDeVlegelaerE.Repositories;

namespace GBReaderDeVlegelaerE.Presenters
{
    public class ReadingPresenter
    {

        private readonly IReadingPage _readingPage;
        
        private readonly IBrowseToViews _routeur;

        private Book _book;

        private readonly ReadingStatistics _readingStatistics;

        private readonly IUserSessionRepository _userSessionRepository;
        
        public ReadingPresenter(IReadingPage readingPage, Book book, ReadingStatistics readingStatistics, IUserSessionRepository userSessionRepository,  IBrowseToViews routeur)
        {
            this._readingPage = readingPage;
            this._book = book;
            this._readingStatistics = readingStatistics;
            this._userSessionRepository = userSessionRepository;
            this._routeur = routeur;
            _readingPage.OnSwitchPageRequested += SeeTheSelectedPage;
            _readingPage.ComeBackToWelcomePageRequested += ComeBackToWelcomePage;
            _readingPage.RestartReadingRequested += RestartReading;
        }

        private void ComeBackToWelcomePage(object? sender, EventArgs e)
        {
            _routeur.GoTo("WelcomePage");
        }

        private void RestartReading(object? sender, EventArgs e)
        {
            //_readingPage.DisplayPageAndTheChoices(BookMapper.ConvertPageToPageViewModel(_book.PagesOfTheBook[0]));
            //SendPageToDisplay(BookMapper.ConvertPageToPageViewModel(_book.PagesOfTheBook[0]));
            //La session utilisateur a été détruite lorsque l'utilisateur a atteint une page Terminale. On en recrée une
            int lastPage = _readingStatistics.SearchAReadingSession(_book.Isbn!.IsbnOfTheBook, _book.PagesOfTheBook![0].NumPage, true);
            SendPageToDisplay(BookMapper.ConvertPageToPageViewModel(_book.PagesOfTheBook[lastPage - 1]));
        }

     

        /// <summary>
        /// Cette méthode permet d'aller chercher la la page que l'utilisateur souhaite lire. De plus,
        /// elle met à jour la session de lecture.
        /// </summary>
        /// <param name="sender">Informations de transmission reçues depuis la ReadingPage</param>
        /// <param name="e">Vue modèle d'un choix reçu depuis la ReadingPage</param>
        private void SeeTheSelectedPage(object? sender, ChoiceViewModel e)
        {
            //Ici, je vais chercher la page dans ma liste de livre. Comme elle est triée, ce sera toujours l'index de la page souhaitée - 1 pour sa position dans la liste.
            PageViewModel pageSouhaitee = BookMapper.ConvertPageToPageViewModel(_book.PagesOfTheBook![e.GetNumPageTo() - 1]);
            SendPageToDisplay(pageSouhaitee);
            //Ici, il vaut mettre à jour la session de lecture en envoyant la variable pageSouhaitee qui est la dernière variable lue par l'utilisateur
            Page page = BookMapper.ConvertPageViewModelToPage(pageSouhaitee);
            bool deletePage = page.ThisIsATerminalePage();
            _readingStatistics.UpdateTheLastPageReading(_book.Isbn!.IsbnOfTheBook, page.NumPage, deletePage);
        }
        

        /// <summary> 
        /// Reçoit le livre complètement chargé depuis le welcomePresenter
        /// </summary>
        /// <param name="currentBook">Le livre avec les informations complètes sur lequelle a cliqué le lecteur</param>
        public void SendBookToLoadComplete(Book currentBook)
        {
            //S'il y a eu un message d'erreur auparavant
            _readingPage.ClearErrorMessage();
            //Le livre en cours de lecture
            _book = currentBook;
            //Rechercher la dernière page de lecture. S'il n'y a pas de session lié à un livre, on envoie la première page du livre courant
            int lastPage = 0;
            lastPage = _readingStatistics.SearchAReadingSession(currentBook.Isbn!.IsbnOfTheBook,
                 _book.PagesOfTheBook![0].NumPage, false);
            if (_book.PagesOfTheBook[lastPage - 1].ThisIsATerminalePage())
            {
                //J'irai supprimer pour recréer une nouvelle session dans le cas où il n'y a qu'une page (cas dégradé)
                lastPage = _readingStatistics.SearchAReadingSession(currentBook.Isbn.IsbnOfTheBook,
                    _book.PagesOfTheBook[0].NumPage, true);
            }

            //Vu ma liste triée, lorsque je récupère l'entier lastPage, l'objet page se trouve à l'index du numéro de page - 1.
            SendPageToDisplay(BookMapper.ConvertPageToPageViewModel(_book.PagesOfTheBook[lastPage - 1]));
        }

        private void SendPageToDisplay(PageViewModel pageSouhaitee)
        {
            _readingPage.DisplayPageAndTheChoices(pageSouhaitee);  
        }

        /// <summary>
        /// Cette méthode permet d'aller sauvegarder les sessions de lecture dans le json
        /// </summary>
        public void SaveSessionReadings()
        {
            _userSessionRepository.Save(_readingStatistics.UserReadings);
        }

        /// <summary>
        /// Cette méthode permet d'afficher une erreur lors
        /// du chargement complet d'un livre
        /// </summary>
        /// <param name="exMessage">message d'erreur</param>
        public void ErrorLoadingCompletBook(string exMessage)
        {
            _readingPage.DisplayErrorMessage(exMessage);
        }
    }
}