using UnityEngine;

namespace Tools
{
    public class VectorHelper
    {
        public static Vector3Int Add(Vector3Int def, int x, int y)
        {
            return new Vector3Int(def.x+x,def.y+y,def.z);
        }
    }
}