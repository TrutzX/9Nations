using System.Collections;
using System.Collections.Generic;
using Game;
using Players;
using UnityEngine;

public class StartUpScen : MonoBehaviour
{
    private bool first;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (first == true)
        {
            first = true;
            GameMgmt.Init();
            int pid = PlayerMgmt.Get().CreatePlayer("userx", "north");
            UnitMgmt.Get().Create(pid,"nking", 3, 3);
            UnitMgmt.Get().Create(pid,"nsoldier", 0, 0);
            UnitMgmt.Get().Create(pid,"nworker", 5, 2);
            int tid = TownMgmt.Get().Create(NGenTown.GetTownName("north"), pid, 2, 2);
            BuildingMgmt.Get().Create(tid, "ntemple",5, 2);
            PlayerMgmt.Get().NextPlayer();
        }
    }
}
