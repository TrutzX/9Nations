using System;
using System.IO;
using Classes;
using Classes.Overlays;
using Game;
using Help;
using IniParser.Model;
using IniParser.Parser;
using UI;
using UnityEngine;

namespace Libraries.Overlays
{
    [Serializable]
    public class Overlay : BaseData
    {

        public Overlay()
        {
        }

        public BaseOverlay RunCode()
        {
            
            return LClass.s.overlaysRuns[id];
        }
    }
}