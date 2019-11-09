using Buildings;
using DataTypes;
using UI;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace MapActions.Actions
{
    public class AttackAction : BaseMapAction
    {
        public override void Perform(MapElementInfo self, MapElementInfo nonSelf)
        {
            // calc damage
            int damage = calcDamage(self, nonSelf);

            string a = $"{self.name} ({self.Player().name})";
            string d = $"{nonSelf.name} ({nonSelf.Player().name})";
            
            Debug.LogError("Damage:"+damage);
            
            // check it
            if (damage == 0) {
                OnMapUI.Get().unitUI.SetPanelMessage($"{self.name} and {d} are equal, nothing to win.");
                NAudio.Play("defend");
                //inform another player
                nonSelf.SetLastInfo($"Defended against {a}");
                return;
            }

            // counter fight
            if (damage < 0) {
                Perform(nonSelf, self);
                OnMapUI.Get().unitUI.SetPanelMessage($"{d} fight back.");
                return;
            }

            // win
            nonSelf.AddHp(-damage);
            OnMapUI.Get().unitUI.SetPanelMessage($"You won. {d} lose {damage} HP.");
            nonSelf.SetLastInfo($"{a} attacked you. {nonSelf.name} lose {damage} HP.");
            //int oX = defensor.getX();
            //int oY = defensor.getY();
            //UiHelper.textAnimation("-" + damage + " hp", oX, oY, true, Color.SALMON);

            //TODO add animation
            //defensor.getActor().addAction(Actions.sequence(Actions.color(Color.RED, 1), Actions.color(Color.WHITE, 1)));
            //getActor().addAction(Actions.parallel(Actions.sequence(Actions.moveTo(oX * 32, oY * 32, 1), Actions.moveTo(x * 32, y * 32, 1)),
            //    Actions.sequence(Actions.color(Color.BLACK, 1), Actions.color(Color.WHITE, 1))));

        }
        
        /// <summary>
        /// Calc the damage
        /// </summary>
        /// <param name="self"></param>
        /// <param name="nonSelf"></param>
        /// <returns></returns>
        private int calcDamage(MapElementInfo self, MapElementInfo nonSelf)
        {
            UnitInfo uSelf = (UnitInfo) self;
            UnitInfo unSelf = (UnitInfo) nonSelf;
            
            float dam = Random.Range(uSelf.config.dam_min, uSelf.config.dam_max + 1);
            int atk = uSelf.config.atk, def = unSelf.config.def;

            // add ethos
            string ae = self.Player().Nation().ethos, de = nonSelf.Player().Nation().ethos;
            // good>bad, bad>neutral, neutral>good
            if (ae == "good" && de == "bad" || ae == "bad" && de == "neutral" || ae == "neutral" && de == "good") {
                dam *= 1.25f;
            } else if (ae == de) {
                dam *= 0.75f;
            }

            // check multi
            if (atk >= def) {
                dam *= 1 + 0.05f * (atk - def);
            } else {
                dam *= 1 - 0.025f * (def - atk);
            }

            return (int) dam;
        }
    }
}