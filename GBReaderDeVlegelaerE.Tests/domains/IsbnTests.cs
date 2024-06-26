using GBReaderDeVlegelaerE.Domains;

namespace GBReaderDeVlegelaerE.Tests;

public class IsbnTests
{

    private Isbn _isbn = new Isbn("2-070039-01-3");
    

    [Test]
    public void GetIsbn()
    {
        //Given
        string isbnExpected = "2-070039-01-3";
        //When //Then
        Assert.That(isbnExpected, Is.EqualTo(_isbn.IsbnOfTheBook));
    }
    
    [Test]
    public void EmptyConstructor()
    {
        //Given //When
        Isbn isbnEmpty = new();
        //Then
        Assert.IsNotNull(isbnEmpty);
    }
    
    [Test]
    public void CorrectIsbn()
    {
        //Given
        //When //Then
        Assert.True(_isbn.CheckISBN(_isbn.IsbnOfTheBook));
    }

    
    
    
    
    
    
}