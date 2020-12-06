using System;
using Game;
using UnityEngine;

namespace Libraries.Coats
{
    [Serializable]
    public class Coat : BaseData
    {
        public string icon16;
        public string flag, flag16;
        public Color color;

        public override string Name()
        {
            //todo dynamic
            return S.T(id.Length==1?"coat"+id:id);
        }
    }
}