using System.Collections;

namespace Players.PlayerTypes
{
    public abstract class BasePlayerType
    {
        public PlayerType id;

        /// <summary>
        /// Called after start a game and load a game
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public virtual IEnumerator Begin(Player player)
        {
            yield return null;
        }
        
        /// <summary>
        /// Only called after start the game
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public virtual IEnumerator Start(Player player)
        {
            yield return null;
        }

        public virtual IEnumerator StartRound(Player player)
        {
            yield return null;
        }
        
        /// <summary>
        /// Only called after loading a game
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public virtual IEnumerator Loaded(Player player)
        {
            yield return null;
        }
        
        public virtual IEnumerator NextRound(Player player)
        {
            yield return null;
        }

        public virtual IEnumerator FinishRound(Player player)
        {
            yield return null;
        }
    }
}