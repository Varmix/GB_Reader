using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using GBReaderDeVlegelaerE.Presenters;

namespace GBReaderDeVlegelaerE.Avalonia.Controls;

public partial class BookElementView : UserControl
{
    /* Déclaration des attributs */
   // private MainPresenter _mainPresenter;
   private GBBookViewModel _book;

    public BookElementView()
    {
        InitializeComponent();
    }
    
    /// <summary>
    /// Cette méthode permet d'afficher les différents informations
    /// d'un livre telles que le titre, l'isbn, le nom
    /// et prénom de l'auteur parmi la liste des livres.
    /// </summary>
    /// <param name="book"></param>
    public void SetGBBookViewModel(GBBookViewModel book)
    {
        _book = book;
        TitleBook.Text =  $"{_book.getTitle()}";
        IsbnBook.Text = $"{_book.getIsbn()}";
        AuthorBook.Text = $"{_book.getFirstNameAuthor() + " " + book.getLastNameAuthor()}";
    }
    /// <summary>
    /// Cette méthode permet lorsqu'on appuie sur le bouton
    /// "Visualer" sur un livre parmi la liste des livres de déclencher
    /// la méthode onBookViewSelected dans notre fenêtre principale.
    /// </summary>
    /// <param name="sender">notre objet BookElementView</param>
    /// <param name="e">Contient les données d'événements et des informations
    /// associées à un événement routé</param>
    private void ShowBook_OnClick(object? sender, RoutedEventArgs e)
    {
        BookOnClick?.Invoke(this, _book);
    }

    public event EventHandler<GBBookViewModel> BookOnClick;
}