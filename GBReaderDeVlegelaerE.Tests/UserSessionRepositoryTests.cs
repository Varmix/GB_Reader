using System.Text;
using GBReaderDeVlegelaerE.Domains;
using GBReaderDeVlegelaerE.Infrastuctures;
using GBReaderDeVlegelaerE.Infrastuctures.file;
using GBReaderDeVlegelaerE.Repositories;
using Newtonsoft.Json;
using NUnit.Framework.Constraints;

namespace GBReaderDeVlegelaerE.Tests;

public class UserSessionRepositoryTests 
{
    private static readonly string? ProjectDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;
    private string _resourcesPath = Path.Combine(ProjectDirectory!, "resources");
    private IDictionary<string?, UserSession> _allReadingSession;
    private IUserSessionRepository _jsonRepository;


    [SetUp]
    public void Setup()
    {
        //Console.WriteLine(NUnit.Framework.TestContext.CurrentContext.TestDirectory);
        _allReadingSession = new Dictionary<string, UserSession>()!;
    }
    

    [Test]
    public void LoadAFileThatNotExists() 
    {
        //Given
        string jsonFilePath = Path.Combine(_resourcesPath, "CreateFileTest.json");
       _jsonRepository = new UserSessionRepository(_resourcesPath, "CreateFileTest.json");
       
       //When
       try
       {
           _jsonRepository.Load();
       }
       catch (GameBookStorageException)
       {
           Assert.Fail("Erreur lors de la lecture dans le fichier CreateFileTest pour le test CreateFilWhenTheProgramWantToWrite");
       }

       //Then
       try
       {
           FileAssert.Exists(jsonFilePath);
       }
       catch (AssertionException)
       {
           Assert.Fail("Erreur lors de la vérification de l'existance du fichier CreateFileTest");
       }

       //Delete
       try
       {
           File.Delete(jsonFilePath);
       }
       catch (PathTooLongException)
       {
           Assert.Fail("Suppression du fichier impossible CreateFileTest.json");
       }
       catch (DirectoryNotFoundException)
       {
           Assert.Fail("Suppression du fichier impossible CreateFileTest.json");
       }
       catch (NotSupportedException)
       {
           Assert.Fail("Suppression du fichier impossible CreateFileTest.json");
       }
       catch (IOException)
       {
           Assert.Fail("Suppression du fichier impossible CreateFileTest.json");
       }
    }
    
    [Test]
    public void LoadCorrectlyData()
    {
        //Given
        string? isbn = "2-070039-05-6";
        UserSession userSession = new UserSession(1, Convert.ToDateTime("2022-12-07T08:51:02.2769356+01:00"), Convert.ToDateTime("2022-12-07T12:20:20.1133035+01:00"));
        string jsonFilePath = Path.Combine(_resourcesPath, "CorrectDataLoad.json");
        _jsonRepository = new UserSessionRepository(_resourcesPath, "CorrectDataLoad.json");
        UserSession userSessionTest = new ();
        //When
        ReadingStatistics readingStatistics = new ReadingStatistics();
        //When
        try
        {
            readingStatistics =  _jsonRepository.Load();
        }
        catch (GameBookStorageException)
        {
            Assert.Fail("Erreur lors de la lecture dans le fichier CorrectData.json pour le test LoadCorrectlyData");
        }
        
        
        //Then
        if (readingStatistics.UserReadings.TryGetValue(isbn, out userSessionTest!))
        {
            Assert.That(userSessionTest.LastPage, Is.EqualTo(userSession.LastPage));
            Assert.That(userSessionTest.StartReading, Is.EqualTo(userSession.StartReading));
            Assert.That(userSessionTest.LastUpdate, Is.EqualTo(userSession.LastUpdate));
        }
    }
    
    [Test]
    public void LoadDataWithBadFormatJson()
    {
        //Given
        _jsonRepository = new UserSessionRepository(_resourcesPath, "LoadDataWithBadFormatJson.json");
        //When
        //Then
        Assert.Throws<GameBookStorageException>(() => _jsonRepository.Load());
    }
    
