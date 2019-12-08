using System.Collections;
using UnityEngine;

namespace Libraries
{
    public interface IRead
    {
        IEnumerator ParseCsv(string path);
        
        IEnumerator ParseIni(string path);

        string Name();
        
        Sprite Sprite();

        void AfterLoad();
    }
}