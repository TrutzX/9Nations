using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Tools
{
    public class Ncsv
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

        public static string[][] Csv2ArrayOwn(string data)
        {
            List<List<string>> array = new List<List<string>>();
            int row = 0;
            foreach (string d in StringToList(data))
            {
                if (d.Length > 0)
                    array.Add(new List<string>());

                string act = "";
                bool foundString = false;
                for (int i = 0; i < d.Length; i++)
                {
                    if (foundString)
                    {
                        if (d[i] == '"' && (i+1 >= d.Length || d[i+1] == ','))
                        {
                            array.Last().Add(act);
                            act = "";
                            foundString = false;
                        }
                        else
                        {
                            act += d[i];
                        }
                    }
                    else
                    {
                        if (d[i] == '"' && (i == 0 || d[i - 1] == ','))
                        {
                            foundString = true;
                        }
                        else if (d[i] == ',')
                        {
                            array.Last().Add(act);
                            act = "";
                        }
                        else
                        {
                            act += d[i];
                        }
                    }
                }

                if (act.Length > 0)
                    array.Last().Add(act);
                row++;
                
                //Debug.Log(d);
                //Debug.Log(String.Join("--",array.Last()));
            }

            //convert
            string[][] convert = new string[array.Count][];
            for (int l = 0; l < array.Count; l++)
            {
                convert[l] = array[l].ToArray();
            }
            return convert;
        }
        
        public static string[][] Csv2Array(string data, char delimeter)
        {
            // initialize variables
            var newline = '\n';
            var i = 0;
            var c = data[i];
            var row = 0;
            var col = 0;
            List<List<string>> array = new List<List<string>>();

            while (i < data.Length)
            {
                // skip whitespaces
                while (c == ' ' || c == '\t' || c == '\r')
                {
                    c = data[++i]; // read next char
                }

                // get value
                var value = "";
                if (c == '\"')
                {
                    // value enclosed by double-quotes
                    c = data[++i];

                    do
                    {
                        if (c != '\"')
                        {
                            // read a regular character and go to the next character
                            value += c;
                            c = data[++i];
                        }

                        if (c == '\"')
                        {
                            // check for escaped double-quote
                            var cnext = data[i + 1];
                            if (cnext == '\"')
                            {
                                // this is an escaped double-quote. 
                                // Add a double-quote to the value, and move two characters ahead.
                                value += '\"';
                                i += 2;
                                c = data[i];
                            }
                        }
                    } while (i < data.Length && c != '\"');

                    if (i >= data.Length-1)
                    {
                        throw new ArgumentException("Unexpected end of data, double-quote expected");
                    }

                    c = data[++i];
                }
                else
                {
                    // value without quotes
                    while (i < data.Length && c != delimeter && c != newline && c != ' ' && c != '\t' && c != '\r')
                    {
                        value += c;
                        c = data[++i];
                    }
                }

                // add the value to the array
                if (array.Count <= row)
                    array.Add(new List<string>());
                array[row].Add(value);

                // skip whitespaces
                while (c == ' ' || c == '\t' || c == '\r')
                {
                    c = data[++i];
                }

                // go to the next row or column
                if (c == delimeter)
                {
                    // to the next column
                    col++;
                }
                else if (c == newline)
                {
                    // to the next row
                    col = 0;
                    row++;
                }
                else if (i >= data.Length)
                {
                    // unexpected character
                    throw new ArgumentException("Delimiter expected after character " + i);
                }

                // go to the next character
                try
                {
                    c = data[++i];
                }
                catch (Exception e)
                {
                    Debug.Log(i);
                    Debug.Log(data.Length);
                    Debug.Log(data);
                    Debug.Log(c);
                    Debug.Log(e);
                    throw e;
                }
                
            }

            //convert
            string[][] convert = new string[array.Count][];
            for (int l = 0; l < array.Count; l++)
            {
                convert[l] = array[l].ToArray();
            }
            return convert;
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
            return Regex.Split(data, "\n|\r|\r\n");
        }
    }
}