using System;
using System.Collections.Generic;

namespace Players
{
    [Serializable]
    public class QuestMgmt
    {
        public List<Quest> quests;

        public QuestMgmt()
        {
            quests = new List<Quest>();
        }

        public void AddQuest(Quest q)
        {
            quests.Add(q);
        }

        public void NextRound(Player player)
        {
            foreach (Quest q in quests)
            {
                q.NextRound(player);
            }
        }
    }
}