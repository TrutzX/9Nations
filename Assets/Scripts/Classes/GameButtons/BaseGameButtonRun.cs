using Audio;
using Libraries;
using Libraries.GameButtons;
using Players;
using UI;
using UnityEngine;

namespace Classes.GameButtons
{
    public abstract class BaseGameButtonRun : ScriptableObject
    {
        public string id;

        protected BaseGameButtonRun(string id)
        {
            this.id = id;
        }

        public void Call(Player player)
        {
            GameButton b = L.b.gameButtons[id];
            if (!b.req.Check(player))
            {
                NAudio.PlayBuzzer();
                UIHelper.ShowOk(b.name, b.req.Desc(player));
                return;
            }
            Run(player);
        }

        protected abstract void Run(Player player);
    }
}