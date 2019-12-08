using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Game;
using Help;
using IniParser.Model;
using IniParser.Parser;
using Maps;
using UI;
using UnityEngine;

namespace Libraries
{
    [Serializable]
    public abstract class BaseMgmt<T> : IRead where T : BaseData
    {
        [SerializeField] protected Dictionary<string, T> Data;
        [SerializeField] protected string name;
        [SerializeField] protected string icon;
        [SerializeField] protected string lastRead;

        public BaseMgmt()
        {
            Data = new Dictionary<string, T>();
        }

        protected BaseMgmt(string name, string icon) : this()
        {
            this.name = name;
            this.icon = icon;
        }

        public int Length{ get{ return Data.Count; } }
        
        public T this[string id]{
            get{
                if (ContainsKey(id))
                {
                    return Data[id];
                }
                throw new MissingMemberException($"Can not find {id} in {name}");
            }
        }
        
        protected bool ContainsKey(string key)
        {
            return Data.ContainsKey(key);
        }

        protected bool Bool(string data)
        {
            return Boolean.Parse(data);
        }

        protected int Int(string data)
        {
            return Int32.Parse(data);
        }

        protected void Ref(Dictionary<string,string> refs, string data)
        {
            string[] r = data.Split(':');
            refs.Add(r[0],r[1]);
        }

        protected void Res(Dictionary<string,int> res, string data)
        {
            string[] r = data.Split(':');
            res.Add(r[0],Int32.Parse(r[1]));
        }
        
        public IEnumerator ParseCsv(string path)
        {
            yield return L.b.Load.ShowMessage("Loading "+Name());
            string[][] data = CSV.Read(Read(path));
            yield return L.b.Load.ShowSubMessage($"Loading 0/{data.Length}");
            for (int l = 1; l < data.Length; l++)
            {
                if (l % 10 == 0)
                {
                    yield return L.b.Load.ShowSubMessage($"Loading {l}/{data.Length}");
                }
                
                //skip?
                if (data[l][0] == "")
                {
                    continue;
                }

                T ele = GetOrCreate(data[l][0]);
                for (int i = 1; i < data[l].Length; i++)
                {
                    if (string.IsNullOrEmpty(data[l][i]) || i >= data[0].Length)
                    {
                        continue;
                    }

                    try
                    {
                        ParseBaseElement(ele, data[0][i], data[l][i]);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                        Debug.Log($"{l}/{i}: {data[l].Length}/{data[0].Length}");
                        Debug.Log(data[0][i]);
                        Debug.Log(data[l][i]);
                    }
                    
                    
                }
                
            }
            yield return L.b.Load.ShowSubMessage("Tidy up "+Name());
            AfterLoad();
        }

        protected string Read(string path)
        {
            lastRead = path;
            //intern?
            if (path.StartsWith("!"))
            {
                TextAsset t = Resources.Load<TextAsset>(path.Substring(1));
                return t.text;
            }

            return File.ReadAllText(path);
        }
        
        public IEnumerator ParseIni(string path)
        {
            yield return L.b.Load.ShowMessage("Loading "+Name());
            
            IniData data = new IniDataParser().Parse(Read(path));
            
            //add level
            foreach (SectionData section in data.Sections)
            {
                T ele = GetOrCreate(section.SectionName);
                foreach (KeyData key in section.Keys)
                {
                    if (string.IsNullOrEmpty(key.Value))
                    {
                        continue;
                    }
                    ParseBaseElement(ele, key.KeyName, key.Value);
                }
            }
        }
        
        public string Name()
        {
            return name;
        }
        
        public Sprite Sprite()
        {
            if (string.IsNullOrEmpty(icon) && Data.Count > 0)
            {
                return Data.First().Value.Sprite();
            }
            return SpriteHelper.Load(icon);
        }

        protected void ParseBaseElement(T ele, string header, string data)
        {
            switch (header)
            {
                case "name":
                    ele.Name = data;
                    break;
                case "icon":
                    ele.Icon = data;
                    break;
                case "desc":
                    ele.Desc = data;
                    break;
                case "sound":
                    ele.Sound = data;
                    break;
                case "hidden":
                    ele.Hidden = Bool(data);
                    break;
                default:
                    ParseElement(ele, header, data);
                    break;
            }
        }

        protected abstract void ParseElement(T ele, string header, string data);
        
        protected T GetOrCreate(string id)
        {
            if (ContainsKey(id))
            {
                return this[id];
            }

            Data[id] = Create();
            Data[id].Id = id;
            return Data[id];
        }

        public Dictionary<string, T>.KeyCollection Keys()
        {
            return Data.Keys;
        }

        public Dictionary<string, T>.ValueCollection Values()
        {
            return Data.Values;
        }

        protected abstract T Create();

        /// <summary>
        /// This method will called after loading a file
        /// </summary>
        public virtual void AfterLoad(){}
    }
}