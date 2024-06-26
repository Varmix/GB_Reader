using GBReaderDeVlegelaerE.Domains;

namespace GBReaderDeVlegelaerE.Tests;

public class BookTests
{

    private Book _book;

    [SetUp]
    public void Setup()
    {
        _book = new Book(new Author("Emile", "Zola", "210054"), "L'assomoir", new Isbn("2-070039-01-3"), "Livre de Zola");
    }
    

    [Test]
    public void GetTitleOfTheBook()
    {
        //Given
        string titleExpected = "L'assomoir";
        //When //Then
        Assert.That(titleExpected, Is.EqualTo(_book.Title));
    }
    
    [Test]
    public void GetSummaryOfTheBook()
    {
        //Given
        string lastNameExpected = "Livre de Zola";
        //When //Then
        Assert.That(lastNameExpected, Is.EqualTo(_book.Summary));
    }
    
    [Test]
    public void GetIsbnFromTheBook()
    {
        //Given
        string isbnExpected = "2-070039-01-3";
        //When //Then
        Assert.That(isbnExpected, Is.EqualTo(_book.Isbn.IsbnOfTheBook));
    }
    
    [Test]
    public void GetFirstNameOfTheAuthorOfTheBook()
    {
        //Given
        string firstNameExpected = "Emile";
        //When //Then
        Assert.That(firstNameExpected, Is.EqualTo(_book.Author.FirstName));
    }
    
    [Test]
    public void GetLastNameOfTheAuthorOfTheBook()
    {
        //Given
        string lastNameExpected = "Zola";
        //When //Then
        Assert.That(lastNameExpected, Is.EqualTo(_book.Author.LastName));
    }
    
    [Test]
    public void EmptyConstructor()
    {
        //Given
        Book emptyBook = new();
        //When //Then
        Assert.NotNull(emptyBook);
    }
    
    [Test]
    public void AddPagesToTheBook()
    {
        //Given
        IList<Page>? pagesOfTheBook = new List<Page>();
        IList<Choice> choicesOfTheFirstPage = new List<Choice>();
        Choice choice = new Choice("Allez en page 2");
        choice.NumPageTo = 2;
        choicesOfTheFirstPage.Add(choice);
        Page firstPage = new Page(1, "Vous commencez l'histoire", choicesOfTheFirstPage);
        pagesOfTheBook.Add(firstPage);
        _book.PagesOfTheBook = pagesOfTheBook;
        int numberOfPageExpected = 1;
        //When //Then
        Assert.That(numberOfPageExpected, Is.EqualTo(_book.PagesOfTheBook.Count));
    }
    
    [Test]
    public void CorrectFormatTitle()
    {
        //Given
        string? titleToVerify = "Le petit garçon qui volait des avions";
        //When //Then
        Assert.True(_book.CorrectLengthOfTitle(titleToVerify));
    }
    
    [Test]
    public void CorrectFormatSummary()
    {
        //Given
        string? summaryToVerify = "Colton est un jeune garçon, livré très tôt à lui-même";
        //When //Then
        Assert.True(_book.CorrectLengthOfSummary(summaryToVerify));
    }
    
    [Test]
    public void NotCorrectFormatTitle()
    {
        //Given
        string? titleToVerify = "";
        //When //Then
        Assert.False(_book.CorrectLengthOfSummary(titleToVerify));
    }
    
    [Test]
    public void NotCorrectFormatSummary()
    {
        //Given
        string? summaryToVerify = "";
        //When //Then
        Assert.False(_book.CorrectLengthOfSummary(summaryToVerify));
    }
    

    
    
    
    
    
    
    
}