    [Test]
    public void LoadDataWithValueNullInJson()
    {
        //Given
        _jsonRepository = new UserSessionRepository(_resourcesPath, "LoadNullInJson.json");
        //When
        ReadingStatistics readingStatistics = new ReadingStatistics();
        //When
        try
        {
            readingStatistics =  _jsonRepository.Load();
        }
        catch (GameBookStorageException)
        {
            Assert.Fail("Erreur lros de la lecture dans le fichier LoadNullInJson.json pour le test LoadDataWithValueNullInJson");
        }
        //Then
        Assert.That(readingStatistics.UserReadings, Is.Empty);
    }
    
    [Test]
    public void WriteSomeReadingSessionsAndLoadCorrectlyWithCreationFile()
    {
        //Given
        const string firstIsbn = "2-070039-03-X";
        const string secondIsbn = "2-070039-01-3";
        UserSession firstUserSession = new UserSession(2, Convert.ToDateTime("2022-12-07T11:49:38.6652227+01:00"), Convert.ToDateTime("2022-12-07T12:20:20.1133035+01:00"));
        UserSession secondUserSession = new UserSession(1, Convert.ToDateTime("2022-12-08T12:09:53.445323+01:00"), Convert.ToDateTime("2022-12-08T12:09:53.4453273+01:00"));
        _allReadingSession.Add(firstIsbn, firstUserSession);
        _allReadingSession.Add(secondIsbn, secondUserSession);
        _jsonRepository = new UserSessionRepository(_resourcesPath, "WriteCorrectlyInJson.json");
        string jsonFilePath = Path.Combine(_resourcesPath, "WriteCorrectlyInJson.json");
        UserSession userSessionFirstTest =  new();
        UserSession userSessionSecondTest = new();
        //When
        ReadingStatistics readingStatistics = new ReadingStatistics();
        try
        {
            _jsonRepository.Save(_allReadingSession);
            readingStatistics = _jsonRepository.Load();
        }
        catch (GameBookStorageException)
        {
            Assert.Fail("Erreur avec l'écriture ou lecture dans le fichier json pour le test WriteSomeReadingSessionsAndLoadCorrectlyWithCreationFile");
        }
        
        //Then
        if (readingStatistics.UserReadings.TryGetValue(firstIsbn, out userSessionFirstTest!))
        {
            Assert.That(userSessionFirstTest.LastPage, Is.EqualTo(firstUserSession.LastPage));
            Assert.That(userSessionFirstTest.StartReading, Is.EqualTo(firstUserSession.StartReading));
            Assert.That(userSessionFirstTest.LastUpdate, Is.EqualTo(firstUserSession.LastUpdate));
        }

        if (readingStatistics.UserReadings.TryGetValue(secondIsbn, out userSessionSecondTest!))
        {
            Assert.That(userSessionSecondTest.LastPage, Is.EqualTo(secondUserSession.LastPage));
            Assert.That(userSessionSecondTest.StartReading, Is.EqualTo(secondUserSession.StartReading));
            Assert.That(userSessionSecondTest.LastUpdate, Is.EqualTo(secondUserSession.LastUpdate));
        }

        try
        {
            File.Delete(jsonFilePath);
        }
        catch (PathTooLongException)
        {
            Assert.Fail("Suppression du fichier impossible WriteCorrectlyInJson.json");
        }
        catch (DirectoryNotFoundException)
        {
            Assert.Fail("Suppression du fichier impossible WriteCorrectlyInJson.json");
        }
        catch (NotSupportedException)
        {
            Assert.Fail("Suppression du fichier impossible WriteCorrectlyInJson.json");
        }
        catch (IOException)
        {
            Assert.Fail("Suppression du fichier impossible WriteCorrectlyInJson.json");
        }

    }
    
    [Test]
    public void LoadDataWithOneObjectDamageJson()
    {
        //Given
        _jsonRepository = new UserSessionRepository(_resourcesPath, "LoadDataWithOneObjectDamagedJson.json");
        //When
        //Then
        Assert.Throws<GameBookStorageException>(() => _jsonRepository.Load());
    }
    
