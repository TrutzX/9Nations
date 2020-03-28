using System;
using Game;
using Players;
using UI;
using UnityEngine;

namespace Tools
{
    public class ExceptionHelper
    {
        /// <summary>
        /// Show and throw again
        /// </summary>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        public static void ShowException(Exception e)
        {
            string stack = e.StackTrace.Split(new[] { Environment.NewLine },StringSplitOptions.None)[0];
            
            WindowPanelBuilder w = WindowPanelBuilder.Create("A error happened");
            w.panel.AddLabel(e.GetType().ToString());
            w.panel.AddLabel(e.Message);
            w.panel.AddLabel(stack);
            w.panel.AddLabel("The error will sent automatically.");
            
            //add reset?
            if (GameHelper.IsGame())
            {
                w.panel.AddButton("Reset round", PlayerMgmt.Get().ResetRound);
            }
            
            w.AddClose();
            w.Finish();
            throw new Exception("Exception Handler",e);
        }
    }
}