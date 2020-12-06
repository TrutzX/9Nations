using System.Collections;

namespace Tools
{
    public interface IGameRoundObject
    {
        /**
         * Start a new round. Round Infos via S.Round()
         */
        void StartRound();

        /**
         * Call before a round will be finished
         */
        IEnumerator FinishRound();

        /**
         * Call for performing the next round
         */
        IEnumerator NextRound();

        /**
         * Call after the game is loaded
         */
        void AfterLoad();
    }
}