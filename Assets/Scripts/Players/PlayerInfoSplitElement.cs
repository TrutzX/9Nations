using Game;
using Libraries;
using Libraries.Rounds;
using Players.Infos;
using UI;
using UI.Show;
using UnityEngine;

namespace Players
{
    public class PlayerInfoSplitElement: SplitElement
    {
        private Player _player;
        
        //TODO ADD Icon
        public PlayerInfoSplitElement(Player player) : base(player.name, player.Coat().Icon)
        {
            _player = player;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabelT("general");
            panel.AddSubLabel("Name",_player.name, _player.Coat().Icon);
            panel.AddSubLabel(L.b.nations.Name(),_player.Nation().Name(), _player.Nation().Icon);
            panel.AddSubLabel("Points",_player.points.ToString());
            panel.AddModi(S.Game().data.modi);

        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}