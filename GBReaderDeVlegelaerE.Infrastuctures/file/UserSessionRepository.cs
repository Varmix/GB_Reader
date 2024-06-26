using System.Text;
using GBReaderDeVlegelaerE.Domains;
using GBReaderDeVlegelaerE.Repositories;
using Newtonsoft.Json;

namespace GBReaderDeVlegelaerE.Infrastuctures.file;

public class UserSessionRepository : IUserSessionRepository
{
    /* Déclaration des attributs */
    private static string _currentUserHomeDir = Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
    private static string _folderPath = Path.Combine(_currentUserHomeDir, "ue36");
    private static string? _jsonFilePath;

    /// <summary>
    /// Constructeur pour l'application
    /// </summary>
    public UserSessionRepository()
    {
        _jsonFilePath = Path.Combine(_folderPath, "q210054-session.json");
    }

    /// <summary>
    /// Constructeur pour les tests
    /// </summary>
    /// <param name="resourcePath">le chemin pour le fichier</param>
    /// <param name="nameFolder">le nom du fichier</param>
    public UserSessionRepository(string resourcePath, string nameFolder)
    {
        _jsonFilePath = Path.Combine(resourcePath, nameFolder);
    }
    
    /// <summary>
    /// Cette méthode permet d'aller lire dans le fichier json les sessions de lecture
    /// </summary>
    /// <returns>Un objet ReadingStatistics comprenant les sessions de lecture</returns>
    /// <exception cref="GameBookStorageException">Une exception personnalisée peut être lancée lors de la lecture
    /// dans le fichier json</exception>
    public ReadingStatistics Load()
    {
        IDictionary<string?, UserSession> allReadingSessions = new Dictionary<string, UserSession>()!;
        Mapping mapper = new();
        try
        {
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
            if (!File.Exists(_jsonFilePath))
            {
                using (File.Create(_jsonFilePath!))
                {
                }
            }
            else
            {
                IDictionary<string?, UserSessionDTO> allReadingSessionsDTO = new Dictionary<string, UserSessionDTO>()!;
                allReadingSessionsDTO =
                    JsonConvert.DeserializeObject<IDictionary<string, UserSessionDTO>>(File.ReadAllText(_jsonFilePath))!;
                if (allReadingSessionsDTO != null && allReadingSessionsDTO.Count > 0)
                {
                    allReadingSessions = mapper.ToUserSessionsDTOToUserSession(allReadingSessionsDTO);
                }
            }

        }
        catch (JsonSerializationException)
        {
            throw new GameBookStorageException("Erreur lors de la sérialisation en lecture dans votre fichier json");
        }
        catch (JsonReaderException)
        {
            throw new GameBookStorageException("Erreur lors de la lecture des sessions dans le fichier json");
        }
        catch (PathTooLongException)
        {
            throw new GameBookStorageException("Chemin de fichier json trop long");
        }
        catch (DirectoryNotFoundException)
        {
            throw new GameBookStorageException("Le directoire de votre fichier json n'a pas été trouvé");
        }
        catch (FileNotFoundException)
        {
            throw new GameBookStorageException("Votre fichier json n'a pas été trouvé");
        }
        catch (IOException)
        {
            throw new GameBookStorageException("Erreur survenue dans le fichier json");
        }
        

        return new ReadingStatistics(allReadingSessions);
    }
    
    /// <summary>
    /// Cette méthode permet d'aller écrire les sessions de lecture dans le json
    /// </summary>
    /// <param name="userSessions">un dictionnaire comprenant les sessions de lecture</param>
    /// <exception cref="GameBookStorageException">Une exception personnalisée peut être lancée lors de l'éctirue dans le fichier</exception>
    public void Save(IDictionary<string?, UserSession> userSessions)
    {
        //Transformation des UserSession en UserSessionDTO
        Mapping mapper = new Mapping();
        IDictionary<string?, UserSessionDTO> allUsersSessionDTO = mapper.ToUserSessionsToUserSessionsDTO(userSessions);
        try
        {
            // var fileInfo = new FileInfo(_jsonFilePath);
            // var fileMode = fileInfo.Exists ? FileMode.Truncate : FileMode.Create;
            // using (FileStream fs = new FileStream(_jsonFilePath, fileMode, FileAccess.Write))
            {
                // using (StreamWriter writer = new StreamWriter(fs))
                {
                    if (allUsersSessionDTO != null)
                    {
                        string json = JsonConvert.SerializeObject(allUsersSessionDTO);
                        File.WriteAllText(_jsonFilePath!, json, Encoding.UTF8);
                    }

                }
            }
        }
        catch (JsonSerializationException)
        {
            throw new GameBookStorageException("Erreur lors de la sérialisation dans l'écriture du fichier json");
        }
        catch (PathTooLongException)
        {
            throw new GameBookStorageException("Chemin de fichier json trop long");
        }
        catch (DirectoryNotFoundException)
        {
            throw new GameBookStorageException("Le directoire de votre fichier json n'a pas été trouvé");
        }
        catch (FileNotFoundException)
        {
            throw new GameBookStorageException("Votre fichier json n'a pas été trouvé");
        }
        catch (IOException)
        {
            throw new GameBookStorageException("Erreur survenue dans le fichier json");
        }
    }
}