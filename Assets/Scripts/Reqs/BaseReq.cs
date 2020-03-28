using Buildings;
using JetBrains.Annotations;
using Players;
using Tools;
using UnityEngine;

namespace reqs
{
    public abstract class BaseReq : ScriptableObject
    {
        public string id;
        
        /// <summary>
        /// Checks only for the player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="sett"></param>
        /// <returns>true, if valid, false otherwise</returns>
        public abstract bool Check(Player player, string sett);

        /// <summary>
        /// Checks for this special object
        /// </summary>
        /// <param name="player"></param>
        /// <param name="onMap"></param>
        /// <param name="sett"></param>
        /// <param name="pos"></param>
        /// <returns>true, if valid, false otherwise</returns>
        public abstract bool Check(Player player, [CanBeNull] MapElementInfo onMap, string sett, NVector pos);

        /// <summary>
        /// Hide form user, if final
        /// </summary>
        /// <param name="player"></param>
        /// <param name="sett"></param>
        /// <returns></returns>
        public abstract bool Final(Player player, string sett);

        /// <summary>
        /// Hide form user, if final
        /// </summary>
        /// <param name="player"></param>
        /// <param name="onMap"></param>
        /// <param name="sett"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public abstract bool Final(Player player, [CanBeNull] MapElementInfo onMap, string sett, NVector pos);

        /// <summary>
        /// Return an general description
        /// </summary>
        /// <param name="player"></param>
        /// <param name="sett"></param>
        /// <returns></returns>
        public abstract string Desc([CanBeNull] Player player, string sett);
        
        /// <summary>
        /// Return an general description
        /// </summary>
        /// <param name="player"></param>
        /// <param name="onMap"></param>
        /// <param name="sett"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public abstract string Desc(Player player, [CanBeNull] MapElementInfo onMap, string sett, NVector pos);
        
    }
}