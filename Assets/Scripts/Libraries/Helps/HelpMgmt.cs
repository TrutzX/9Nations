using System;
using Help;
using UI;

namespace Libraries.Helps
{
    [Serializable]
    public class HelpMgmt : BaseMgmt<NHelp>
    {
        public HelpMgmt() : base("help") { }
        
        public void AddHelp(string category, WindowBuilderSplit wbs)
        {
            foreach (NHelp h in GetAllByCategory(category))
            {
                wbs.Add(new LexiconSplitElement(h, $"About {h.name}", SpriteHelper.Load("lexicon")));
            }
        }
    }
}