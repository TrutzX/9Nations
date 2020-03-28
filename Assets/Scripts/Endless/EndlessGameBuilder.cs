using System.Collections.Generic;
using Help;
using Improvements;
using Libraries;
using Maps;
using UI;

namespace Endless
{
    public class EndlessGameBuilder
    {
        public static void Show()
        {
            Dictionary<string, string> startConfig = new Dictionary<string, string>();
            
        
            WindowTabBuilder t = WindowTabBuilder.Create("Endless game");
            t.Add(new MapSplitTab(startConfig));
            t.Add(new GeneralSplitTab(startConfig));
        
            t.Finish();
        }
    }
}