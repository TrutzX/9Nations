using System.Collections.Generic;
using Actions;
using Buildings;
using Campaigns;
using DataTypes;
using Help;
using Players;
using reqs;
using UI;
using UnityEngine;

namespace MapActions
{
    public class CampaignHelper
    {
        public static void ShowCampaigns()
        {
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Select your campaign","Play");

            foreach(Campaign c in Data.campaign)
            {
                if (!c.hidden)
                    b.AddElement(new CampaignSplitElement(c));
            }
            
            b.Finish();
        }

        public static void ShowScenarios(Campaign campaign)
        {
            //load buildings
            WindowBuilderSplit b = WindowBuilderSplit.Create("Select your scenario","Play");

            foreach(DataTypes.Scenario s in campaign.Scenarios())
            {
                b.AddElement(new ScenarioSplitElement(s));
            }
            
            b.Finish();
        }
    }
    
   
}