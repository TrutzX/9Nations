using Audio;
using Buildings;
using DG.Tweening;
using Game;
using Libraries;
using Libraries.FActions;
using Libraries.FActions.General;
using MapElements;
using MapElements.Units;
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
            BuildPanelIntern(sett, S.Unit(sett.pos));
        }

        protected void BuildPanelIntern(ActionDisplaySettings sett, MapElementInfo info)
        {
            if (info == null)
            {
                base.BuildPanel(sett);
                return;
            }

            sett.header = TextHelper.Cap(S.T("attack", info.name));
            base.BuildPanel(sett);

            if (sett.compact)
                return;

            sett.panel.AddHeaderLabel(S.T("attackSelf", sett.mapElement.name));
            sett.panel.AddSubLabelT("atk", sett.mapElement.data.atk, "atk");
            sett.panel.AddSubLabelT("dam", S.T("attackDam", sett.mapElement.baseData.damMin, sett.mapElement.baseData.damMax),
                "damage");

            sett.panel.AddHeaderLabel(S.T("attackNonSelf", info.name));
            sett.panel.AddSubLabelT("def", info.data.def, "def");

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
                        sett.panel.AddHeaderLabelT("fightmodi");
                        found = true;
                    }

                    sett.panel.AddImageLabel(fm.Desc(), fm.Icon);
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
                OnMapUI.Get().unitUI.ShowPanelMessage(S.T("attackDamEqual",self.name,d));
                NAudio.Play("defend");
                //inform another player
                nonSelf.AddNoti($"Defended against {a}",self.baseData.Icon);
                return;
            }

            // counter fight
            if (damage < 0) {
                Perform(nonSelf, self);
                OnMapUI.Get().unitUI.ShowPanelMessage($"{d} fight back.");
                return;
            }
    
            // win
            ShowUnitAttack(self, nonSelf);
            nonSelf.AddHp(-damage);
            L.b.animations.Hp(-damage, nonSelf.Pos(), nonSelf);
            OnMapUI.Get().unitUI.ShowPanelMessage($"You won. {d} lose {damage} HP.");
            nonSelf.AddNoti($"{a} attacked you. {nonSelf.name} lose {damage} HP.",self.baseData.Icon);
            //int oX = defensor.getX();
            //int oY = defensor.getY();
            //UiHelper.textAnimation("-" + damage + " hp", oX, oY, true, Color.SALMON);

            //TODO add animation
            //defensor.getActor().addAction(Actions.sequence(Actions.color(Color.RED, 1), Actions.color(Color.WHITE, 1)));
            //getActor().addAction(Actions.parallel(Actions.sequence(Actions.moveTo(oX * 32, oY * 32, 1), Actions.moveTo(x * 32, y * 32, 1)),
            //    Actions.sequence(Actions.color(Color.BLACK, 1), Actions.color(Color.WHITE, 1))));

        }

        private void ShowUnitAttack(MapElementInfo own, MapElementInfo enemy)
        {
            int x = own.Pos().x - enemy.Pos().x;
            int y = own.Pos().y - enemy.Pos().y;
            
            Debug.LogWarning(own.name+" "+own.IsBuilding());
            //show attacker animation
            if (!own.IsBuilding())
            {
                UnitInfo unit = (UnitInfo) own;

                var sq = DOTween.Sequence();
                sq.Append(unit.transform.DOMove(new Vector3(unit.Pos().x + 0.5f - x/4f, unit.Pos().y - y/4f), 1));
                sq.Append(unit.transform.DOMove(new Vector3(unit.Pos().x + 0.5f, unit.Pos().y), 1));
                unit.UnitAnimator().PlayFightAnimation(x>0?UnitAnimatorType.AttackEast:x<0?UnitAnimatorType.AttackWest:y<0?UnitAnimatorType.AttackSouth:UnitAnimatorType.AttackNorth);
            }
            
            //show defender animation
            if (!enemy.IsBuilding())
            {
                UnitInfo unit = (UnitInfo) enemy;

                var sq = DOTween.Sequence();
                sq.Append(unit.transform.DOMove(new Vector3(unit.Pos().x + 0.5f - x/4f, unit.Pos().y - y/4f), 1));
                sq.Append(unit.transform.DOMove(new Vector3(unit.Pos().x + 0.5f, unit.Pos().y), 1));
                unit.UnitAnimator().PlayFightAnimation(x>0?UnitAnimatorType.DefendEast:x<0?UnitAnimatorType.DefendWest:y<0?UnitAnimatorType.DefendSouth:UnitAnimatorType.DefendNorth);
            }
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