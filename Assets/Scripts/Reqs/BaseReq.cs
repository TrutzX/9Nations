using Buildings;
using JetBrains.Annotations;
using Players;
using UnityEngine;

namespace reqs
{
    public abstract class BaseReq : ScriptableObject
    {
        public string id;
        
        /// <summary>
        /// Checks for this special object
        /// </summary>
        /// <param name="player"></param>
        /// <param name="onMap"></param>
        /// <param name="sett"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>true, if valid, false otherwise</returns>
        public abstract bool Check(Player player, [CanBeNull] MapElementInfo onMap, string sett, int x, int y);
        
        /// <summary>
        /// Checks only for the player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="sett"></param>
        /// <returns>true, if valid, false otherwise</returns>
        public abstract bool Check(Player player, string sett);

        /// <summary>
        /// Hide form user, if final
        /// </summary>
        /// <param name="player"></param>
        /// <param name="onMap"></param>
        /// <param name="sett"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public abstract bool Final(Player player, [CanBeNull] MapElementInfo onMap, string sett, int x, int y);

        /// <summary>
        /// Hide form user, if final
        /// </summary>
        /// <param name="player"></param>
        /// <param name="sett"></param>
        /// <returns></returns>
        public abstract bool Final(Player player, string sett);

    
        public abstract string Desc(Player player, [CanBeNull] MapElementInfo onMap, string sett, int x, int y);

        /// <summary>
        /// Return an general desciption
        /// </summary>
        /// <param name="sett"></param>
        /// <returns></returns>
        public abstract string Desc(string sett);


    }
}