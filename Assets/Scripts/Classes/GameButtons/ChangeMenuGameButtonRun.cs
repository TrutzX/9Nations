using Libraries;
using Players;
using Tools;
using UI;
using UnityEngine;

namespace Classes.GameButtons
{
    public class BackMenuGameButtonRun : BaseGameButtonRun
    {
        protected readonly string menu;

        public BackMenuGameButtonRun() : this("back", "title") { }

        public BackMenuGameButtonRun(string id = "back", string menu="title") : base(id)
        {
            this.menu = menu;
        }

        protected override void Run(Player player)
        {
            UIHelper.ClearChild(GameObject.Find("MenuPanel"));
            L.b.gameButtons.BuildMenu(null, menu, null, true, GameObject.Find("MenuPanel").transform);
        }
    }
    
    public class MoreMenuGameButtonRun : BackMenuGameButtonRun
    {
        public MoreMenuGameButtonRun() : base("more","more") { }
    }
}