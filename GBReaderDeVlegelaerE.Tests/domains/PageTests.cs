using GBReaderDeVlegelaerE.Domains;

namespace GBReaderDeVlegelaerE.Tests;

public class PageTests
{

    private Page _page;
    
    
    [SetUp]
    public void Setup()
    {
        IList<Choice> choicesOfTheFirstPage = new List<Choice>();
        Choice choice = new Choice("Allez en page 2");
        choice.NumPageTo = 2;
        choicesOfTheFirstPage.Add(choice);
        _page = new Page(1, "Vous commencez l'histoire", choicesOfTheFirstPage);
    }

    

    [Test]
    public void GetTextePage()
    {
        //Given
        string textPageExpected = "Vous commencez l'histoire";
        //When //Then
        Assert.That(textPageExpected, Is.EqualTo(_page.TextePage));
    }
    
    [Test]
    public void GetNumPage()
    {
        //Given
        int numPageExpected = 1;
        //When //Then
        Assert.That(numPageExpected, Is.EqualTo(_page.NumPage));
    }
    
    [Test]
    public void VerifyThatThePageHaveAChoice()
    {
        //Given
        int numberOfChoicesExpected = 1;
        //When //Then
        Assert.That(numberOfChoicesExpected, Is.EqualTo(_page.AllOfChoices.Count));
    }
    
    
    [Test]
    public void ConstructorWithoutChoices()
    {
        //Given
        Page page = new Page(1, "PageDeTest");
        int numPageExpected = 1;
        string textPageExpected = "PageDeTest";
        int numberOfChoicesExepcted = 0;
        //When //Then
        Assert.That(numPageExpected, Is.EqualTo(page.NumPage));
        Assert.That(textPageExpected, Is.EqualTo(page.TextePage));
        Assert.That(numberOfChoicesExepcted, Is.EqualTo(page.AllOfChoices.Count));
    }
    
    [Test]
    public void ItsNotATerminalPage()
    {
        //Given //When //Then
        Assert.False(_page.ThisIsATerminalePage());
    }
    
    [Test]
    public void VerifyThatThePageHaveOneChoice()
    {
        //Given
        int numberOfChoicesExpected = 1;
        ////When //Then
        Assert.That(numberOfChoicesExpected, Is.EqualTo(_page.AllOfChoices.Count));
    }
    
    [Test]
    public void GiveANewListOfChoicesToThePage()
    {
        //Given
        IList<Choice> newListOfChoices = _page.AllOfChoices;
        newListOfChoices.Add(new Choice("Allez en page 3"));
        _page.AllOfChoices = newListOfChoices;
        int numberOfChoicesExpected = 2;
        ////When
        /// //Then
        Assert.That(numberOfChoicesExpected, Is.EqualTo(_page.AllOfChoices.Count));
    }
    
    
    
    
    
    
    
}