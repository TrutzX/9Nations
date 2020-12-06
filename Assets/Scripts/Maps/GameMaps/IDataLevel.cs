using UnityEngine;
using UnityEngine.Tilemaps;

namespace Maps.GameMaps
{
    public interface IDataLevel
    {
        int At(Vector3Int pos, bool winter = false);
    }
}