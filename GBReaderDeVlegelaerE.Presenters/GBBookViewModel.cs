using GBReaderDeVlegelaerE.Domains;

namespace GBReaderDeVlegelaerE.Presenters;

/// <summary>
/// Cette classe va permette d'avoir
/// accès aux différentes informations d'un livre
/// uniquement en lecture depuis les vues.
/// </summary>
public class GBBookViewModel
{
    /* Déclaration des attributs */
    private Book _gameBook;

    /// <summary>
    /// Constructeur de la classe GBBookViewModel prenant
    /// en paramètre un livre.
    /// </summary>
    /// <param name="gameBook"></param>
    public GBBookViewModel(Book gameBook)
    {
        _gameBook = gameBook;
    }
    
    /// <summary>
    /// Cette méthode permet de récupérer le prénom d'un auteur
    /// </summary>
    /// <returns>Le prénom de l'auteur sous forme de chaine de caractères</returns>
    public string getFirstNameAuthor()
    {
        return _gameBook.Author!.FirstName;
    }

    /// <summary>
    /// Cette méthode permet de récupérer le nom de l'auteur
    /// </summary>
    /// <returns>Le nom de l'auteur sous forme de chaine de caractères</returns>
    public string getLastNameAuthor()
    {
        return _gameBook.Author!.LastName;
    }

    /// <summary>
    /// Cette méthode permet de récupérer l'isbn d'un livre
    /// </summary>
    /// <returns>L'isbn d'un livre sous forme de chaine de caractères</returns>
    public string? getIsbn()
    {
        return _gameBook.Isbn!.IsbnOfTheBook;
    }
    /// <summary>
    /// Cette méthode permet de récupérer le titre d'un livre
    /// </summary>
    /// <returns>Le titre d'un livre sous forme de chaine de caractères</returns>
    public string? getTitle()
    {
        return _gameBook.Title;
    }

    /// <summary>
    /// Cette méthode permet de récupérer le résumé d'un livre
    /// </summary>
    /// <returns>Le résumé d'un livre sous forme de chaine de caractères</returns>
    public string? getSummary()
    {
        return _gameBook.Summary;
    }

    public IList<Page>? GetListOfPages()
    {
        return _gameBook.PagesOfTheBook;
    }

    public IList<PageViewModel> GetListOfPagesVm()
    {
        IList<PageViewModel> allPagesVm = new List<PageViewModel>();
        IList<Page>? allPages = _gameBook.PagesOfTheBook;
        foreach (Page page in allPages!)
        {
            allPagesVm.Add(new PageViewModel(page));
        }

        return allPagesVm;
    }

    public Author? getAuthor()
    {
        return _gameBook.Author;
    }

    public Isbn? getIsbnForBook()
    {
        return _gameBook.Isbn;
    }
}