using System.IO;
using UnityEngine;

namespace Scenario
{
    public class Map
    {
        public string name;
        //public DirectoryInfo folder;

        public Map(string id)
        {
            name = id;
            //this.folder = folder;
        }

        public string Layer(int id)
        {
            return Resources.Load<TextAsset>($"Maps/{name}/{id}").text;
        }
    }
}