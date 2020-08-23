using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary>
    /// Author: https://forum.unity.com/threads/can-the-ui-buttons-detect-a-right-mouse-click.279027/
    /// </summary>
    public class ClickableObject : MonoBehaviour, IPointerClickHandler
    {

        public Action left;
        public Action middle;
        public Action right;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (left != null && eventData.button == PointerEventData.InputButton.Left)
            {
                left();
            } else if (middle != null && eventData.button == PointerEventData.InputButton.Middle)
                middle();
            else if (right != null && eventData.button == PointerEventData.InputButton.Right)
            {
                right();
            }
                
        }
    }
}