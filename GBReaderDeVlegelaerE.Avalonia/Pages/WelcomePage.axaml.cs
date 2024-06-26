using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using GBReaderDeVlegelaerE.Presenters;

namespace GBReaderDeVlegelaerE.Avalonia.Pages
{
    public partial class WelcomePage : UserControl, IWelcomeView
    {
        public WelcomePage()
        {
            InitializeComponent();
        }
        /**
         * Réagit lorsqu'on appuye sur le bouton "Rechercher"
         */
        private void Search_OnClick(object? sender, RoutedEventArgs args)
        {
              //Récupérer la valeur encodée
              WarningDisplay.Text = "";
              string wordEnteredByUser = SearchWord.Text == null ? "" : SearchWord.Text.Length == 0 ? "" : SearchWord.Text;
              AllBooksPanel.Children.Clear();
              FilterBook?.Invoke(sender, wordEnteredByUser);
        }

        /// <summary>
        /// Cette méthode permet de créer un User Control d'un livre et
        /// de l'ajouter dans un panneau de la fenêtre principale parmi
        /// la liste des livres.
        /// Par la même occassion, chaque contrôle utilisateur est abonné à un
        /// écouteur.
        /// </summary>
        /// <param name="gameBookViewModel">Un livre avec des informations
        /// accessibles uniquement en lecture</param>
        public void displayAllBooksView(GBBookViewModel gameBookViewModel)
        {
            BooksTitle.IsVisible = true;
            BorderBookList.IsVisible = true;
            var bookElement = new Controls.BookElementView();
            //Abonnement au bouton d'un BookElementView, on est côté observateur.
            bookElement.BookOnClick += OnClickBasicBook;
            bookElement.SetGBBookViewModel(gameBookViewModel);
            AllBooksPanel.Children.Add(bookElement);
        }

        /// <summary>
        /// Lorsque le bouton "Visualiser" est appuyé dans un UserControl
        /// BookElementView, elle déclenche cette méthode vu l'abonnement.
        /// Elle va permettre de réagir à l'événement survenu, en appelant
        /// onBookViewSelected
        /// </summary>
        /// <param name="sender">Le BookElementView</param>
        /// <param name="gameBookViewModel">Le livre en modèle vue</param>
        private void OnClickBasicBook(object? sender, GBBookViewModel gameBookViewModel)
        {
            onBookViewSelected(gameBookViewModel);
        }
        

        /// <summary>
        /// Cette méthode permet d'afficher un message
        /// d'erreur à l'utilisateur (dans le cas où le fichier
        /// n'existe pas ou la liste des livres contenue dans le fichier
        /// est vide).
        /// </summary>
        /// <param name="errorMsg">une chaine de caractères contenant le message d'erreur</param>
        public void displayAlert(string errorMsg)
        {
            HeaderBottomMainTitle.IsVisible = false;
            AlertDisplay.IsVisible = true;
            QuitApplication.IsVisible = true;
            AlertDisplay.Text = $"{errorMsg}";

         }

        /// <summary>
        /// Cette méthode permet d'afficher un message d'alerte
        /// à l'utilisateur. Par exemple, si la recherche d'un livre
        /// ne correspond à aucun résultat, il en est informé.
        /// </summary>
        /// <param name="warningMsg">une chaine de caractères contenant le message
        /// d'alerte</param>
        public void displayWarning(string warningMsg)
        {
            BookDetailContainer.IsVisible = false;
            WarningDisplay.IsVisible = true;
            BorderBookList.IsVisible = false;
            WarningDisplay.Text = $"{warningMsg}";
        }
        
        /// <summary>
        /// Cette méthode permet d'afficher un message lorsqu'une
        /// erreur est survenue dans le fichier json
        /// </summary>
        /// <param name="errorMsg"></param>
        public void DisplayError(string errorMsg)
        {
            ErrorMsg.Text = $"{errorMsg}";
        }
        


        /// <summary>
        /// Cette méthode permet de créer un User Control d'un livre détaillé
        /// lorsqu'un utilisateur sélectionne un livre à visualier parmi la liste
        /// des livres. Ce contrôle utilisateur s'affiche dans un panneau
        /// à droite de la liste de tous les livres
        /// </summary>
        /// <param name="book">Un livre avec des informations accessibles
        /// uniquement en lecture</param>
        public void onBookViewSelected(GBBookViewModel book)
        {
            BookDetailContainer.IsVisible = true;
            BookDetailPanel.Children.Clear();
            //Créer une vue avec les détails du livre sur la droite où je vais lui envoyer un book comme dans le mainPresebter
            var bookDetail = new Controls.BookDetailsView();
            //On s'abonne à l'événement dans la partie gauche de l'opérateur +=, à droite, c'est la méthode en réaction lorsque l'événement survient
            bookDetail.DetailsBookOnClick += OnClickDetailsBook;
            bookDetail.SetGBBookViewModel(book);
            BookDetailPanel.Children.Add(bookDetail);
        }

        private void OnClickDetailsBook(object? sender, GBBookViewModel eGbBookViewModel)
        {
            //On renvoie un événement vers notre présenter avec les informations de l'événement du bouton d'un livre détaillé
            OnDetailsBookClick?.Invoke(sender, eGbBookViewModel);
        }

        /// <summary>
        /// Cette méthode permet de quitter l'application si le fichier contenant
        /// les livres n'existe pas ou que la liste des livres contenue dans le fichier
        /// est vide.
        /// </summary>
        /// <param name="sender">Le bouton</param>
        /// <param name="e">Contient les données d'événements et des informations
        /// associées à un événement routé</param>
        private void QuitApplication_OnClick(object? sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        

        public event EventHandler<string> FilterBook;

        public event EventHandler<GBBookViewModel> OnDetailsBookClick;
        
        public event EventHandler<string> SeeStatisticsPage;
        

        private void ViewStatistics_OnClick(object? sender, RoutedEventArgs e)
        {
            SeeStatisticsPage?.Invoke(sender, "StatsPage");
        }
        
    }
}