using System.Collections;
using System.Collections.Generic;
using Help;
using Libraries;
using Libraries.Terrains;
using Loading;
using PixelsoftGames.PixelUI;
using UI;
using UI.Show;
using UnityEngine;

public class UITest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Start2());
    }

    // Update is called once per frame
    IEnumerator Start2()
    {
        yield return L.Init(GameObject.Find("UICanvas").GetComponentInChildren<LoadingScreen>(true));
        L.b.Load.FinishLoading();
        
        WindowTabBuilder t = WindowTabBuilder.Create("Tab test");
        t.Add(new LexiconSplitTab<DataTerrain>(L.b.terrain));
        t.Add(new T1("la","logo"));
        t.Add(new T1("lssassaasf dsddssda","debug"));
        
        t.Finish();
    }

    class T1 : Tab
    {

        public T1(string name, string icon) : base(name, icon)
        {
        }

        public override void Show(Transform parent)
        {
        }
    }
}
