using Libraries;
using Libraries.Helps;
using Libraries.Inputs;
using UI.Show;

namespace Help
{
    public class HelpSplitTab : SplitElementTab
    {
        public HelpSplitTab() : base(LSys.tem.helps.Name(), LSys.tem.helps.Sprite())
        {
            foreach(NHelp h in LSys.tem.helps.Values())
            {
                Add(new LexiconSplitElement(h));
            }
            
            Add(new InputOptionSplitElement());
            Add(new StatisticSplitElement());
        }
    }
}