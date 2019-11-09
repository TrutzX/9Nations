using Buildings;

namespace MapActions.Actions
{
    public class HealAction : BaseMapAction
    {
        public override void Perform(MapElementInfo self, MapElementInfo nonSelf)
        {
            //use the hp and heal the other
            //TODO Rate?
            if (self.data.ap >= nonSelf.data.hpMax - nonSelf.data.hp)
            {
                self.data.ap -= nonSelf.data.hpMax + nonSelf.data.hp;
                nonSelf.data.hp = nonSelf.data.hpMax;
                return;
            }

            nonSelf.data.hp += self.data.ap;
            self.data.ap = 0;
            
        }
    }
}