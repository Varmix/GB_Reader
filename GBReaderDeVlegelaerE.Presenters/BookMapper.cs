using GBReaderDeVlegelaerE.Domains;

namespace GBReaderDeVlegelaerE.Presenters
{
    public class BookMapper
    {
        public static Book convertBookViewModelToBookBasic(GBBookViewModel gameBookViewModel)
        {
            return new Book(gameBookViewModel.getAuthor(), gameBookViewModel.getTitle(),
                gameBookViewModel.getIsbnForBook(), gameBookViewModel.getSummary());
        }

        public static GBBookViewModel ConvertBookToBookViewModel(Book book)
        {
            return new GBBookViewModel(book);
        }

        public static PageViewModel ConvertPageToPageViewModel(Page page)
        {
            return new PageViewModel(page);
        }

        public static Page ConvertPageViewModelToPage(PageViewModel pageViewModel)
        {
            return new Page(pageViewModel.GetNumPage(), pageViewModel.GetTextePage(), pageViewModel.GetListOfChoices());
        }
        
    }
}