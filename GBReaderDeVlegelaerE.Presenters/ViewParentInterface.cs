namespace GBReaderDeVlegelaerE.Presenters;

public interface ViewParentInterface
{
    //void setPresenter(MainPresenter mainPresenter);

    event EventHandler<GBBookViewModel> BookOnClick;
}