using System.Collections.Generic;
using System.Linq;
using GameButtons;
using Libraries;
using Players;
using UnityEngine;

namespace UI
{
    public class WindowsMgmt : MonoBehaviour
    {
        private static WindowsMgmt self;
    
        /// <summary>
        /// Get it
        /// </summary>
        /// <returns></returns>
        public static WindowsMgmt Get()
        {
            //return GameObject.Find("WindowsMgmt").GetComponentInChildren<WindowsMgmt>();
            return self;
        }

        private void Start()
        {
            self = this;
        }
    
        public List<DestroyGameObject> GetAllOpenWindow()
        {
            return transform.GetComponentsInChildren<DestroyGameObject>().Where(d => d.IsWindow).ToList();
        }
    
        public bool AnyOpenWindow()
        {
            return GetAllOpenWindow().Count > 0;
        }
    }
}
