namespace GBReaderDeVlegelaerE.Presenters;

public interface IWelcomeView
{

    void displayAllBooksView(GBBookViewModel gameBook);
    
    void displayAlert(string errorMsg);

    void displayWarning(string warningMsg);

    void DisplayError(string errorMsg);

    void onBookViewSelected(GBBookViewModel book);
    
    event EventHandler<string> FilterBook;

    event EventHandler<GBBookViewModel> OnDetailsBookClick;

    event EventHandler<string> SeeStatisticsPage;

}