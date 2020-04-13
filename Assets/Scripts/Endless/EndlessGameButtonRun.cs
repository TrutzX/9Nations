using System.Collections.Generic;
using Classes.GameButtons;
using Libraries;
using Players;
using UI;

namespace Endless
{
    public class EndlessGameButtonRun : BaseGameButtonRun
    {
        public EndlessGameButtonRun() : base ("endless") { }

        protected override void Run(Player player)
        {
            L.b.gameOptions.startConfig = new Dictionary<string, string>();
            
            WindowTabBuilder t = WindowTabBuilder.Create("Endless game");
            t.Add(new MapSplitTab(L.b.gameOptions.startConfig));
            t.Add(new GeneralSplitTab(L.b.gameOptions.startConfig));
        
            t.Finish();
        }
    }
}