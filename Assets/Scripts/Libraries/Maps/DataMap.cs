using System;
using System.IO;
using Help;
using IniParser.Model;
using IniParser.Parser;
using UI;
using UnityEngine;

namespace Libraries.Maps
{
    [Serializable]
    public class DataMap : BaseData
    {
        public string folder;
        public string level;
        public string author;
        public int width;
        public int height;
        //public DirectoryInfo folder;

        public DataMap()
        {
            Icon = "map";
        }

        public IniData Config()
        {
            IniDataParser i = new IniDataParser();
            //intern?
            if (Intern())
            {
                TextAsset t = Resources.Load<TextAsset>(folder.Substring(1) + "/map");
                return i.Parse(t.text);
            }
            
            return i.Parse(File.ReadAllText(folder));
        }
        public int[][] Layer(string format,int id)
        {
            if (Intern())
            {
                TextAsset t = Resources.Load<TextAsset>(Dir()+format+id);
                return CSV.Convert(CSV.Read(t.text));
            }
            
            return CSV.Convert(CSV.Read(File.ReadAllText(Dir()+format+id+".csv")));
        }

        private bool Intern()
        {
            return folder.StartsWith("!");
        }
        
        private string Dir()
        {
            if (Intern())
            {
                return folder.Substring(1) + Path.DirectorySeparatorChar;
            }
            
            DirectoryInfo dir = new FileInfo(folder).Directory;
            return dir.FullName + Path.DirectorySeparatorChar;
        }
        
        public override void ShowLexicon(PanelBuilder panel)
        {
            base.ShowLexicon(panel);

            panel.AddSubLabel("Author",author);
            panel.AddSubLabel("Level",level);
            panel.AddSubLabel("Size",$"{width}x{height}");
            if (Data.features.debug.Bool())
                panel.AddLabel("Folder: " + folder);
            req.BuildPanel(panel, null);
            
            if (Intern())
            {
                panel.AddHeaderLabel("Overview");
                panel.AddImage(Resources.Load<Sprite>(folder.Substring(1) + "/" + id));
            }
            else
            {
                FileInfo img = new FileInfo(Dir() + id + ".png");
                if (img.Exists)
                {
                    panel.AddHeaderLabel("Overview");
                    panel.AddImage(SpriteHelper.LoadExternalSprite(img.FullName));
                }
            }
        }
    }
}