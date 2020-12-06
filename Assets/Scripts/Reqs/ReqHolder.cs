using System;
using System.Collections.Generic;
using Buildings;
using Game;
using MapElements;
using Players;
using Tools;
using UI;
using UnityEngine;

namespace reqs
{
    [Serializable]
    public class ReqHolder
    {
        public List<string[]> reqs;

        public ReqHolder()
        {
            reqs = new List<string[]>();
        }

        public void Add(string key, string value)
        {
            reqs.Add(new[]{key, value});
        }
        
        public bool Check(MapElementInfo onMap, NVector pos)
        {
            return Check(S.ActPlayer(), onMap, pos);
        }

        /// <summary>
        /// Check if the req allow it
        /// </summary>
        /// <param name="player"></param>
        /// <param name="onMap"></param>
        /// <param name="pos"></param>
        /// <param name="final"></param>
        /// <returns>true=can use it, false=not possible</returns>
        public bool Check(Player player, MapElementInfo onMap, NVector pos, bool final=false)
        {
            foreach (var req in reqs)
            {
                //can use it?
                BaseReq r = OLib.GetReq(req[0]);
                if (!r.Check(player, onMap, req[1], pos) && (r.Final(player, onMap, req[1], pos) || !final))
                {
                    return false;
                }
            }

            return true;
        }
        
        /// <summary>
        /// Check if the req allow it
        /// </summary>
        /// <param name="player"></param>
        /// <param name="final"></param>
        /// <returns>true=can use it, false=not possible</returns>
        public bool Check(Player player, bool final=false)
        {
            foreach (var req in reqs)
            {
                //can use it?
                BaseReq r = OLib.GetReq(req[0]);
                if (!r.Check(player, req[1]) && (r.Final(player, req[1]) || !final))
                {
                    return false;
                }
            }

            return true;
        }

        public string Desc(MapElementInfo onMap, NVector pos)
        {
            return Desc(S.ActPlayer(), onMap, pos);
        }

        /// <summary>
        /// Return the description, why the check its not working or null
        /// </summary>
        /// <param name="player"></param>
        /// <param name="reqs"></param>
        /// <param name="onMap"></param>
        /// <param name="pos"></param>
        /// <returns>the error message or null</returns>
        public string Desc(Player player, MapElementInfo onMap, NVector pos)
        {
            foreach (var req in reqs)
            {
                BaseReq br = OLib.GetReq(req[0]);
                
                //can use it?
                if (!br.Check(player, onMap, req[1], pos))
                {
                    return br.Desc(player, onMap, req[1], pos);
                }
            }

            return null;
        }
        

        /// <summary>
        /// Return the description, why the check its not working
        /// </summary>
        /// <param name="player"></param>
        /// <returns>the error message or null</returns>
        public string Desc(Player player)
        {
            foreach (var req in reqs)
            {
                BaseReq br = OLib.GetReq(req[0]);
                
                //can use it?
                if (!br.Check(player, req[1]))
                {
                    return br.Desc(player, req[1]);
                }
            }

            return null;
        }

        /// <summary>
        /// Build the reqs
        /// </summary>
        /// <param name="reqs"></param>
        /// <returns></returns>
        public void Add(params string[] reqs)
        {
            
            foreach (string req in reqs)
            {
                if (String.IsNullOrEmpty(req))
                {
                    continue;
                }

                var s = SplitHelper.Delimiter(req);
                Add(s.key,s.value);
            }
        }

        public void Add(ReqHolder req)
        {
            foreach (var r in req.reqs)
            {
                Add(r[0], r[1]);
            }
        }

        public void BuildPanel(PanelBuilder panel, Player player = null)
        {
            BuildPanel(panel,S.T("requirement",reqs.Count), player);
        }

        public void BuildPanel(PanelBuilder panel, string title, Player player = null)
        {
            try
            {
                //addHeader
                if (reqs.Count == 0)
                    return;
            
                panel.AddHeaderLabel(title);
        
                //add req
                foreach (var req in reqs)
                {
                    BaseReq r = OLib.GetReq(req[0]);
                    if (player == null)
                        panel.AddLabel(r.Desc(null, req[1]));
                    else
                    {
                        panel.AddImageLabel(r.Desc(player, req[1]), r.Check(player, req[1]));
                    }
                    
                }
            }
            catch (Exception e)
            {
                ExceptionHelper.ShowException(e);
            }
        }

        public void BuildPanel(PanelBuilder panel, MapElementInfo onMap, NVector pos)
        {
            BuildPanel(panel,S.T("requirement",reqs.Count), onMap, pos);
        }
        
        public void BuildPanel(PanelBuilder panel, string title, MapElementInfo onMap, NVector pos)
        {
            try
            {
                //addHeader
                if (reqs.Count == 0)
                    return;
                
                panel.AddHeaderLabel(title);
            
                //add req
                foreach (var req in reqs)
                {
                    BaseReq r = OLib.GetReq(req[0]);
                    panel.AddImageLabel(r.Desc(S.ActPlayer(),onMap, req[1], pos), r.Check(S.ActPlayer(), onMap, req[1], pos));
                }
            }
            catch (Exception e)
            {
                ExceptionHelper.ShowException(e);
            }
        }
    }
}