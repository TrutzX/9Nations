using Audio;
using Buildings;
using Game;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    public class ActionAttackUnit : BasePerformAction
    {
        public ActionAttackUnit() : this("attackUnit"){}
        
        public ActionAttackUnit(string id) : base(id){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            Perform(info, S.Unit().At(pos));
        }

        public override void BuildPanel(ActionDisplaySettings sett)
        {
            //found dest?
            MapElementInfo info = S.Game().At(sett.pos);
            if (info == null)
            {
                base.BuildPanel(sett);
                return;
            }

            sett.header = $"Attack {info.name}";
            base.BuildPanel(sett);
            
            if (sett.compact)
                return;

            sett.panel.AddHeaderLabel($"{sett.mapElement.name} attacks");
            sett.panel.AddSubLabel("Attack",sett.mapElement.baseData.atk.ToString(),"atk");
            sett.panel.AddSubLabel("Damage",$"{sett.mapElement.baseData.damMin}-{sett.mapElement.baseData.damMax}","damage");

            sett.panel.AddHeaderLabel($"{info.name} defends");
            sett.panel.AddSubLabel("Defense",info.baseData.def.ToString(),"def");
            
            //both units?
            if (!sett.mapElement.IsBuilding() && !info.IsBuilding())
            {
                bool found = false;
                foreach (var fm in L.b.fightModis.Values())
                {
                    if (!fm.req.Check(sett.mapElement, sett.pos))
                        continue;

                    if (!found)
                    {
                        sett.panel.AddHeaderLabel("Fight modificator");
                        found = true;
                    }
                    
                    sett.panel.AddImageLabel(fm.Desc, fm.Icon);
                }
            }
        }
        
        public virtual void Perform(MapElementInfo self, MapElementInfo nonSelf)
        {
            // calc damage
            int damage = CalcDamage(self, nonSelf);

            string a = $"{self.name} ({self.Player().name})";
            string d = $"{nonSelf.name} ({nonSelf.Player().name})";
            
            Debug.LogError("Damage:"+damage);
            
            // check it
            if (damage == 0) {
                OnMapUI.Get().unitUI.ShowPanelMessage($"{self.name} and {d} are equal, nothing to win.");
                NAudio.Play("defend");
                //inform another player
                nonSelf.SetLastInfo($"Defended against {a}");
                return;
            }

            // counter fight
            if (damage < 0) {
                Perform(nonSelf, self);
                OnMapUI.Get().unitUI.ShowPanelMessage($"{d} fight back.");
                return;
            }

            // win
            nonSelf.AddHp(-damage);
            OnMapUI.Get().unitUI.ShowPanelMessage($"You won. {d} lose {damage} HP.");
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
        private int CalcDamage(MapElementInfo self, MapElementInfo nonSelf)
        {
            
            float dam = Random.Range(self.baseData.damMin, self.baseData.damMax + 1);
            int atk = self.baseData.atk, def = nonSelf.baseData.def;

            foreach (var fm in L.b.fightModis.Values())
            {
                if (!fm.req.Check(self, nonSelf.Pos()))
                    continue;

                dam *= (1 + fm.modi / 100f);
            }

            // check multi
            if (atk >= def) {
                dam *= 1 + 0.05f * (atk - def);
            } else {
                dam *= 1 - 0.025f * (def - atk);
            }

            return (int) dam;
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