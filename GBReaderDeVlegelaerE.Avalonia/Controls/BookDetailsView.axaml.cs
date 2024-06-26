using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using GBReaderDeVlegelaerE.Presenters;

namespace GBReaderDeVlegelaerE.Avalonia.Controls;

public partial class BookDetailsView : UserControl
{
    /* Déclarations des attributs */
    private GBBookViewModel _book;
    
    /* Constructeur de la classe BookDetailsView permettant d'intialiser
     les composants*/
    public BookDetailsView()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Cette méthode permet d'afficher les différents informations
    /// d'un livre sélectionné telles que le titre, l'isbn, le résumé, le nom
    /// et prénom de l'auteur.
    /// </summary>
    /// <param name="book">Un livre avec des informations accessibles uniquement
    /// en lecture</param>
    public void SetGBBookViewModel(GBBookViewModel book)
    {
        _book = book;
        TitleBookDetail.Text = $"{_book.getTitle()}";
        IsbnBookDetail.Text = $"{_book.getIsbn()}";
        SummaryBookDetail.Text = $"{_book.getSummary()}";
        AuthorBookDetail.Text = $"{_book.getFirstNameAuthor() + " " + _book.getLastNameAuthor()}";

    }

    private void ContinueToRead_OnClick(object? sender, RoutedEventArgs e)
    {
        DetailsBookOnClick?.Invoke(this, _book);
    }

    public event EventHandler<GBBookViewModel> DetailsBookOnClick;
}