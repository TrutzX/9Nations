using InputAction;
using Libraries;
using UI.Show;

namespace Help
{
    public class HelpSplitTab : SplitElementTab
    {
        public HelpSplitTab() : base("Help", "magic:lexicon")
        {
            foreach(DataTypes.Help h in Data.help)
            {
                Add(new HelpSplitElement(h));
            }
            
            Add(new InputOptionSplitElement());
            Add(new StatisticSplitElement());
        }
    }
}