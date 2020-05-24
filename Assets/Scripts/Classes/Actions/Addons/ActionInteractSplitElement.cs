
using Buildings;
using Game;
using Libraries.FActions;
using Players;
using Tools;
using UI;
using UI.Show;

namespace Classes.Actions.Addons
{
    public class ActionInteractSplitElement : SplitElement
    {
        private ActionHolders holders;
        private ActionHolder action;
        private MapElementInfo self;
        private NVector pos;
        
        public ActionInteractSplitElement(ActionHolders holders, ActionHolder action, MapElementInfo self, NVector pos) : base(action.DataAction().Name(), action.DataAction().Icon)
        {
            this.holders = holders;
            this.action = action;
            this.self = self;
            this.pos = pos;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            ActionDisplaySettings ads = new ActionDisplaySettings(panel, S.ActPlayer(), self, pos, action);
            action.PerformAction().BuildPanel(ads);
        }

        public override void Perform()
        {
            //check ap
            if (action.DataAction().cost > self.data.ap)
            {
                ActionHelper.WaitRound(holders, action, self, pos);
                return;
            }
            
            string erg = holders.Perform(action, ActionEvent.Direct, S.ActPlayer(), self, pos);
            
            if (erg != null)
                UIHelper.ShowOk(action.DataAction().Name(),erg);
        }
    }
}