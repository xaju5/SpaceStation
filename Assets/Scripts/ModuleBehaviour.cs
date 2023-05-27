using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleBehaviour : MonoBehaviour
{
    private readonly string sockedTag = "Socket";

    private void OnMouseDrag()
    {
        EnableSockets(true);
        MoveSelectedModule();
        if ( Input.GetKeyDown(KeyCode.R))
        {
            RotateSelectedModule();
        }
    }
    private void OnMouseUp()
    {
        EnableSockets(false);
    }

    private void MoveSelectedModule()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        transform.position = mousePosition;
    }

    private void RotateSelectedModule()
    {
        transform.Rotate(0, 0, 90);
    }

    private void EnableSockets(bool isEnable)
    {
        GameObject[] sockets = GameObject.FindGameObjectsWithTag(sockedTag);
        foreach (GameObject socket in sockets)
        {
            socket.GetComponent<MeshRenderer>().enabled = isEnable;
            socket.GetComponent<BoxCollider>().enabled = isEnable;
        }
        
    }
    
}
