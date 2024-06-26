using System.Text.Json.Nodes;
using GBReaderDeVlegelaerE.Domains;
using GBReaderDeVlegelaerE.Presenters.routes;
using GBReaderDeVlegelaerE.Repositories;

namespace GBReaderDeVlegelaerE.Presenters;

public class WelcomePresenter
{
    private IWelcomeView _welcomeView;
    private IGameBookRepository _bookStorage;
    private LibraryBook _libraryBook;
    private readonly IBrowseToViews _router;
    private MainPresenter? _mainPresenter;
    

    /// <summary>
    /// Constructeur de la classe WelcomePresenter prenant
    /// en paramètre l'interface de la fenêtre principale, finalement le routeur qui permettra un changement de vue.
    /// Par la même occassion, notre classe connaître sa vue par son interface ainsi que le storage.
    /// Par contre, il connait la librarie par la concrète vu que c'est un objet modèle.
    /// </summary>
    /// <param name="welcomeView">L'interface de sa vue</param>
    /// <param name="bookStorage">L'interface du storage</param>
    /// <param name="libraryBook">la librairie contenant tous les livres</param>
    /// <param name="router">L'interface de la fenêtre principale</param>
    /// <param name="readingPresenter"></param>
    /// <param name="statisticsPresenter"></param>
    public WelcomePresenter(IWelcomeView welcomeView, IGameBookRepository bookStorage, LibraryBook libraryBook,
        IBrowseToViews router)
    {
        _welcomeView = welcomeView;
        _bookStorage = bookStorage;
        _libraryBook = libraryBook;
        _router = router;

        //Subscription events
        //J'appelle l'event de la mainWinow, je m'abonne et je vais lui donner la méthode de droite en réaction
        _welcomeView.FilterBook += WelcomePageOnFilterBook;
        _welcomeView.OnDetailsBookClick += OnClickContinueToReadRequested;
        _welcomeView.SeeStatisticsPage += OnClickSeeStatisticsRequested;
    }

    private void OnClickSeeStatisticsRequested(object? sender, string nextPage)
    {
        _router.GoTo(nextPage);
        _mainPresenter!.GetStatistictsPresenter().DisplayStatistics();
    }
    
    private void OnClickContinueToReadRequested(object? sender, GBBookViewModel eGbBookViewModel)
    {
        _router.GoTo("ReadingPage");
        //Book avec les informations basiques
        Book bookToComplete = BookMapper.convertBookViewModelToBookBasic(eGbBookViewModel);
        if (_libraryBook.VerifyIsABookIsAlreadyLoaded(bookToComplete.Isbn!.IsbnOfTheBook))
        {
            bookToComplete = _libraryBook.GetASpecificBookOnIsbn(bookToComplete.Isbn.IsbnOfTheBook);
        }
        else
        {
            try
            {
                _bookStorage.LoadCompleteBookInformation(bookToComplete);
            }
            catch (GameBookStorageException ex)
            {
                _mainPresenter!.GetReadingPresenter().ErrorLoadingCompletBook(ex.Message);
            }
        }
        _mainPresenter!.GetReadingPresenter().SendBookToLoadComplete(bookToComplete);
    }

    private void WelcomePageOnFilterBook(object? sender, string e)
    {
        FilterBooks(e);
    }
    public void GetEachBookFromDb()
    {
        //Lire les livres en bd
        try
        {
            _bookStorage.LoadCovers();
        }
        catch (GameBookStorageException)
        {
            _welcomeView.displayAlert("Erreur lors de la lecture des livres, merci de relancer l'application");
        }

        if (NoneBookToRead())
        {
            return;
        }

        int firstBook = 0;
        foreach (var bookElement in _libraryBook.LibraryIncludingBooks)
        {
            GBBookViewModel bookViewModel = new GBBookViewModel(bookElement);
            
            _welcomeView.displayAllBooksView(bookViewModel);
            firstBook++;
            if (firstBook == 1)
            {
                _welcomeView.onBookViewSelected(bookViewModel);
            }
        }
        
    }

    private bool NoneBookToRead()
    {
        if (_libraryBook.NoBooksInTheLibrary())
        {
            _welcomeView.displayAlert("Aucun livre dans la librarie, vous ne pouvez lire !");
            return true;
        }

        return false;
    }

    
    
    /// <summary>
    /// Cette méthode permet de filtrer la liste contenant tous les livres
    /// en fonction de la chaine de caractères entrée par l'utilisateur.
    /// </summary>
    /// <param name="wordEnteredByUser">Chaine de caractères entrée par l'utilisateur</param>
    public void FilterBooks(string wordEnteredByUser)
    {
        int count = 0;
        foreach (var bookElement in _libraryBook.LibraryIncludingBooks)
        {
            GBBookViewModel bookViewModel = new GBBookViewModel(bookElement);
            if(bookViewModel.getTitle()!.ToLower().Contains(wordEnteredByUser.ToLower().Trim()) || bookViewModel.getIsbn()!.Equals(wordEnteredByUser))
            {
                _welcomeView.displayAllBooksView(bookViewModel);
                count++;
            }
            
        }
        if (count == 0)
        {
            String warningMsg = "Aucun résultat ne correspond à votre recherche !";
            _welcomeView.displayWarning(warningMsg);
        }
    }

    /// <summary>
    /// Cette méthode permet d'affecter le MainPresenter
    /// au WelcomePresenter
    /// </summary>
    /// <param name="mainPresenter">le présentateur principal</param>
    public void setMainPresenter(MainPresenter? mainPresenter)
    {
        _mainPresenter = mainPresenter;
    }

    /// <summary>
    /// Cette méthode permet d'informer l'utilisateur dès son arrivée sur l'application qu'un
    /// problème est survenu dans le fichier comprenant la sauvegarde de ses session lecture.
    /// </summary>
    public void ErrorJson()
    {
        string errorMsg = "Vos sessions de lecture ont été réinitialisés, une erreur dans le fichier json a été rencontrée";
        _welcomeView.DisplayError(errorMsg);
    }
}