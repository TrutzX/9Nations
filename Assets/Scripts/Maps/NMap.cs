using System;
using System.IO;
using Help;
using IniParser;
using IniParser.Model;
using IniParser.Parser;
using Libraries;
using UnityEngine;

namespace Maps
{
    [Serializable]
    public class NMap : BaseData
    {
        public string Folder;
        public string Author;
        public string Width;
        public string Height;
        //public DirectoryInfo folder;

        public NMap()
        {
            Icon = "map";
        }

        public IniData Config()
        {
            IniDataParser i = new IniDataParser();
            //intern?
            if (Folder.StartsWith("!"))
            {
                TextAsset t = Resources.Load<TextAsset>(Folder.Substring(1) + "/map");
                return i.Parse(t.text);
            }
            
            return i.Parse(File.ReadAllText(Folder));
        }
        public int[][] Layer(string format,int id)
        {
            if (Folder.StartsWith("!"))
            {
                TextAsset t = Resources.Load<TextAsset>(Folder.Substring(1) + Path.DirectorySeparatorChar+format+id);
                return CSV.Convert(CSV.Read(t.text));
            }
            
            DirectoryInfo dir = new FileInfo(Folder).Directory;
            Debug.Log(dir.FullName+ Path.DirectorySeparatorChar+format+id+".csv");
            
            return CSV.Convert(CSV.Read(File.ReadAllText(dir.FullName + Path.DirectorySeparatorChar+format+id+".csv")));
        }
        
        public override void ShowDetail(PanelBuilder panel)
        {
            base.ShowDetail(panel);
            panel.AddSubLabel("Author",Author);
            if (!string.IsNullOrEmpty(Width) && !string.IsNullOrEmpty(Height))
                panel.AddLabel($"Size: {Width}x{Height}");
            if (Data.features.debug.Bool())
                panel.AddLabel("Folder: " + Folder);
        }
    }
}