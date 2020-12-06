using System.Collections;
using Game;
using Libraries;
using LoadSave;

namespace Players.PlayerTypes
{
    public class HumanPlayerType : BasePlayerType
    {
        public HumanPlayerType()
        {
            id = PlayerType.Human;
        }
        
        public override IEnumerator Begin(Player player)
        {
            
            yield return player.fog.CreatingFog(player.id);
        }
        

        public override IEnumerator FinishRound(Player player)
        {
            //TODO Autosave only for human player
            //save
            if (LSys.tem.options["autosave"].Bool())
            {
                yield return GameMgmt.Get().load.ShowSubMessage($"Save auto save");
                LoadSaveMgmt.UpdateSave($"autosave{player.id}",$"Auto save {player.name}");
            }

            yield return null;
        }
    }
}