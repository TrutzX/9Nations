using Buildings;
using Game;
using Libraries.FActions;
using Libraries.FActions.General;
using MapElements;
using MapElements.Units;
using Players;
using Tools;
using Units;

namespace Classes.Actions
{
    public class ActionHeal : BasePerformAction
    {
        public ActionHeal() : base("heal"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            UnitInfo self = (UnitInfo) info;
            UnitInfo nonSelf = S.Unit().At(pos);
            
            //use the ap and heal the other
            //TODO Rate?
            if (self.data.ap >= nonSelf.data.hpMax - nonSelf.data.hp)
            {
                self.data.ap -= nonSelf.data.hpMax + nonSelf.data.hp;
                nonSelf.data.hp = nonSelf.data.hpMax;
                nonSelf.AddNoti(S.T("healComplete"), DataAction().Icon);
                return;
            }

            nonSelf.data.hp += self.data.ap;
            nonSelf.AddNoti(S.T("healHp", self.data.ap), DataAction().Icon);
            self.data.ap = 0;
        }

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            throw new System.NotImplementedException();
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.trigger = ActionEvent.Direct;
            return conf;
        }
    }
}