using Game;
using UI;
using UI.Show;
using UnityEngine;

namespace Players.Quests
{
    public class DebugQuestSplitElement : SplitElement
    {
        public DebugQuestSplitElement() : base("debug", "debug")
        {
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            foreach (Quest q in S.ActPlayer().quests.quests)
            {
                panel.AddSubLabel(q.Name(),q.status.ToString(),q.Sprite());
            }
        }

        public override void Perform()
        {
            Debug.LogWarning("Not implemented");
        }
    }
}