    [Test]
    public void LoadDataWithAKeyMissingInJson()
    {
        //Given
        _jsonRepository = new UserSessionRepository(_resourcesPath, "LoadDataWithMissingKey.json");
        //When
        //Then
        Assert.Throws<GameBookStorageException>(() => _jsonRepository.Load());
    }
    
    [Test]
    public void LoadDataWithAKeyValueMissingInJson()
    {
        //Given
        _jsonRepository = new UserSessionRepository(_resourcesPath, "LoadDataWithMissingKeyValue.json");
        //When
        //Then
        Assert.Throws<GameBookStorageException>(() => _jsonRepository.Load());
    }
    
    [Test]
    public void LoadEmptyObjectInJson()
    {
        //Given
        _jsonRepository = new UserSessionRepository(_resourcesPath, "LoadEmptyObject.json");
        ReadingStatistics readingStatistics = new ReadingStatistics();
        //When
        try
        {
           readingStatistics =  _jsonRepository.Load();
        }
        catch (GameBookStorageException)
        {
            Assert.Fail("Erreur lors de la lecture dans le fichier json pour le test LoadEmptyObjectInJson");
        }
        //Then
        Assert.That(readingStatistics?.UserReadings, Is.Empty);
    }
    
    [Test]
    public void LoadDataWithTextAppendAtTheEndOfTheJson()
    {
        //Given
        _jsonRepository = new UserSessionRepository(_resourcesPath, "LoadDataWithTextAppendAtTheEnd.json");
        //When
        //Then
        Assert.Throws<GameBookStorageException>(() => _jsonRepository.Load());
    }
    
    [Test]
    public void WriteOneReadingSessionAndLoadCorrectlyExistingFile()
    {
        //Given
        const string firstIsbn = "2-070039-03-X";
        UserSession firstUserSession = new UserSession(2, Convert.ToDateTime("2022-12-07T11:49:38.6652227+01:00"), Convert.ToDateTime("2022-12-07T12:20:20.1133035+01:00"));
        _allReadingSession.Add(firstIsbn, firstUserSession);
        _jsonRepository = new UserSessionRepository(_resourcesPath, "WriteInAnExistingJsonFile.json");
        string jsonFilePath = Path.Combine(_resourcesPath, "WriteInAnExistingJsonFile.json");
        UserSession userSessionFirstTest =  new();
        //When
        ReadingStatistics readingStatistics = new ReadingStatistics();
        try
        {
            _jsonRepository.Save(_allReadingSession);
            readingStatistics = _jsonRepository.Load();
        }
        catch (GameBookStorageException)
        {
            Assert.Fail("Erreur avec l'écriture ou lecture dans le fichier json pour le test WriteSomeReadingSessionAndLoadCorrectlyExistingFile");
        }
        
        //Then
        if (readingStatistics.UserReadings.TryGetValue(firstIsbn, out userSessionFirstTest!))
        {
            Assert.That(userSessionFirstTest.LastPage, Is.EqualTo(firstUserSession.LastPage));
            Assert.That(userSessionFirstTest.StartReading, Is.EqualTo(firstUserSession.StartReading));
            Assert.That(userSessionFirstTest.LastUpdate, Is.EqualTo(firstUserSession.LastUpdate));
        }

        try
        {
            //Je réalise un troncage car sinon nous allons toujours lire/écrire les mêmes données afin que les résultats ne soit pas biaisés.
            File.WriteAllText(jsonFilePath, string.Empty);
        } 
        catch (PathTooLongException)
        {
            Assert.Fail("Erreur lors du troncage dans le fichier WriteInAnExistingJsonFile");
        }
        catch (DirectoryNotFoundException)
        {
            Assert.Fail("Erreur lors du troncage dans le fichier WriteInAnExistingJsonFile");
        }
        catch (FileNotFoundException)
        {
            Assert.Fail("Erreur lors du troncage dans le fichier WriteInAnExistingJsonFile");
        }
        catch (IOException)
        {
            Assert.Fail("Erreur lors du troncage dans le fichier WriteInAnExistingJsonFile");
        }
        
    }
    
    
    
}