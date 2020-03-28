using System;
using Buildings;
using Libraries.FActions;
using Libraries.FActions.General;
using Players;
using Tools;

namespace Classes.Actions
{
    public class ActionCameraMove : BasePlayerPerformAction
    {
        public ActionCameraMove() : base("cameraMove"){}

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            CameraMove.Get().MoveTo(new NVector(holder.data["pos"]));
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.data["pos"] = setting;
            conf.trigger = ActionEvent.Direct;
            return conf;
        }
    }
}