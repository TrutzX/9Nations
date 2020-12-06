using System.Linq;
using Buildings;
using Classes.Actions.Addons;
using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.Elements;
using Libraries.FActions;
using Libraries.FActions.General;
using MapElements;
using Players;
using Tools;
using UI;
using UI.Show;
using Units;

namespace Classes.Actions
{
    public abstract class BaseSelectElementAction<T, TU> : BasePerformAction
        where TU : BaseData, new() where T : BaseMgmt<TU>
    {
        protected BaseSelectElementAction(string id) : base(id)
        {
        }

        protected abstract T Objects();

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            if (CheckAllowed(player, info, pos, holder)) return;

            WindowTabBuilder wtb = WindowTabBuilder.Create(holder.DataAction().Desc());

            AddLast(player, info, pos, holder, wtb);

            foreach (string e in player.elements.elements)
            {
                Element ele = L.b.elements[e];
                SplitElementTab set = new SplitElementTab(ele.Name(), ele.Icon, holder.DataAction().Name());

                var b = Objects().GetAllByCategory(ele.id).OrderBy(o => o.Name()).ToList();
                foreach (TU build in b)
                {
                    AddObject(player, info, pos, build.id, set);
                }

                if (set.Count() > 0)
                {
                    wtb.Add(set);
                }
            }

            //has some?
            if (wtb.Count() >= 1)
            {
                wtb.Finish();
                return;
            }

            wtb.CloseWindow();
            info.UI().ShowPanelMessageError(S.T("tabNo", holder.DataAction().Desc()));
        }

        protected bool CheckAllowed(Player player, MapElementInfo info, NVector pos, ActionHolder holder)
        {
            if (!holder.data.ContainsKey("allowed"))
            {
                return false;
            }

            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create(holder.DataAction().Desc(), holder.DataAction().Name());

            foreach (string key in SplitHelper.Separator(holder.data["allowed"]))
            {
                AddObject(player, info, pos, key, b);
            }

            //has some?
            if (b.Count() >= 1)
            {
                b.Finish();
                return true;
            }

            b.CloseWindow();
            info.UI().ShowPanelMessageError(S.T("tabNo", holder.DataAction().Desc()));
            return true;
        }

        protected void AddLast(Player player, MapElementInfo info, NVector pos, ActionHolder holder,
            WindowTabBuilder wtb)
        {
            var last = L.b.playerOptions["last" + id];
            //add last used?
            if (!string.IsNullOrEmpty(last.Value()))
            {
                SplitElementTab set = new SplitElementTab(last.Name(), last.Icon, holder.DataAction().Name());
                foreach (string key in SplitHelper.Separator(last.Value()))
                {
                    if (string.IsNullOrEmpty(key)) continue;

                    AddObject(player, info, pos, key, set);
                }

                wtb.Add(set);
            }
        }

        protected virtual void AddObject(Player player, MapElementInfo info, NVector pos, string key, ISplitManager set)
        {
            TU build = Objects()[key];

            if (build.req.Check(player, info, pos, true))
            {
                var be = CreateSplitElement(build, info, pos, set);
                be.disabled = build.req.Desc(player, info, pos);
                set.Add(be);
                //win.AddBuilding(build.id);
            }
        }

        protected abstract SplitElement CreateSplitElement(TU build, MapElementInfo info, NVector pos,
            ISplitManager set);

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            throw new System.NotImplementedException();
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.trigger = ActionEvent.Direct;
            if (!string.IsNullOrEmpty(setting))
            {
                conf.data["allowed"] = setting;
            }

            return conf;
        }
    }
}