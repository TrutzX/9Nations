using System;
using System.IO;
using Libraries;

namespace Maps
{
    [Serializable]
    public class MapMgmt : BaseMgmt<NMap>
    {
        public MapMgmt()
        {
            name = "Maps";
        }
        protected override void ParseElement(NMap ele, string header, string data)
        {
            switch (header)
            {
                case "folder":
                    ele.Folder = data;
                    break;
                case "file":
                    ele.Folder = Path.Combine(Path.GetDirectoryName(lastRead),data);
                    break;
                case "author":
                    ele.Author = data;
                    break;
                case "width":
                    ele.Width = data;
                    break;
                case "height":
                    ele.Height = data;
                    break;
            }
        }

        protected override NMap Create()
        {
            return new NMap();
        }
    }
}