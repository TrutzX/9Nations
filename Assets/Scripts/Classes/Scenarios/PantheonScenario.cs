using Players;
using Players.Quests;

namespace Classes.Scenarios
{
    public class PantheonScenario : IRun
    {

        public void Run()
        {
            int pid = PlayerMgmt.Get().CreatePlayer(System.Environment.UserName, "forger");
            PlayerMgmt.Get(pid).quests.Add(QuestHelper.Win().AddReq("building",">1:temple"));
        }

        public string ID()
        {
            return "pantheon";
        }
    }
}