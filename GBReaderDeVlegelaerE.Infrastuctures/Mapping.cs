using System.Collections;
using GBReaderDeVlegelaerE.Domains;
using GBReaderDeVlegelaerE.Infrastuctures.file;

namespace GBReaderDeVlegelaerE.Infrastuctures;

public class Mapping
{
    /// <summary>
    /// Cette méthode permet de convertir un objet UserSession en UserSessionDTO
    /// </summary>
    /// <param name="userSession">un objet UserSession</param>
    /// <returns>un objet UserSessionDTO</returns>
    public UserSessionDTO toUserSessionDTO(UserSession userSession)
    {
        return new UserSessionDTO(userSession.LastPage, userSession.StartReading, userSession.LastUpdate);
    }

    /// <summary>
    /// Cette méthode permet de convertir un Dictionnaire de sessions de lecture
    /// en un dictionnaire de session de lecture DTO
    /// </summary>
    /// <param name="userSessions"></param>
    /// <returns></returns>
    public IDictionary<string?, UserSessionDTO> ToUserSessionsToUserSessionsDTO(IDictionary<string?, UserSession> userSessions)
    {
        IDictionary<string?, UserSessionDTO> userSessionsDTO = new Dictionary<string, UserSessionDTO>()!;
        foreach (var userSession in userSessions)
        {
            UserSession userSessionToConvert = userSession.Value;
            userSessionsDTO.Add(userSession.Key, toUserSessionDTO(userSessionToConvert));
        }

        return userSessionsDTO;
    }

    /// <summary>
    /// Cette méthode permet de convertir un UserSessionDTO en UserSession
    /// </summary>
    /// <param name="userSessionDto">un objet UserSessionDTO</param>
    /// <returns>un objet UserSession</returns>
    public UserSession ToUserSession(UserSessionDTO userSessionDto)
    {
        return new UserSession(userSessionDto.LastPage, userSessionDto.StartReading, userSessionDto.LastUpdate);
    }

    /// <summary>
    /// Cette méthode permet de convertir un dictionnaire de sessions de lecture DTO en
    /// un dictionnaire de sessions de lecture
    /// </summary>
    /// <param name="allReadingSessionsDto">Un dictionnaire de sessions de lecture DTO</param>
    /// <returns>Un dictionnaire de sessions de lecture</returns>
    public IDictionary<string?, UserSession> ToUserSessionsDTOToUserSession(IDictionary<string?, UserSessionDTO> allReadingSessionsDto)
    {
        IDictionary<string?, UserSession> userSessions = new Dictionary<string, UserSession>()!;
        foreach (var userSessionDto in allReadingSessionsDto)
        {
            UserSessionDTO userSessionDtoToConvert = userSessionDto.Value;
            userSessions.Add(userSessionDto.Key, ToUserSession(userSessionDtoToConvert));
        }

        return userSessions;
    }
}