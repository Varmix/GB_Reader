namespace GBReaderDeVlegelaerE.Domains
{
    public class ReadingStatistics
    {
        private IDictionary<string?, UserSession> _userReadings;

        /// <summary>
        /// Constructeur vide permettant d'intialiser le dictionnaire
        /// comprenant le sessions de lecture en cas de problème dans le
        /// ficheir json
        /// </summary>
        public ReadingStatistics()
        {
            _userReadings =  new Dictionary<string, UserSession>()!;
        }
        
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="allReadingSessions">un dictionnaire de sessions de lecture</param>
        public ReadingStatistics(IDictionary<string?, UserSession> allReadingSessions)
        {
            this._userReadings = allReadingSessions;
        }

        /// <summary>
        /// Cette fonction permet de mettre à jour une session de lecture pour un livre.
        /// Si on arrive à une page terminale, la session est supprimée.
        /// </summary>
        /// <param name="isbnIsbnOfTheBook">l'isbn du livre en cours de lecture</param>
        /// <param name="lastPage">le numéro de la dernière page</param>
        /// <param name="deletePage">booléen qui déterminer la suppression ou non de la session de lecture en cours</param>
        public void UpdateTheLastPageReading(string? isbnIsbnOfTheBook, int lastPage, bool deletePage)
        {
            if (deletePage)
            {
                //Je supprimme la session de lecture correspondante au livre
                _userReadings.Remove(isbnIsbnOfTheBook);
                return;
            }
            //Mise à jour de la dernière page sur base de l'isbn
            _userReadings[isbnIsbnOfTheBook].LastPage = lastPage;
            //Mise à jour de la session de lecture
            _userReadings[isbnIsbnOfTheBook].LastUpdate = DateTime.Now;
        }
        
        /// <summary>
        /// Cette méthode permet de chercher si une session de lecture est existante ou non pour un livre.
        /// Par la même occassion, elle gère un cas dégradé, à savoir un livre publié avec une seule page.
        /// Dans cette situation, lorsque le leccteur est arrivé à la fin de sa lecture, finalement juste en cliquant
        /// sur le livre. La méthode se charge de supprimer la session de lecture, de cette manière s'il clique à nouveau
        /// sur le livre afin de le lire, une nouvelle session démarre.
        /// </summary>
        /// <param name="isbnIsbnOfTheBook"></param>
        /// <param name="pageZeroIfNoSession"></param>
        /// <param name="deletePage"></param>
        /// <returns></returns>
        public int SearchAReadingSession(string? isbnIsbnOfTheBook, int pageZeroIfNoSession, bool deletePage)
        {
            if (deletePage)
            {
                //Je supprimme la session de lecture correspondante au livre
                _userReadings.Remove(isbnIsbnOfTheBook);
            }
            //Si le dictionnaire n'a pas de session lié au livre, il faut en créer une
            if (!_userReadings.ContainsKey(isbnIsbnOfTheBook))
            {
                //Lorsqu'on crée l'objet, la session de lecture démarre et la dernière mise à jour est la date et heure actuelle
               _userReadings[isbnIsbnOfTheBook] = new UserSession(pageZeroIfNoSession, DateTime.Now, DateTime.Now);
            }
            return _userReadings[isbnIsbnOfTheBook].LastPage;
        }
        
        /// <summary>
        /// Retourne une copie du dictionnaire comprenant les sessions de lectures
        /// </summary>
        public IDictionary<string?, UserSession> UserReadings => new Dictionary<string, UserSession>(_userReadings!)!;
    }
}