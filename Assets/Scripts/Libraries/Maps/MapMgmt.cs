using System;
using System.IO;
using UnityEngine;

namespace Libraries.Maps
{
    [Serializable]
    public class MapMgmt : BaseMgmt<DataMap>
    {
        public MapMgmt() : base("map") { }
        protected override void ParseElement(DataMap ele, string header, string data)
        {
            switch (header)
            {
                case "folder":
                    ele.folder = data;
                    break;
                case "file":
                    ele.folder = Path.Combine(Path.GetDirectoryName(lastRead),data);
                    break;
                case "author":
                    ele.author = data;
                    break;
                case "level":
                    ele.level = data;
                    break;
                case "width":
                    ele.width = Int(data);
                    break;
                case "height":
                    ele.height = Int(data);
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }
    }
}