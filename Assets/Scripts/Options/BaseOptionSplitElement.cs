using Libraries;
using UI;
using UI.Show;
using UnityEngine;

namespace Options
{
    public class BaseOptionSplitElement : SplitElement
    {
        protected readonly string category;

        public BaseOptionSplitElement(string category, string title, string icon) : base(title, icon)
        {
            this.category = category;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            foreach (var o in LSys.tem.options.GetAllByCategory(category))
            {
                if (!o.req.Check(null))
                {
                    continue;
                }

                o.AddOption(panel);
            }
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}