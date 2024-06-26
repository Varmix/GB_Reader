using GBReaderDeVlegelaerE.Domains;

namespace GBReaderDeVlegelaerE.Presenters
{
    public class UserSessionViewModel
    {
        private string? _isbn;
        private UserSession _userSession;
        
        public UserSessionViewModel(string? isbn, UserSession userSessionInformation)
        {
            _isbn = isbn;
            _userSession = userSessionInformation;
        }

        public string? GetIsbn()
        {
            return _isbn;
        }

        public int LastPage() => _userSession.LastPage;

        public DateTime GetStartReading()
        {
            return _userSession.StartReading;
        }

        public DateTime GetLastUpdate()
        {
            return _userSession.LastUpdate;
        }
    }
}