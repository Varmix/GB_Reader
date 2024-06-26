using GBReaderDeVlegelaerE.Domains;
using NUnit.Framework.Internal;

namespace GBReaderDeVlegelaerE.Tests;

public class ChoiceTests
{

    private Choice choice = new Choice("Vous venez de terminer le livre");
    

    [Test]
    public void GetContentChoice()
    {
        //Given
        string contentChoiceExpected = "Vous venez de terminer le livre";
        //When //Then
        Assert.That(contentChoiceExpected, Is.EqualTo(choice.ContentChoice));
    }
    
    [Test]
    public void GiveANumPageToTheChoice()
    {
        //Given
        int numPageTo = 3;
        //When
        choice.NumPageTo = numPageTo;
        //Then
        Assert.That(numPageTo, Is.EqualTo(choice.NumPageTo));
    }





}