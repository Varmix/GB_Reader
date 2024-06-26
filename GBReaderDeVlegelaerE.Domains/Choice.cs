namespace GBReaderDeVlegelaerE.Domains
{
    public class Choice
    {
        private int _numPageTo;

        private string _contentChoice;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="contentChoice">Le contenu du choix</param>
        public Choice(string contentChoice)
        {
            this._contentChoice = contentChoice;
        }
        
        /// <summary>
        /// Cette méthode permet de récupérer le page
        /// vers où se dirige le choix
        /// </summary>
        public int NumPageTo
        {
            get => _numPageTo;
            set => _numPageTo = value;
        }

        /// <summary>
        /// Cette méthode permet de récupérer le contenu du choix
        /// </summary>
        public string ContentChoice
        {
            get => _contentChoice;
        }
    }
}