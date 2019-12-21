using System;
using System.IO;
using Help;
using IniParser;
using IniParser.Model;
using IniParser.Parser;
using Libraries;
using UI;
using UnityEngine;
using UnityEngine.UI;

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
            if (Intern())
            {
                TextAsset t = Resources.Load<TextAsset>(Folder.Substring(1) + "/map");
                return i.Parse(t.text);
            }
            
            return i.Parse(File.ReadAllText(Folder));
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
            return Folder.StartsWith("!");
        }
        
        private string Dir()
        {
            if (Intern())
            {
                return Folder.Substring(1) + Path.DirectorySeparatorChar;
            }
            
            DirectoryInfo dir = new FileInfo(Folder).Directory;
            return dir.FullName + Path.DirectorySeparatorChar;
        }
        
        public override void ShowDetail(PanelBuilder panel)
        {
            base.ShowDetail(panel);

            panel.AddSubLabel("Author",Author);
            if (!string.IsNullOrEmpty(Width) && !string.IsNullOrEmpty(Height))
                panel.AddLabel($"Size: {Width}x{Height}");
            if (Data.features.debug.Bool())
                panel.AddLabel("Folder: " + Folder);
            
            
            if (Intern())
            {
                panel.AddHeaderLabel("Overview");
                panel.AddImage(Resources.Load<Sprite>(Folder.Substring(1) + "/" + Id));
            }
            else
            {
                FileInfo img = new FileInfo(Dir() + Id + ".png");
                if (img.Exists)
                {
                    panel.AddHeaderLabel("Overview");
                    panel.AddImage(SpriteHelper.LoadExternalSprite(img.FullName));
                }
            }
        }
    }
}