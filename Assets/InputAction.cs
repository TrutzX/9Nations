using System.Collections;
using System.Collections.Generic;
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
            GameObject.Find("WindowsMgmt").GetComponent<WindowsMgmt>().SwitchMainMenu();
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

        if (Input.GetKeyDown(KeyCode.W))
        {
            Move(Camera.main.GetComponent<Transform>(),0,+1);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Camera.main.GetComponent<Transform>(),0,-1);
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Camera.main.GetComponent<Transform>(),-1,0);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Camera.main.GetComponent<Transform>(),+1,0);
        }
       
    }

    void Move(Transform trans, int x, int y)
    {
        trans.position = new Vector3(trans.position.x+x,trans.position.y+y,trans.position.z);
    }
}
