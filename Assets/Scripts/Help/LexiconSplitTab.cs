using Libraries;
using UI.Show;

namespace Help
{
    public class LexiconSplitTab<T> : SplitElementTab where T : BaseData
    {
        public LexiconSplitTab(BaseMgmt<T> mgmt) : base(mgmt.Name(), mgmt.Sprite())
        {
            foreach (string key in mgmt.Keys())
            {
                if (!mgmt[key].Hidden)
                    Add(new LexiconSplitElement(mgmt[key]));
            }
        }
    }
}