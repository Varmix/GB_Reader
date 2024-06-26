using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace GBReaderDeVlegelaerE.Domains
{
    public class Page
    {
        private int _numPage;

        private String _textePage;

        private IList<Choice> _allOfChoices;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="numeroPage">numéro de la page</param>
        /// <param name="textePage">texte de la page</param>
        public Page(int numeroPage, String textePage)
        {
            this._numPage = numeroPage;
            this._textePage = textePage;
            this._allOfChoices = new List<Choice>();
        }
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="numeroPage">numéro de la page</param>
        /// <param name="textePage">texte de la page</param>
        /// <param name="allOfChoicesOfAPage">les choix relatifs à la page</param>
        public Page(int numeroPage, String textePage, IList<Choice> allOfChoicesOfAPage)
        {
            this._numPage = numeroPage;
            this._textePage = textePage;
            this._allOfChoices = new List<Choice>(allOfChoicesOfAPage);
        }

        /// <summary>
        /// Cette méthode permet de récupérer le texte de la page
        /// </summary>
        public string TextePage => _textePage;

        /// <summary>
        /// Vérifie si une page est terminale
        /// </summary>
        /// <returns>true si la page est terminale, sinon false</returns>
        public bool ThisIsATerminalePage() => AllOfChoices.Count == 0;

        /// <summary>
        /// Cette méthode permet de récupérer les choix d'une page ou de les mettre à jour
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public IList<Choice> AllOfChoices
        {
            get => new List<Choice>(_allOfChoices);
            set => _allOfChoices = value ?? throw new ArgumentNullException(nameof(value));
        }
        
        /// <summary>
        /// Cette méthode permet de récupérer le numéro de la page
        /// </summary>
        public int NumPage => _numPage;
    }
}