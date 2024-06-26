namespace GBReaderDeVlegelaerE.Domains
{
    public class LibraryBook
    {
        
        private IDictionary<string?, Book> libraryIncludingBooks = new Dictionary<string, Book>()!;

        /// <summary>
        /// Constructeur vide
        /// </summary>
        public LibraryBook()
        {
            
        }

        /// <summary>
        /// Cette méthode permet d'ajouter des livres dans la library
        /// </summary>
        /// <param name="book">Un livre</param>
        public void AddBookInTheLibrary(string? isbn, Book book)
        {
            libraryIncludingBooks.Add(isbn, book);
        }
        
        public IList<Book> LibraryIncludingBooks => new List<Book>(libraryIncludingBooks.Values.ToList());

        /// <summary>
        /// Permet de remplacer un livre chargée de nouvelles
        /// informations dans la librarier
        /// </summary>
        /// <param name="currentBook">Le livre actuelle comportant les modifications</param>
        public void ReplaceABook(Book currentBook)
        {
            if (libraryIncludingBooks.ContainsKey(currentBook.Isbn!.IsbnOfTheBook))
            {
                libraryIncludingBooks[currentBook.Isbn.IsbnOfTheBook] = currentBook;
            }
        }
        
        /// <summary>
        /// Vérifier si un livre a déjà été chargé en base de données
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public bool VerifyIsABookIsAlreadyLoaded(string? isbn) => libraryIncludingBooks.ContainsKey(isbn) &&
                                                                  (libraryIncludingBooks[isbn].PagesOfTheBook!.Count > 0);


        /// <summary>
        /// Récupérer un livre sur son isbn
        /// </summary>
        /// <param name="isbn">isbn d'un livre</param>
        /// <returns>le livre associé à l'isbn</returns>
        public Book GetASpecificBookOnIsbn(string? isbn) => libraryIncludingBooks[isbn];

        /// <summary>
        /// Cette méthode permet de savoir si la librairie contient des livres ou non
        /// </summary>
        /// <returns>true si la librairie ne contient aucun livre, sinon false</returns>
        public bool NoBooksInTheLibrary() => libraryIncludingBooks.Count == 0;
    }
}