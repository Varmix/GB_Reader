using System.Data;
using System.Data.SqlClient;
using System.Net;
using GBReaderDeVlegelaerE.Domains;
using GBReaderDeVlegelaerE.Repositories;

namespace GBReaderDeVlegelaerE.Infrastuctures.database
{
    public class BookStorage : IGameBookRepository
    {
        private readonly IDbConnection _con;

        private LibraryBook _libraryBook;

        private const string LOAD_COVER_BOOKS = @"SELECT B.titre as titreLivre, B.isbn as isbnLivre, B.resume as resumeLivre, B.matricule as matriculeAutheur,
              A.prenom as prenomAuteur, A.nom as nomAuteur
              FROM Book B
              Join Author A on B.matricule = A.matricule
              WHERE estPublie = 1;";

        private const string GET_IDBOOK_ON_ISBN =
            @"SELECT idBook as idBook FROM Book WHERE isbn = @isbn";

        private const string LOAD_PAGES_WWITHOUT_CHOICES =
            @"SELECT P.idPage as idPage, P.numPage as numeroPage, P.textPage as textePage
              FROM Page P
              WHERE idBook = @idBook";

        private const string LOAD_CHOICES_ON_ID_PAGE =
            @"SELECT c.texteAction as texteAction, c.numPageTo as numPageTo FROM Choice c WHERE c.numPageFrom = @idPageFrom";

        private const string GET_NUMPAGE_ON_PAGE_ID =
            "SELECT p.numPage as numPageTo FROM Page p WHERE p.idPage = @idPage";

        public BookStorage(IDbConnection con, LibraryBook libraryBook)
        {
            this._con = con;
            this._libraryBook = libraryBook;
        }
        
