using GBReaderDeVlegelaerE.Domains;

namespace GBReaderDeVlegelaerE.Presenters
{
    public class ChoiceViewModel
    {

        private Choice _choice;
        
        public ChoiceViewModel(Choice choice)
        {
            this._choice = choice;
        }

        public int GetNumPageTo() => _choice.NumPageTo;

        public string GetContentChoice() => _choice.ContentChoice;

        public override string ToString() => $"{GetContentChoice()} : allez en page : {GetNumPageTo()}";
    }
}