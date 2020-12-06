using System;
using Buildings;
using Game;
using LoadSave;
using Players;
using Tools;

namespace reqs
{
    public class ReqSaveFileCount : BaseReqMinMaxPlayer
    {

        protected override int ValueMax(Player player, string element, string sett)
        {
            //TODO
            return 10;
        }

        protected override int ValueAct(Player player, string element, string sett)
        {
            return LoadSaveMgmt.GetAllSaves().Count;
        }

        protected override string Name(string element, string sett)
        {
            return "save file";
        }
    }
    
}