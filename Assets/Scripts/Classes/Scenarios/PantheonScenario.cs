using Game;
using Players;
using Players.Quests;

namespace Classes.Scenarios
{
    public class PantheonScenario : IRun
    {

        public void Run()
        {
            int pid = S.Players().CreatePlayer(System.Environment.UserName, "forger");
            S.Player(pid).quests.Add(QuestHelper.Win().AddReq("building",">1:temple"));
        }

        public string ID()
        {
            return "pantheon";
        }
    }
}