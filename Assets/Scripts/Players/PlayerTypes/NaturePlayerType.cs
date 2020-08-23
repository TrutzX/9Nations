using System.Collections;
using Game;
using Libraries;
using Tools;
using UnityEngine;

namespace Players.PlayerTypes
{
    public class NaturePlayerType : BasePlayerType
    {
        public NaturePlayerType()
        {
            id = PlayerType.Nature;
        }
        
        public override IEnumerator Start(Player player)
        {
            player.fog.noFog = true;
            
            int tid = S.Towns().Create(player.name, player.id, new NVector(0, 0, 0), false);
            player.data["town"] = tid.ToString();

            //todo own class/modul?
            if (L.b.gameOptions["chestGen"].Bool())
            {
                yield return GenChest(tid);
            }
        }

        private static IEnumerator GenChest(int tid)
        {
            yield return LSys.tem.Load.ShowMessage(S.T("chestGenLoading"));
            int width = S.Game().data.map.width;
            int height = S.Game().data.map.height;
            int level = S.Game().data.map.levels.Count;
            int count = (width * height * level) / 50;
            int helper = 0;
            for (int i = 0; i < count;)
            {
                helper++;
                NVector pos = new NVector(Random.Range(0, width - 1), Random.Range(0, height - 1), Random.Range(0, level - 1));

                //passable?
                if (!S.Map().Terrain(pos).Passable())
                    continue;

                //full?
                if (!S.Building().Free(pos))
                    continue;

                S.Building().Create(tid, "chest", pos);

                if (helper > 500)
                {
                    Debug.LogWarning($"Can not place more chests ({i}/{count}).");
                    yield break;
                }

                if (i % 20 == 0)
                {
                    yield return LSys.tem.Load.ShowMessage(S.T("chestGenLoadingSub", i, count));
                }

                i++;
                helper = 0;
            }
        }
    }
}