        /// <summary>
        /// Cette méthode permet de récupérer les informations basiques d'un livre en base de données
        /// </summary>
        /// <exception cref="GameBookStorageException">Peut lancer une exception personnalisée si on n'a pu les informations
        /// basqies d'un livre correctement</exception>
        public void LoadCovers()
        {
            
            try
            {
                using (IDbCommand loadCovers = _con.CreateCommand())
                {
                    loadCovers.CommandText = LOAD_COVER_BOOKS;
                    using (IDataReader reader = loadCovers.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Vérification pour l'auteur
                            string firstName = (string)reader["prenomAuteur"];
                            string lastName = (string)reader["nomAuteur"];
                            string matricule = (string)reader["matriculeAutheur"];
                            Author? auteur = new Author(firstName, lastName, matricule);
                            if (auteur.CoorectLengthOfFirstNameOrLastName(firstName) &&
                                auteur.CoorectLengthOfFirstNameOrLastName(lastName)
                                && auteur.CorrectFormatMatricule(matricule))
                            {
                                string? titre = (string)reader["titreLivre"];
                                string? isbn = (string)reader["isbnLivre"];
                                string? resume = (string)reader["resumeLivre"];
                                Isbn? isbnFromDb = new (isbn);
                                Book bookFromDb = new Book(auteur, titre, isbnFromDb, resume);
                                //Vérification des champs
                                if (isbnFromDb.CheckISBN(isbn) && bookFromDb.CorrectLengthOfTitle(titre) && bookFromDb.CorrectLengthOfSummary(resume))
                                {
                                    _libraryBook.AddBookInTheLibrary(isbn, bookFromDb);
                                } 
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw new GameBookStorageException("Erreur lors de la lecture des couvertures de livre");
            }
        }
        
        /// <summary>
        /// Cette méthode permet de charger les informations complètes d'un livre
        /// </summary>
        /// <param name="currentBook">le livre comprenant les informations basiques</param>
        /// <exception cref="GameBookStorageException">Une exception personnalisée peut être lancée
        /// si le chargement complet des livres n'a pu se réaliser</exception>
        public void LoadCompleteBookInformation(Book currentBook)
        {
            try
            {
                int idBook = GetIdBookOnIsbn(currentBook.Isbn!.IsbnOfTheBook);

                //Récupérer les pages sans les choix
                IDictionary<Page, int> pagesWithoutChoices = LoadPagesWithoutChoices(idBook, currentBook);

                foreach (var page in pagesWithoutChoices)
                {
                    IDictionary<Choice, int> choicesOfAPage = LoadChoicesOfAPage(page.Value);
                    //Convertir tous les idPages en NumPage avant de les affecter à la page
                    foreach (var choice in choicesOfAPage)
                    {
                        //Ici, j'ai l'id que je converti directement en numPage afin de l'affecter au choix
                        int numPageTo = GetNumPageOnPageId(choice.Value);
                        //Remplacement de l'idPage par le numPage
                        choice.Key.NumPageTo = numPageTo;
                    }

                    //Ici je converti ma map de choix, int en lsite de choix que je viens affecter à la page
                    page.Key.AllOfChoices = choicesOfAPage.Keys.ToList();
                }

                //Convertir la map en liste et la trier
                //IList<Page> pagesOfABook = pagesWithoutChoices.Keys.ToList().Sort((x, y) => x.CompareTo(y));
                IList<Page>? pagesOfABook = pagesWithoutChoices.Keys.OrderBy(p => p.NumPage).ToList();
                //Affecter la liste de page au livre
                currentBook.PagesOfTheBook = pagesOfABook;
                //Remplacer le livre dans la librarie afin de conserver des informations cohérentes
                _libraryBook.ReplaceABook(currentBook);

            }
            catch (SqlException)
            {
                throw new GameBookStorageException("Erreur lors de la lecture complète du livre");
            }
        }

        private int GetIdBookOnIsbn(string? isbnOfTheBook)
        {
            int idBook = -1;

            using (IDbCommand getIdBook = _con.CreateCommand())
            {
                //Requête SQL
                getIdBook.CommandText = GET_IDBOOK_ON_ISBN;

                //Définir le paramètre
                var nameParam = getIdBook.CreateParameter();
                nameParam.ParameterName = "@isbn";
                nameParam.DbType = DbType.String;
                nameParam.Value = isbnOfTheBook;
                getIdBook.Parameters.Add(nameParam);

                //Lire le résultat, dans notre cas, 1 seul id
                using (IDataReader reader = getIdBook.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        idBook = ((reader["idBook"] as int?)!).Value;
                    }
                }
            }


            return idBook;
        }

        //J'ai mis le currentBook en paramètre pour aller le remplacer dans la libraire
        private IDictionary<Page, int> LoadPagesWithoutChoices(int idBook, Book currentBook)
        {
            IDictionary<Page, int> pagesWithoutChoices = new Dictionary<Page, int>();
            
            using (IDbCommand loadPagesWithoutChoices = _con.CreateCommand())
            {
                //Requête SQL
                loadPagesWithoutChoices.CommandText = LOAD_PAGES_WWITHOUT_CHOICES;

                //Définir les paramètres
                var nameParam = loadPagesWithoutChoices.CreateParameter();
                nameParam.ParameterName = "@idBook";
                nameParam.DbType = DbType.Int32;
                nameParam.Value = idBook;
                loadPagesWithoutChoices.Parameters.Add(nameParam);
                using (IDataReader reader = loadPagesWithoutChoices.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int numPage = ((reader["numeroPage"] as int?)!).Value;
                        string textePage = (string)reader["textePage"];
                        int idPage = ((reader["idPage"] as int?)!).Value;
                        pagesWithoutChoices.Add(new Page(numPage, textePage), idPage);
                    }
                }
            }


            return pagesWithoutChoices;
        }

        private IDictionary<Choice, int> LoadChoicesOfAPage(int idPageFrom)
        {
            IDictionary<Choice, int> choicesOfAPage = new Dictionary<Choice, int>();

            using(IDbCommand loadChoicesOnIdPage = _con.CreateCommand())
            {
                //Requête SQL
                loadChoicesOnIdPage.CommandText = LOAD_CHOICES_ON_ID_PAGE;

                //Définir les paramètres
                var nameParam = loadChoicesOnIdPage.CreateParameter();
                nameParam.ParameterName = "@idPageFrom";
                nameParam.DbType = DbType.Int32;
                nameParam.Value = idPageFrom;
                loadChoicesOnIdPage.Parameters.Add(nameParam);

                using (IDataReader reader = loadChoicesOnIdPage.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string texteChoix = (string)reader["texteAction"];
                        int idNumPageTo = ((reader["numPageTo"] as int?)!).Value;
                        choicesOfAPage.Add(new Choice(texteChoix), idNumPageTo);
                    }
                }
            }

            return choicesOfAPage;
        }

        private int GetNumPageOnPageId(int choiceIdNumPageTo)
        {
            int numPageToInDb = -1;
            using (IDbCommand numPageTo = _con.CreateCommand())
            {
                //Requête sql
                numPageTo.CommandText = GET_NUMPAGE_ON_PAGE_ID;

                //Définir les paramètres
                var nameParam = numPageTo.CreateParameter();
                nameParam.ParameterName = "@idPage";
                nameParam.DbType = DbType.Int32;
                nameParam.Value = choiceIdNumPageTo;
                numPageTo.Parameters.Add(nameParam);
                using (IDataReader reader = numPageTo.ExecuteReader())
                {
                    //Un seul résultat
                    if (reader.Read())
                    {
                        numPageToInDb = ((reader["numPageTo"] as int?)!).Value;
                    }
                }
            }


            return numPageToInDb;
        }
        
        public void Dispose()
        {
            _con.Dispose();
        }
    }
}