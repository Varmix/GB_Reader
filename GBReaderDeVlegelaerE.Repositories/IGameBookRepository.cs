using GBReaderDeVlegelaerE.Domains;

namespace GBReaderDeVlegelaerE.Repositories
{
    public interface IGameBookRepository
    {
        void LoadCovers();
        void LoadCompleteBookInformation(Book currentBook);
    }
}