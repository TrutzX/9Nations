using System.Collections;
using System.Collections.Generic;
using DigitalRuby.Tween;
using Game;
using Units;
using UnityEngine;

public class InputAction : MonoBehaviour
{
    public GameObject aUnit;
    

    // Update is called once per frame
    void Update()
    {
        //open window?
        if (WindowsMgmt.Get().AnyOpenWindow())
            return;
        
        MoveUnit();
        MoveCamera();
        CheckWindow();
    }

    void CheckWindow()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            WindowsMgmt.GameMainMenu();
        }
    }

    void MoveUnit()
    {
        if (aUnit == null)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            aUnit.GetComponent<UnitInfo>().MoveTo(0,+1);
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            aUnit.GetComponent<UnitInfo>().MoveTo(0,-1);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            aUnit.GetComponent<UnitInfo>().MoveTo(-1,0);
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            aUnit.GetComponent<UnitInfo>().MoveTo(+1,0);
        }
       
    }

    void MoveCamera()
    {

        if (CameraMove.Get().IsMoving())
            return;
        
        if (Input.GetKey(KeyCode.W))
        {
            MoveCamera(0,+1);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            MoveCamera(0,-1);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            MoveCamera(-1,0);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            MoveCamera(+1,0);
        }
       
    }

    void MoveCamera(int x, int y)
    {
        Transform trans = Camera.main.GetComponent<Transform>();
        //valide pos?
        if (trans.position.y + y < 0 || trans.position.x + x < 0 || trans.position.y + y > GameMgmt.Get().data.mapheight || trans.position.x + x > GameMgmt.Get().data.mapwidth)
        {
            NAudio.PlayBuzzer();
            return;
        }
        
        CameraMove.Get().MoveTo((int)trans.position.x + x, (int)trans.position.y + y);
    }
}
