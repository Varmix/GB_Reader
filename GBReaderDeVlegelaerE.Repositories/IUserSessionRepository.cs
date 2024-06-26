using GBReaderDeVlegelaerE.Domains;

namespace GBReaderDeVlegelaerE.Repositories;

public interface IUserSessionRepository
{
    ReadingStatistics Load();
    
    void Save(IDictionary<string?, UserSession> readingStatisticsUserReadings);
}