using Towns;
using UI;
using UnityEngine;

namespace Players
{
    public class QuestHelper
    {

        public static Quest Lose()
        {
            return new Quest("Lose the game","base:destroy").AddAction("endGameLose","");
        }
        
        public static Quest Win()
        {
            return new Quest("Win the game","other:win").AddAction("endGameWin","");
        }

        public static void ShowQuestWindow()
        {
            
                //load buildings
                WindowBuilderSplit b = WindowBuilderSplit.Create("Quest window",null);

                foreach (Quest q in PlayerMgmt.ActPlayer().quests.quests)
                {
                    b.AddElement(new QuestSplitElement(q));
                }

                b.Finish();
            }

        public class QuestSplitElement : WindowBuilderSplit.SplitElement
        {
            protected Quest quest;
            public QuestSplitElement(Quest quest) : base(quest.name, SpriteHelper.LoadIcon(quest.icon))
            {
                this.quest = quest;
            }

            public override void ShowDetail(PanelBuilder panel)
            {
                quest.ShowInfo(panel);
            }

            public override void Perform()
            {
                Debug.LogWarning("Not implemented");
            }
        }
    }
}