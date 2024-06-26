namespace GBReaderDeVlegelaerE.Domains
{
    public class UserSession
    {
        private int _lastPage;
        private DateTime _startReading;
        private DateTime _lastUpdate;

        /// <summary>
        /// Constructeur
        /// </summary>
        public UserSession()
        {
            
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="lastPage">dernière page lue</param>
        /// <param name="startReading">la date et l'heure de début de lecture du livre</param>
        /// <param name="lastUpdate">le dernier choix sur lequel l'utilisateur a appuyé</param>
        public UserSession(int lastPage, DateTime startReading, DateTime lastUpdate)
        {
            this._lastPage = lastPage;
            this._startReading = startReading;
            this._lastUpdate = lastUpdate;
        }
        /// <summary>
        /// Cette méthode permet de récupérer la dernière page ou la mettre à jour
        /// </summary>
        public int LastPage
        {
            get => _lastPage;
            set => _lastPage = value;
        }
        
        /// <summary>
        /// Cette méthode permet de récupérer l'heure et la date de début de lecture du livre
        /// </summary>
        public DateTime StartReading
        {
            get => _startReading;
        }
        
        /// <summary>
        /// Cette méthode permet de récupérer ou de mettre à jour le dernier choix sur lequel l'utilisateur a appuyé
        /// </summary>
        public DateTime LastUpdate
        {
            get => _lastUpdate;
            set => _lastUpdate = value;
        }
    }
}