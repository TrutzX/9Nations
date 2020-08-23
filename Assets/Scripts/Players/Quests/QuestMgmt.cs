using System;
using System.Collections.Generic;
using System.Linq;
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
            if (quests.Count(qu => qu.id == q.id) > 0)
            {
                throw new InvalidCastException($"A Quest with the id {q.id} already exist");
            }

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