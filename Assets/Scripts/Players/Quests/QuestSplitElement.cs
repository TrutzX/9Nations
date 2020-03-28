using UI;
using UI.Show;
using UnityEngine;

namespace Players.Quests
{
    public class QuestSplitElement : SplitElement
    {
        protected Quest quest;
        public QuestSplitElement(Quest quest) : base(quest.name, quest.Icon)
        {
            this.quest = quest;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            quest.ShowInfo(panel, PlayerMgmt.ActPlayer());
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}