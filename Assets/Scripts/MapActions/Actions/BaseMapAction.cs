using Buildings;
using UnityEngine;

namespace MapActions.Actions
{
    public abstract class BaseMapAction : ScriptableObject
    {
        public string id;

        /// <summary>
        /// Perform the operation
        /// </summary>
        /// <param name="self"></param>
        /// <param name="nonSelf"></param>
        public abstract void Perform(MapElementInfo self, MapElementInfo nonSelf);
    }
}