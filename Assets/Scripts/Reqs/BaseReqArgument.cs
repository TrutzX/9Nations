using System;
using Buildings;
using JetBrains.Annotations;
using Players;
using Tools;

namespace reqs
{
    public class BaseReqArgument
    {
        public Player player;
        public string sett;
        public MapElementInfo onMap;
        public NVector pos;

        public BaseReqArgument(Player player, string sett, MapElementInfo onMap = null, NVector pos = null)
        {
            this.player = player;
            this.sett = sett;
            this.onMap = onMap;
            this.pos = pos;
        }

        /// <summary>
        /// Return false, if onMap && pos null
        /// </summary>
        /// <returns></returns>
        public bool NeedMap()
        {
            return !(onMap == null && pos == null);
        }

        /// <summary>
        /// Throws an exception, if onMap && pos null
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void NeedMapException()
        {
            if (!NeedMap()) 
                throw new NotImplementedException();
        }
    }
}