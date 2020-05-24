using Classes.GameButtons;
using Game;
using Help;
using Libraries;
using Players.Infos;
using Towns;
using UI;
using UnityEngine;

namespace Players.Kingdoms
{
    public class KingdomGameButtonRun : BaseGameButtonRun
    {
        public KingdomGameButtonRun() : base ("kingdom") { }

        protected override void Run(Player player)
        {
            
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Kingdom overview",null);
            b.Add(new PlayerInfoSplitElement(S.ActPlayer()));
            b.Add(new CameraUnitSplitElement(b));
            
            //add all towns
            foreach (Town t in S.Towns().GetByActPlayer())
            {
                b.Add(new CameraTownSplitElement(b, t));
            }
            
            LSys.tem.helps.AddHelp("kingdom", b);
            b.Add(new InfosSplitElement());
            b.Add(new LexiconSplitElement(S.ActPlayer().Nation()));
            b.Add(new LexiconSplitElement(GameMgmt.Get().gameRound.GetRound()));
            
            
            b.Finish();
        }

        public override Sprite Sprite(Player player)
        {
            return player.Coat().Sprite();
        }
    }
}