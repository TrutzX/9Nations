using Players;
using Tools;

namespace reqs
{
    public class ReqFeaturePlayer : BaseReqOnlyPlayer
    {

        public override bool Check(Player player, string sett)
        {
            var v = SplitHelper.Split(sett);
            return player.GetFeature(v.key) == v.value;
        }

        public override bool Final(Player player, string sett)
        {
            return true;
        }

        public override string Desc(Player player, string sett)
        {
            return $"Need the feature {SplitHelper.Split(sett).key}.";
        }
    }
}