using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Help
{
    public class CSV
    {
        public static string[][] Read(FileInfo filePath)
        {
            //TextAsset textAsset = Resources.Load("1", TextAsset);
            List<string[]> lists = new List<string[]>();
            
            StreamReader sr = filePath.OpenText();
            
            string data;
            while ((data = sr.ReadLine()) != null)
            {
                lists.Add(data.Split(','));
            }
            
            return lists.ToArray();
        }

        public static string[][] Read(string data)
        {
            List<string[]> lists = new List<string[]>();
            foreach (string d in StringToList(data))
            {
                if (d.Length > 0)
                    lists.Add(d.Split(','));
            }
            
            return lists.ToArray();
        }

        public static int[][] Convert(string[][] data)
        {
            int[][] result = new int[data.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = Array.ConvertAll(data[i], int.Parse);
            }

            return result;
        }

        public static string[] StringToList(string data)
        {
            return Regex.Split ( data, "\n|\r|\r\n" );
        }
    }
}