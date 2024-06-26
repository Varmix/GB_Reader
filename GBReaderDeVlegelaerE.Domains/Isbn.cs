using System.Text.RegularExpressions;

namespace GBReaderDeVlegelaerE.Domains
{
    public class Isbn
    {
        private string? _isbnOfTheBook;

        /// <summary>
        /// Constructeur vide
        /// </summary>
        public Isbn()
        {
        }

        /// <summary>
        /// Surchage de constructeur
        /// </summary>
        /// <param name="isbn">une chaine de caractères contenant l'isbn</param>
        public Isbn(string? isbn)
        {
            this._isbnOfTheBook = isbn;
        }

        /// <summary>
        /// Propriété en lecture
        /// </summary>
        public string? IsbnOfTheBook => _isbnOfTheBook;

        /// <summary>
        /// Cette méthode permet de vérifier si l'isbn
        /// d'un livre est de la bonne taille
        /// </summary>
        /// <param name="isbn">L'isbn d'un livre</param>
        /// <returns>true si la longueur de la chaine de caractères relative à l'isbn est de 13,sinon false</returns>
        private bool LengthIsbnIsCorrect(string? isbn)
        {
            return isbn!.Length == 13;
        }

        /// <summary>
        /// Cette méthode permet de vérifier si l'isbn récupéré
        /// du fichier json n'a pas été corrompu.  
        /// </summary>
        /// <param name="isbnUser">l'isbn récupéré dans le fichier json</param>
        /// <returns>true si le code de vérification que l'on calcule est équivalent
        /// à celui récupéré du fichier json. Sinon, false</returns>
        public bool CheckISBN(string? isbnUser)
        {
            string? isbn = isbnUser;
            //Si la longueur n'est pas bonne, on sort de la méthode.
            if (!LengthIsbnIsCorrect(isbn) || !FormatIsbnCorrect(isbn))
            {
                return false;
            }

            isbn = isbnUser!.Substring(0, 1) + isbnUser.Substring(2, 6) + isbnUser.Substring(9, 2) +
                   isbnUser.Substring(12);
            //On vient calculer la somme de le l'isbn en prennant les 9 premiers chiffres de l'isbn
            int sum = sumIsbn(isbn);
            //Calcul de la reste de la somme précédente par 11
            int restSum = sum % 11;
            //Calcul du code controle
            int controlCode = 11 - restSum;
            //Vérification si le code de vérification entrée par l'utilsiateur est égale au code fourni par la méthode
            string copyIsbn = particularVerificationCode(isbn, controlCode);
            if (!checkVerificationCode(isbn, copyIsbn))
            {
                return false;
            }

            _isbnOfTheBook = copyIsbn.Substring(0, 1) + "-" + copyIsbn.Substring(1, 6) + "-" +
                             copyIsbn.Substring(7, 2) + "-" + copyIsbn.Substring(9);
            return true;
        }

        /// <summary>
        /// Cette méthode permet de vérifier si le code de vérification calculée est équivalent
        /// au code de vérification de l'isbn récupéré.
        /// </summary>
        /// <param name="isbn">isbn récupéré du fichier</param>
        /// <param name="copyIsbn">isbn calculé</param>
        /// <returns>true si les codes de vérfication sont identiques, sinon false</returns>
        /// 
        private bool checkVerificationCode(string? isbn, string copyIsbn)
        {
            return copyIsbn[9] == isbn![9];
        }

        /// <summary>
        /// Cette méthode permet d'assigner le code de contrôle calculé
        /// à une copie de l'isbn récupéré sans le fichier json. Bien évidemment,
        /// ce dernier a été tronqué pour ne conserver que les 9 premiers chiffres
        /// (sans le code de vérification)
        /// </summary>
        /// <param name="isbn">isbn récupéré du fichier json</param>
        /// <param name="controlCode">code de contrôle calculé</param>
        /// <returns>retourne l'isbn avec le code de vérification calculé.
        /// Si ce dernier est 10, on le remplace par un "X". S'il vaut 11, on le rempalce par un "0".
        /// Dans les autres cas, on assignera le code de vérification calculé.</returns>
        private string particularVerificationCode(string? isbn, int controlCode)
        {
            string copyIsbn = "";
            //Cas particulier
            if (controlCode == 10)
            {
                copyIsbn = isbn!.Substring(0, 9) + "X";
            }
            else if (controlCode == 11)
            {
                copyIsbn = isbn!.Substring(0, 9) + "0";
            }
            else
            {
                copyIsbn = isbn!.Substring(0, 9) + controlCode.ToString();
            }

            return copyIsbn;
        }

        /// <summary>
        /// Cette méthode permet d'effectuer la multiplication
        /// des 9 premiers chiffres de l'isbn par un poids allant de 10 à 2 afin
        /// de trouver le code de vérification.
        /// </summary>
        /// <param name="isbn">L'isbn sans le code de vérification</param>
        /// <returns>La somme de la multiplication des 9 premiers chiffres de l'isbn par un poinds allant
        /// de 10 à 2.</returns>
        private int sumIsbn(string? isbn)
        {
            int isbnNumber, multiplicatorNumber, resultat;
            int sum = 0;
            //Je ne prends pas le dernier caractère pour faire la vérification
            for (int i = 0; i < isbn!.Length - 1; i++)
            {
                isbnNumber = int.Parse(isbn.Substring(i, 1));
                multiplicatorNumber = 10 - i;
                resultat = multiplicatorNumber * isbnNumber;
                sum += resultat;
            }

            return sum;
        }

        /// <summary>
        /// Vérifie si le format d'un isbn est correct
        /// </summary>
        /// <param name="isbn">un isbn fourni par l'utilisateur</param>
        /// <returns>true si le format est correct, sinon false</returns>
        private bool FormatIsbnCorrect(string? isbn)
        {
            return Regex.IsMatch(isbn!, @"\d-\d{6}-\d{2}-[\d|X]{1}");
        }
    }
}