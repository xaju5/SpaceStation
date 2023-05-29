using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleBehaviour : MonoBehaviour
{
    private readonly string SOCKET_TAG = "Socket";
    
    [SerializeField]
    private float lockDistance = 2;

    private bool isSelected;
    private bool isLocked;

    private void Start()
    {
        isSelected = false;
        isLocked = false;
    }

    private void OnMouseDown()
    {
        isSelected = true;
        ShowSockets(true);
    }

    private void OnMouseDrag()
    {
        if (!isSelected) return;
        if (isLocked)
        {
            checkLock();
            return;
        }

        MoveSelectedModule();
        if ( Input.GetKeyDown(KeyCode.R))
        {
            RotateSelectedModule();
        }
    }
    private void OnMouseUp()
    {
        isSelected = false;
        ShowSockets(false);
        isLocked = false;
    }

    private void OnTriggerEnter(Collider otherSocket)
    {
        if (!isSelected) return;
        if (isLocked) return;
        if (!otherSocket.gameObject.CompareTag(SOCKET_TAG)) return;

        isLocked = true;
        AttachSocket(otherSocket.transform.position);
    }

    private void checkLock()
    {
        Vector3 distance = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)) - transform.position;
        if(distance.magnitude >= lockDistance)
        {
            isLocked = false;
        }
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

    private void ShowSockets(bool canShow)
    {
        GameObject[] sockets = GameObject.FindGameObjectsWithTag(SOCKET_TAG);
        foreach (GameObject socket in sockets)
        {
            socket.GetComponent<MeshRenderer>().enabled = canShow;
            socket.GetComponent<BoxCollider>().enabled = canShow;
        }
        
    }
    
    private void AttachSocket(Vector3 otherSocketPosition)
    {
        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            Vector3 offset = otherSocketPosition - child.position;
            if (offset.magnitude <= 1f)
            {
                transform.position += offset;
                break;
            }
        }
    }
}
