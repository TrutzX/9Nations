using Classes.Actions.Addons;
using Game;
using Libraries;
using Libraries.FActions;
using Libraries.Spells;
using MapElements;
using Players;
using Players.Infos;
using Tools;
using UI;
using UI.Show;
using Units;

namespace Classes.Actions
{
    public class ActionSpellLearn : BaseSelectElementAction<SpellMgmt,Spell>
    {
        public ActionSpellLearn() : base("spellLearn"){}

        protected override SpellMgmt Objects()
        {
            return L.b.spells;
        }

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            if (evt == ActionEvent.NextRound)
            {
                LearnSpell(holder.data["waiting"], info);
                return;
            }
            base.Perform(evt, player, info, pos, holder);
        }

        protected override void AddObject(Player player, MapElementInfo info, NVector pos, string key, ISplitManager set)
        {
            Spell spell = Objects()[key];
            
            if (info.data.spells.Know(key) || !spell.req.Check(player, info, pos, true))
            {
                return;
            }
            
            //know it?
            var be = CreateSplitElement(spell, info, pos, set);
            be.disabled = spell.req.Desc(player, info, pos);
            set.Add(be);
            //win.AddBuilding(build.id);
            
        }
        
        protected override SplitElement CreateSplitElement(Spell spell, MapElementInfo info, NVector pos, ISplitManager ism)
        {
            return new BaseSelectElementSplitElement<Spell>(id, spell, info, pos, ism, (go, position) =>
            {
                //check ap
                if (spell.cost*10 > go.data.ap)
                {
                    ActionHelper.WaitRound(go.data.action, go.data.action.Get("spellLearn"), go, go.Pos(), spell.cost*10, spell.id, ActionEvent.NextRound);
                    return;
                }

                go.data.ap -= spell.cost * 10;
                LearnSpell(spell.id, go);
            });
        }

        private void LearnSpell(string key, MapElementInfo go)
        {
            Spell spell = Objects()[key];
            go.data.spells.Learn(spell.id);
            
            go.AddNoti(new Info(S.T("spellLearned", go.name, spell.Name()),spell.Icon));
        }
    }
}