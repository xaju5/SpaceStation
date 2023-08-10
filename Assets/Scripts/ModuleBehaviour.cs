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
    private List<GameObject> attachedSokectList;

    private void Start()
    {
        isSelected = false;
        isLocked = false;
        attachedSokectList = new();
    }

    private void OnMouseDown()
    {
        ModuleSelection();
    }

    private void ModuleSelection()
    {
        isSelected = true;
        ShowSockets(true);
        DeattachAllSockects();
    }

    private void OnMouseDrag()
    {
        if (!isSelected) return;
        if (isLocked)
        {
            CheckLock();
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
        ModuleUnselection();
    }

    private void ModuleUnselection(){
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
        AttachSocket(otherSocket.gameObject);
    }

    private void CheckLock()
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
    
    private void AttachSocket(GameObject otherGameObject)
    {
        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            Vector3 offset = otherGameObject.transform.position - child.position;
            SocketBehaviour childSocket = child.GetComponent<SocketBehaviour>();
            SocketBehaviour otherSocket = otherGameObject.GetComponent<SocketBehaviour>();

            if (offset.magnitude <= 1f && !otherSocket.IsAttached() && !childSocket.IsAttached())
            {
                childSocket.SetAttachedSocked(otherGameObject);
                otherSocket.SetAttachedSocked(child.gameObject);
                attachedSokectList.Add(child.gameObject);
                attachedSokectList.Add(otherGameObject);

                transform.position += offset;
                break;
            }
        }
    }

    private void DeattachAllSockects()
    {
        foreach(GameObject socket in attachedSokectList)
        {
            socket.GetComponent<SocketBehaviour>().DeleteAttachedSocked();
        }
        attachedSokectList.Clear();
    }
}
