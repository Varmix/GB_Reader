using GBReaderDeVlegelaerE.Domains;

namespace GBReaderDeVlegelaerE.Presenters
{
    public class PageViewModel
    {
        private readonly Page _page;

        public PageViewModel(Page page)
        {
            this._page = page;
        }

        public int GetNumPage() => _page.NumPage;

        public string GetTextePage() => _page.TextePage;

        public IList<Choice> GetListOfChoices() => _page.AllOfChoices;

        public IList<ChoiceViewModel> GetListOfChoicesViewModel()
        {
            IList<ChoiceViewModel> allChoicesVm = new List<ChoiceViewModel>();
            IList<Choice> allChoices = _page.AllOfChoices;
            foreach (Choice choice in allChoices)
            {
                allChoicesVm.Add(new ChoiceViewModel(choice));
            }
            return allChoicesVm;
        }
    }
}