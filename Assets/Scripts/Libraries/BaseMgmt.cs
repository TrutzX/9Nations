using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Game;
using Help;
using IniParser.Model;
using IniParser.Parser;
using JetBrains.Annotations;
using Maps;
using Players;
using reqs;
using Tools;
using UI;
using UnityEngine;

namespace Libraries
{
    [Serializable]
    public class BaseMgmt<T> : IRead where T : BaseData, new()
    {
        [SerializeField] protected Dictionary<string, T> Data;
        [SerializeField] protected readonly string id;
        [SerializeField] protected string icon;
        [SerializeField] protected string lastRead;
        
        /// <summary>
        /// For save only
        /// </summary>
        public BaseMgmt() : this(null)
        {
        }
        
        public BaseMgmt(string id)
        {
            Data = new Dictionary<string, T>();
            this.id = id;
        }
        
        protected BaseMgmt(string id, string icon) : this(id)
        {
            this.icon = icon;
        }

        public int Length => Data.Count;

        public T this[string key]{
            get{
                if (ContainsKey(key))
                {
                    return Data[key];
                }
                throw new MissingMemberException($"Can not find {key} in {id}");
            }
        }

        public T Random()
        {
            return Data.ElementAt(UnityEngine.Random.Range(0, Data.Count)).Value;
        }

        public T Random(string category)
        {
            var all = GetAllByCategory(category);
            return all[UnityEngine.Random.Range(0, all.Count)];
        }
        
        public bool ContainsKey(string key)
        {
            return Data.ContainsKey(key);
        }

        protected bool Bool(string data)
        {
            if (!Boolean.TryParse(data, out var erg))
            {
                Debug.LogError($"Can not parse bool {data} for {id}");
            }
            return erg;
        }

        protected int Int(string data)
        {
            if (!Int32.TryParse(data, out var erg))
            {
                Debug.LogError($"Can not parse number {data} for {id}");
            }
            return erg;
        }

        protected float Float(string data)
        {
            if (!Double.TryParse(data, out var erg))
            {
                Debug.LogError($"Can not parse float {data} for {id}");
            }
            return (float) erg;
        }

        protected void Delimiter(Dictionary<string,string> refs, string data)
        {
            var d = SplitHelper.Delimiter(data);
            refs.Add(d.key,d.value);
        }

        protected void Delimiter(Dictionary<string,int> refs, string data)
        {
            var d = SplitHelper.DelimiterInt(data);
            refs.Add(d.key,d.value);
        }

        protected void Res(Dictionary<string,int> res, string data)
        {
            var d = SplitHelper.SplitInt(data);
            res.Add(d.key, d.value);
        }

        public IEnumerator ParseCsv(string path)
        {
            yield return LSys.tem.Load.ShowMessage("Loading "+Name());
            //string[][] data = Ncsv.Read(Read(path));
            //Debug.Log($"Library {id}: from {path}");
            string[][] data = Ncsv.Csv2ArrayOwn(Read(path));
            yield return LSys.tem.Load.ShowSubMessage($"Loading 0/{data.Length}");
            Debug.Log($"Library {id}: Reading {data.Length} elements with {data[0].Length} headers from {path}");
            for (int l = 1; l < data.Length; l++)
            {
                if (l % 10 == 0)
                {
                    yield return LSys.tem.Load.ShowSubMessage($"Loading {l}/{data.Length}");
                }
                
                //skip?
                if (string.IsNullOrEmpty(data[l][0]) || data[l][0].StartsWith("//"))
                {
                    continue;
                }

                T ele = GetOrCreate(data[l][0]);
                
                //wrong size?
                if (data[l].Length > data[0].Length)
                {
                    Debug.LogWarning($"{id}: {data[l][0]} has {data[l].Length} data and only {data[0].Length} header");
                    Debug.Log(String.Join(", ,",data[0]));
                    Debug.Log(String.Join(", ,",data[l]));
                }
                
                for (int i = 1; i < data[l].Length; i++)
                {
                    if (string.IsNullOrEmpty(data[l][i]) || i >= data[0].Length)
                    {
                        continue;
                    }
                    
                    if (data[0][i].StartsWith("//")) continue;

                    try
                    {
                        ParseElement(ele, data[0][i], data[l][i]);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                        Debug.Log($"{l}/{i}: {data[l].Length}/{data[0].Length}");
                        Debug.Log(data[l][0]+": "+ele);
                        Debug.Log(data[0][i]);
                        Debug.Log(data[l][i]);
                    }
                    
                    
                }
                
            }
            yield return LSys.tem.Load.ShowSubMessage("Tidy up "+Name());
            AfterLoad();
        }

        protected string Read(string path)
        {
            lastRead = path;
            //intern?
            if (path.StartsWith("!"))
            {
                TextAsset t = UnityEngine.Resources.Load<TextAsset>(path.Substring(1));
                return t.text;
            }

            return File.ReadAllText(path);
        }
        
        public IEnumerator ParseIni(string path)
        {
            yield return LSys.tem.Load.ShowMessage("Loading "+Name());
            
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
                    if (key.KeyName.StartsWith("//")) continue;
                    ParseElement(ele, key.KeyName, key.Value);
                }
            }
        }
        
        public string Name()
        {
            return TextHelper.Cap(S.T(id));
        }
        
        public string Id()
        {
            return id;
        }
        
        public Sprite Sprite()
        {
            if (string.IsNullOrEmpty(icon) && Data.Count > 0)
            {
                return Data.First().Value.Sprite();
            }
            return SpriteHelper.Load(icon);
        }

        protected virtual void ParseElement(T ele, string header, string data)
        {
            
            switch (header)
            {
                case "icon":
                    ele.Icon = data;
                    break;
                case "sound":
                    ele.Sound = data;
                    break;
                case "hidden":
                    ele.Hidden = Bool(data);
                    break;
                case "category":
                    ele.category = data;
                    break;
                case "desc":
                    ele.desc = data;
                    break;
                case "req":
                    ele.req.Add(data);
                    break;
                default:
                    Debug.LogWarning($"{id} "+(ele==null?"??":ele.id)+$" missing {header} for data {data}");
                    break;
            }
        }
        
        public T GetOrCreate(string id)
        {
            if (ContainsKey(id))
            {
                return this[id];
            }

            Data[id] = new T();
            Data[id].Id(id);
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

        /// <summary>
        /// This method will called after loading a file
        /// </summary>
        public virtual void AfterLoad(){}

        public List<T> GetAllByCategory(string category)
        {
            return Values().Where(i => !i.Hidden && !string.IsNullOrEmpty(i.category) && i.category.Contains(category)).ToList();
        }

        public List<T> GetAllByCategory(string category, Player player)
        {
            return GetAllByCategory(category).Where(i => i.req.Check(player)).ToList();
        }

        public string NameList(string[] ids)
        {
            return String.Join(S.T("separator"),ids.ToList().Select(i => this[i].Name()));
        }
    }
}