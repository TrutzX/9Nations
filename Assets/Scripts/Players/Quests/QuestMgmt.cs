using System;
using System.Collections.Generic;
using Players.Quests;

namespace Players
{
    [Serializable]
    public class QuestMgmt
    {
        public List<Quest> quests;
        [NonSerialized] public Player player;

        public QuestMgmt()
        {
            quests = new List<Quest>();
        }

        public void Add(Quest q)
        {
            quests.Add(q);
        }

        public void NextRound()
        {
            foreach (Quest q in quests)
            {
                q.NextRound(player);
            }
        }
    }
}