using Classes.GameButtons;
using Game;
using Help;
using Libraries;
using Players.Infos;
using Towns;
using UI;

namespace Players.Kingdoms
{
    public class KingdomGameButtonRun : BaseGameButtonRun
    {
        public KingdomGameButtonRun() : base ("kingdom") { }

        protected override void Run(Player player)
        {
            
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Kingdom overview",null);
            b.AddElement(new PlayerInfoSplitElement(PlayerMgmt.ActPlayer()));
            b.AddElement(new CameraUnitSplitElement(b));
            
            //add all towns
            foreach (Town t in S.Towns().GetByActPlayer())
            {
                b.AddElement(new CameraTownSplitElement(b, t));
            }
            
            LSys.tem.helps.AddHelp("kingdom", b);
            b.AddElement(new InfosSplitElement());
            b.AddElement(new LexiconSplitElement(PlayerMgmt.ActPlayer().Nation()));
            b.AddElement(new LexiconSplitElement(GameMgmt.Get().gameRound.GetRound()));
            
            
            b.Finish();
        }
    }
}