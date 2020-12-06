using System.Linq;
using Buildings;
using Classes.Actions.Addons;
using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.Elements;
using Libraries.FActions;
using Libraries.FActions.General;
using Libraries.Spells;
using Libraries.Units;
using MapElements;
using Players;
using Tools;
using UI;
using UI.Show;
using Units;

namespace Classes.Actions
{
    
    public class ActionMagic : BaseSelectElementAction<SpellMgmt,Spell>
    {
        public ActionMagic() : base("magic"){}
        
        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            if (evt == ActionEvent.NextRound)
            {
                Spell spell = L.b.spells[holder.data["waiting"]];
                Cast(spell, info);
                return;
            }
            base.Perform(evt, player, info, pos, holder);
        }
        
        protected override SplitElement CreateSplitElement(Spell spell, MapElementInfo info, NVector pos, ISplitManager ism)
        {
            return new BaseSelectElementSplitElement<Spell>(id, spell, info, pos, ism, (go, position) =>
            {
                //check ap
                if (spell.cost > go.data.ap)
                {
                    ActionHelper.WaitRound(go.data.action, go.data.action.Get("magic"), go, go.Pos(), spell.cost, spell.id, ActionEvent.NextRound);
                    return;
                }

                go.data.ap -= spell.cost;
                Cast(spell, go);
            });
        }

        private static void Cast(Spell spell, MapElementInfo go)
        {
            //find destination
            ActionHolder ah = new ActionHolder();
            ah.data["waiting"] = spell.id;
            LClass.s.GetNewAction("magicSelect").PerformCheck(ActionEvent.Direct, S.ActPlayer(), go, go.Pos(), ah);
        }

        protected override SpellMgmt Objects()
        {
            return L.b.spells;
        }

        protected override void AddObject(Player player, MapElementInfo info, NVector pos, string key, ISplitManager set)
        {
            Spell build = Objects()[key];
            
            if (!info.data.spells.Know(key) || !build.req.Check(player, info, pos, true))
            {
                return;
            }
            
            var be = CreateSplitElement(build, info, pos, set);
            be.disabled = build.req.Desc(player, info, pos);
            set.Add(be);
            //win.AddBuilding(build.id);
        }
    }
}