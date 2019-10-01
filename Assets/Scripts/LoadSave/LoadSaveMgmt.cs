using System;
using System.Collections.Generic;
using Help;
using Game;
using Towns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadSave
{
    public class LoadSaveMgmt
    {
        public static LoadSaveInfo CreateNewSave()
        {
            return UpdateSave(GetNextFreeFile());
        }

        public static LoadSaveInfo UpdateSave(string file)
        {
            //Collect infos
            LoadSaveInfo s = new LoadSaveInfo();
            s.date = DateTime.Now;
            s.name = "Endless game"; //TODO
            s.round = RoundMgmt.Get().GetRoundString();
            s.version = Application.version;
            s.file = file;
            
            ES3.Save<LoadSaveInfo>("info",s,file);
            ES3.Save<GameData>("game",GameMgmt.Get().data,file);

            return s;
        }

        public static LoadSaveInfo LoadInfo(string file)
        {
            return ES3.Load<LoadSaveInfo>("info", file);
        }

        public static List<LoadSaveInfo> GetAllSaves()
        {
            List<LoadSaveInfo> saves = new List<LoadSaveInfo>();
            //add files
            if (ES3.FileExists("autosave.9n"))
            {
                saves.Add(LoadInfo("autosave.9n"));
            }
            
            int id = 0;
            while (ES3.FileExists(id + ".9n"))
            {
                saves.Add(LoadInfo(id + ".9n"));
                id++;
            }

            return saves;
        }

        public static string GetNextFreeFile()
        {
            int id = 0;
            while (ES3.FileExists(id + ".9n"))
            {
                id++;
            }

            return id+ ".9n";
        }

        public static void LoadSave(string file)
        {
            GameMgmt.startConfig = new Dictionary<string, string>();
            GameMgmt.startConfig["type"] = "load";
            GameMgmt.startConfig["file"] = file;
            SceneManager.LoadScene(1);
        }
    }
}