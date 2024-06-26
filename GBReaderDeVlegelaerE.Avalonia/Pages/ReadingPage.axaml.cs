using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GBReaderDeVlegelaerE.Avalonia.Controls;
using GBReaderDeVlegelaerE.Presenters;

namespace GBReaderDeVlegelaerE.Avalonia.Pages
{
    public partial class ReadingPage : UserControl, IReadingPage
    {

        private PageViewModel _currentPageViewModel;
        
        public ReadingPage()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Cette méthode permet d'afficher la page avec ses choix que le lecteur souhaite lire.
        /// </summary>
        /// <param name="pageViewModel">Un vue modèle d'une page</param>
        public void DisplayPageAndTheChoices(PageViewModel pageViewModel)
        {
            //Chaque appel, je m'assure d'enlever les choix précédents
            ChoicesContainer.Children.Clear();
            //Retenir la page courante
            _currentPageViewModel = pageViewModel;
            //Affichage pour la page
            NumPageFrom.Text = $"{pageViewModel.GetNumPage()}";
            ContentOfThePage.Text = $"{pageViewModel.GetTextePage()}";
            //S'il n'y a plus de choix, nous sommes sur une page terminale
            if (pageViewModel.GetListOfChoicesViewModel().Count == 0)
            {
                //Afficher les boutons Revenir sur la page d'accueil ou recommencer la lecture
                RestartReading.IsVisible = true;
            }
            //S'il y a des choix, je les affiche.
            else
            {
                //Lorsque le lecteur appuie sur redémarrer la lecture
                RestartReading.IsVisible = false;
                //
                //ComeBackToWelcomePage.IsVisible = false;
                //Affichage des choix relatifs à la page
                foreach (var choiceViewModel in pageViewModel.GetListOfChoicesViewModel())
                {
                    ChoiceView choiceViewUserControl = new ChoiceView();
                    choiceViewUserControl.SetChoiceViewModel(choiceViewModel);
                    //Si l'utilisateur clique sur choix, la méthode de droite est la réaction.
                    choiceViewUserControl.OnChoiceClicked += AnalyzeDirectionPage;
                    ChoicesContainer.Children.Add(choiceViewUserControl);
                }
            }
        }

        private void AnalyzeDirectionPage(object? sender, ChoiceViewModel e)
        {
            //Transmettre au presenter le choix cliqué afin qu'il fasse le traitement et renvoie la page à afficher
            OnSwitchPageRequested?.Invoke(sender, e);
            
        }
        

        private void ComeBackToWelcomePage_OnClick(object? sender, RoutedEventArgs e)
        {
            ComeBackToWelcomePageRequested?.Invoke(sender, e);
        }

        private void RestartReading_OnClick(object? sender, RoutedEventArgs e)
        {
            RestartReadingRequested?.Invoke(sender, e);
        }
        
        public event EventHandler<ChoiceViewModel> OnSwitchPageRequested;

        public event EventHandler ComeBackToWelcomePageRequested;

        public event EventHandler RestartReadingRequested;

        /// <summary>
        /// Cette méthode permet d'enlever le message d'erreur lorsque livre complet a été correctement chargé.
        /// </summary>
        public void ClearErrorMessage()
        {
            ErrorMessage.Text = "";
            ErrorMessage.IsVisible = false;
        }

        /// <summary>
        /// Cette méthode permet d'afficher un message d'erreur, si le chargement d'un livre complet n'a pu
        /// se réaliser.
        /// </summary>
        /// <param name="exMessage">le message d'erreur</param>
        public void DisplayErrorMessage(string exMessage)
        {
            ErrorMessage.IsVisible = true;
            ErrorMessage.Text = exMessage;
        }
    }
}