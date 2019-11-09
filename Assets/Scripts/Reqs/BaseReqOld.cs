using Buildings;
using Players;
using UnityEngine;

namespace reqs
{
    public abstract class BaseReqOld : BaseReq
    {
        
        /// <summary>
        /// Checks for this special object
        /// </summary>
        /// <param name="player"></param>
        /// <param name="onMap"></param>
        /// <param name="sett"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>true, if valid, false otherwise</returns>
        public abstract bool Check(Player player, GameObject onMap, string sett, int x, int y);

        /// <summary>
        /// Hide form user, if final
        /// </summary>
        /// <param name="player"></param>
        /// <param name="onMap"></param>
        /// <param name="sett"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public abstract bool Final(Player player, GameObject onMap, string sett, int x, int y);

    
        public abstract string Desc(Player player, GameObject onMap, string sett, int x, int y);
        

        public override bool Check(Player player, MapElementInfo onMap, string sett, int x, int y)
        {
            return Check(player, onMap == null?null:onMap.gameObject, sett, x, y);
        }

        public override bool Final(Player player, MapElementInfo onMap, string sett, int x, int y)
        {
            return Final(player, onMap == null?null:onMap.gameObject, sett, x, y);
        }

        public override bool Final(Player player, string sett)
        {
            return false;
        }

        public override string Desc(Player player, MapElementInfo onMap, string sett, int x, int y)
        {
            return Desc(player, onMap == null?null:onMap.gameObject, sett, x, y);
        }
    }
}