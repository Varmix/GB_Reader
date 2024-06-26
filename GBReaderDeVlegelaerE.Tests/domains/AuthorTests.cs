using GBReaderDeVlegelaerE.Domains;


namespace GBReaderDeVlegelaerE.Tests;

public class AuthorTests
{

    private Author _author;

    [SetUp]
    public void Setup()
    {
        _author = new Author("Emile", "Zola", "210054");
    }
    

    [Test]
    public void GetFirstName()
    {
        //Given
        string firstNameExpected = "Emile";
        //When //Then
        Assert.That(firstNameExpected, Is.EqualTo(_author.FirstName));
    }
    
    [Test]
    public void GetLastName()
    {
        //Given
        string lastNameExpected = "Zola";
        //When //Then
        Assert.That(lastNameExpected, Is.EqualTo(_author.LastName));
    }
    
    [Test]
    public void CorrectFormatMatricule()
    {
        //Given
        string matricule = "210054";
        //When //Then
        Assert.True(_author.CorrectFormatMatricule(matricule));
    }
    
    [Test]
    public void NotCorrectFormatMatricule()
    {
        //Given
        string matricule = "20000";
        //When //Then
        Assert.False(_author.CorrectFormatMatricule(matricule));
    }
    
    [Test]
    public void MinimumLengthOfFirstNameRequired()
    {
        //Given
        string firstName = "Jo";
        //When //Then
        Assert.True(_author.CoorectLengthOfFirstNameOrLastName(firstName));
    }
    
    [Test]
    public void MinimumLengthOfLastNameRequired()
    {
        //Given
        string lastName = "Bo";
        //When //Then
        Assert.True(_author.CoorectLengthOfFirstNameOrLastName(lastName));
    }
    
    [Test]
    public void MinimumLengthOfFirstNameIsNotRespected()
    {
        //Given
        string firstName = "J";
        //When //Then
        Assert.False(_author.CoorectLengthOfFirstNameOrLastName(firstName));
    }
    
    [Test]
    public void MinimumLengthOfLastNameIsNotRespected()
    {
        //Given
        string lastName = "B";
        //When //Then
        Assert.False(_author.CoorectLengthOfFirstNameOrLastName(lastName));
    }
    
    
    
    
    
    
    
    
}