using System;
using System.Collections.Generic;
using Help;
using Game;
using Libraries;
using Libraries.Rounds;
using Tools;
using Towns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadSave
{
    public class LoadSaveMgmt
    {
        public static LoadSaveInfo CreateNewSave()
        {
            //todo add name dynm
            return UpdateSave(GetNextFreeFile(),"Endless game");
        }

        public static LoadSaveInfo UpdateSave(string file, string name)
        {
            Debug.Log($"UpdateSave {file}");
            //Collect infos
            LoadSaveInfo s = new LoadSaveInfo();
            s.date = DateTime.Now;
            s.name = name;
            s.round = S.Round().GetRoundString();
            s.version = Application.version;
            s.file = file;
            ES3.Save<LoadSaveInfo>("info",s,file+"info.9n");
            ES3.Save<L>("lib",L.b,file+"lib.9n");
            ES3.Save<GameData>("game",GameMgmt.Get().data,file+"game.9n");

            return s;
        }

        public static LoadSaveInfo LoadInfo(string file)
        {
            return ES3.Load<LoadSaveInfo>("info", file+"info.9n");
        }

        public static List<LoadSaveInfo> GetAllSaves()
        {
            List<LoadSaveInfo> saves = new List<LoadSaveInfo>();
            
            foreach (string save in ES3.GetFiles(""))
            {
                if (save.EndsWith("info.9n"))
                {
                    try
                    {
                        saves.Add(LoadInfo(save.Substring(0,save.Length-7)));
                    }
                    catch (Exception e)
                    {
                        //ignore
                        Debug.LogWarning($"Can not load save {save}");
                        Debug.LogException(e);
                    }
                }
                    
            }
            
            return saves;
        }

        public static string GetNextFreeFile()
        {
            int id = 0;
            while (ES3.FileExists(id + "info.9n"))
            {
                id++;
            }

            return id.ToString();
        }

        public static void LoadSave(string file)
        {
            GameMgmt.StartConfig = new Dictionary<string, string>();
            GameMgmt.StartConfig["type"] = "load";
            GameMgmt.StartConfig["name"] = "save game "+file;
            GameMgmt.StartConfig["file"] = file;
            SceneManager.LoadScene(1);
        }
    }
}