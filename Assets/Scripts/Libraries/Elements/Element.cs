using System;
using System.Collections.Generic;
using Classes;
using Game;
using reqs;
using Tools;
using UI;

namespace Libraries.Elements
{
    [Serializable]
    public class Element : BaseData
    {
        public string townHall;

        public override void ShowLexicon(PanelBuilder panel)
        {
            base.ShowLexicon(panel);
            req.BuildPanel(panel, "Requirements for development");
            
            if (LClass.s.elementRuns.ContainsKey(id))
                LClass.s.elementRuns[id].ShowDetail(panel);
        }
    }
}
