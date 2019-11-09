using DataTypes;

namespace InputAction
{
    public class InputKeyHelper
    {
        /// <summary>
        /// Show the action with the key, if possible
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string ActionName(NAction action)
        {
            //has the action an input?
            if (!Data.inputKey.ContainsKey(action.id))
            {
                return action.name;
            }

            return Data.inputKey[action.id].Name();
        }
        
        /// <summary>
        /// Show the gameButton with the key, if possible
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string GameButtonName(GameButton button)
        {
            //has the action an input?
            if (!Data.inputKey.ContainsKey(button.id))
            {
                return button.name;
            }

            return Data.inputKey[button.id].Name();
        }
    }
}