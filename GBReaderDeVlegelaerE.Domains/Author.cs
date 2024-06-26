using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace GBReaderDeVlegelaerE.Domains
{
    public class Author
    {
        private string _firstName;

        private string _lastName;

        private string _matricule;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="firstName">prénom de l'auteur</param>
        /// <param name="lastName">nom de l'auteur</param>
        /// <param name="matricule">matricule</param>
        public Author(string firstName, string lastName, string matricule)
        {
            this._firstName = firstName;
            this._lastName = lastName;
            this._matricule = matricule;
        }
        /// <summary>
        /// Cette méthode permet de récupérer le prénom de l'auteur
        /// </summary>
        public string FirstName => _firstName;
        /// <summary>
        /// Cette méthode permet de récupérer le nom de l'auteur
        /// </summary>
        public string LastName => _lastName;
        
        /// <summary>
        /// Cette méthode permet de vérifier si un nom ou un prénom
        /// fait plus de deux caractères
        /// </summary>
        /// <param name="name">un nom ou un prénom</param>
        /// <returns>true si le prénom fait au moins plus de deux caractères, sinon false</returns>
        public bool CoorectLengthOfFirstNameOrLastName(string name)
        {
            return name.Length >= 2;
        }

        /// <summary>
        /// Cett méthode permet de vérifier que le format du matricule
        /// est correct
        /// </summary>
        /// <param name="matricule">matricule en chaine de caractères</param>
        /// <returns>true si le matricule est correct, sinon false</returns>
        public bool CorrectFormatMatricule(string matricule)
        {
            return Regex.IsMatch(matricule, @"\d{6}");
        }
    }
}