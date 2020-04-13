using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using Help;
using Libraries;
using Libraries.Elements;
using Players.Infos;
using reqs;
using UI;
using UnityEngine;

namespace Players
{
    [Serializable]
    public class PlayerDevelopmentNation
    {
        public List<string> elements;
        public int points;
        
        [NonSerialized] public Player player;

        public PlayerDevelopmentNation()
        {
            elements = new List<string>();
            points = 1;
        }
        
        public bool Contains(string element)
        {
            return elements.Contains(element);
        }
        
        public void StartRound()
        {
            //need to develop?
            if (elements.Count >= points)
            {
                return;
            }
            
            //has max?
            if (elements.Count >= player.Nation().maxElement)
                return;
            
            //has some in nations?
            if (player.Nation().elements.Count > elements.Count)
            {
                Develop(player.Nation().elements[elements.Count]);
                return;
            }
            
            //ask the player
            WindowBuilderSplit wbs = WindowBuilderSplit.Create("Develop your nation","Develop");
            foreach (var ele in L.b.elements.Values())
            {
                //has it?
                if (elements.Contains(ele.id)) continue;
                
                //can use it?
                if (ele.req.Check(player))
                {
                    wbs.AddElement(new PlayerDevelopmentSplitElement(ele, this));
                }
            }
                
            LSys.tem.helps.AddHelp("element", wbs);
            wbs.Finish();
        }

        public string TownHall()
        {
            for (int i = elements.Count-1; i >= 0; i--)
            {
                Element e = L.b.elements[elements[i]];
                if (!string.IsNullOrEmpty(e.townHall))
                    return e.townHall;
            }
            
            throw new MissingMemberException($"{elements.Count} elements has no townHall");
        }

        public void Develop(string element)
        {
            elements.Add(element);
            player.info.Add(new Info($"Your nation develop the {L.b.elements[element].name}.",L.b.elements[element].Icon));
            
            if (LClass.s.elementRuns.ContainsKey(element))
                LClass.s.elementRuns[element].Develop(player);
        }
    }
}