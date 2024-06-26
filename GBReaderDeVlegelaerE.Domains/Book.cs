namespace GBReaderDeVlegelaerE.Domains;

public class Book
{
    /* Déclarations des attributs */
    private readonly Author? _author;
    private readonly string? _title;
    private readonly Isbn? _isbn;
    private readonly string? _summary;
    private IList<Page>? _pagesOfTheBook;

    /// <summary>
    /// Constructeur vide
    /// </summary>
    public Book()
    {
        
    }

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="author">auteur du livre</param>
    /// <param name="title">le titre du livre</param>
    /// <param name="isbnForBook">l'isbn du livre</param>
    /// <param name="summary">le résumé d'un livre</param>
    public Book(Author? author, string? title, Isbn? isbnForBook, string? summary)
    {
        this._author = author;
        this._title = title;
        this._isbn = isbnForBook;
        this._summary = summary;
        this._pagesOfTheBook = new List<Page>();
    }

    /// <summary>
    /// Cette méthode permet de récupérer l'auteur d'un livre
    /// </summary>
    public Author? Author => _author;

    /// <summary>
    /// Cette méthode permet de récupérer le titre d'un livre
    /// </summary>
    public string? Title => _title;

    /// <summary>
    /// Cette méthode permet de récupérer l'objet l'isbn d'un livre
    /// </summary>
    public Isbn? Isbn => _isbn;

    /// <summary>
    /// Cette méthode permet de récupérer le résumé
    /// </summary>
    public string? Summary => _summary;

    public IList<Page>? PagesOfTheBook
    {
        get => new List<Page>(_pagesOfTheBook!);
        set => _pagesOfTheBook = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Cette méthode permet de vérifier la longueur d'un titre d'un livre
    /// </summary>
    /// <param name="titre">une chaine de caractères (titre)</param>
    /// <returns>true si le titre du livre comporte entre 1 et 150 caractères</returns>
    public bool CorrectLengthOfTitle(string? titre) => titre!.Length is > 0 and <= 150;
    
    /// <summary>
    /// Cette méthode permet de vérifier la longueur d'un résumé d'un livre
    /// </summary>
    /// <param name="summary">une chaine de caractères (résumé)</param>
    /// <returns>true si le résumé du livre comporte entre 0 et 500 caractères</returns>
    public bool CorrectLengthOfSummary(string? summary) => summary!.Length is > 0 and <= 500;
}