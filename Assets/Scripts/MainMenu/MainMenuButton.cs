using System;
using System.Collections;
using System.Collections.Generic;
using DataTypes;
using DefaultNamespace;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public void ShowCredits()
    {
        HelpHelper.ShowHelpWindow();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
