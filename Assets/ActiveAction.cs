using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ActiveAction : MonoBehaviour
{
    public GameObject tileset;

    public TileBase setTile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {

            Vector2 point = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

            int mx = (int) point.x;
            int my = (int) point.y;

            GameObject.Find("RessRound").GetComponent<Text>().text = (mx + "," + my);

            //unit their?
            GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");

            foreach (GameObject unit in units)
            {
                if ((int) unit.transform.position.x == mx && (int) unit.transform.position.y == my)
                {
                    GameObject.Find("RessRound").GetComponent<Text>().text = unit.name;
                }
            }

            Tilemap t = tileset.GetComponentInChildren<Tilemap>();
            GameObject.Find("RessRound").GetComponent<Text>().text +=
                t.HasTile(new Vector3Int(mx, my, (int) t.transform.position.z));
            TileBase b = (t.GetTile(new Vector3Int(mx, my, 0)));

            
            //t.SetTile(new Vector3Int(mx*2, my*2, 0),setTile);

            GameObject.Find("RessRound").GetComponent<Text>().text += MapMgmt.Get().GetTerrain(mx, my);
        }
        
        */
            
    }
}
