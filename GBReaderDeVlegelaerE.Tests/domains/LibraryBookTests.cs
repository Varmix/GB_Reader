using System.Collections;
using GBReaderDeVlegelaerE.Domains;

namespace GBReaderDeVlegelaerE.Tests;

public class LibraryBookTests
{

    private LibraryBook _libraryBook;

    [SetUp]
    public void Setup()
    {
        _libraryBook = new();
        _libraryBook.AddBookInTheLibrary("2-070039-01-3", new Book(new Author("Emile", "Zola", "210054"), "L'assomoir", new Isbn("2-070039-01-3"), "Livre de Zola"));
    }
    

    [Test]
    public void VerifyOneBookIsInTheLibrary()
    {
        //Given
        int numberExepcted = 1;
        //When //Then
        Assert.That(numberExepcted, Is.EqualTo(_libraryBook.LibraryIncludingBooks.Count));
    }
    
    [Test]
    public void VerifyIfABookIsAlreadyLoaded()
    {
        //Given
        string? isbnForTheBookExepcted = "2-070039-01-3";
        //When //Then
        Assert.False(_libraryBook.VerifyIsABookIsAlreadyLoaded(isbnForTheBookExepcted));
    }
    
    [Test]
    public void GetASpecificBook()
    {
        //Given
        string? isbnExpected = "2-070039-01-3";
        //When //Then
        Assert.NotNull(_libraryBook.GetASpecificBookOnIsbn(isbnExpected));
    }
    
    [Test]
    public void NoBookInTheListIsFalse()
    {
        //Given//When //Then
        Assert.False(_libraryBook.NoBooksInTheLibrary());
    }
    
    [Test]
    public void ReplaceBookInTheLibrary()
    {
        //Given
        Book book = new Book(new Author("Emile", "Zola", "210054"), "L'assomoir", new Isbn("2-070039-01-3"),
            "Livre de Zola");
        IList<Page>? pagesOfTheBook = new List<Page>();
        IList<Choice> choicesOfTheFirstPage = new List<Choice>();
        Choice choice = new Choice("Allez en page 2");
        choice.NumPageTo = 2;
        choicesOfTheFirstPage.Add(choice);
        Page firstPage = new Page(1, "Vous commencez l'histoire", choicesOfTheFirstPage);
        pagesOfTheBook.Add(firstPage);
        book.PagesOfTheBook = pagesOfTheBook;
        int numberOfPageExpected = 1;
        //When
        _libraryBook.ReplaceABook(book);
        //Then
        Assert.IsNotNull(_libraryBook.GetASpecificBookOnIsbn("2-070039-01-3"));
        Assert.That(numberOfPageExpected, Is.EqualTo(_libraryBook.GetASpecificBookOnIsbn("2-070039-01-3").PagesOfTheBook.Count));
    }







}