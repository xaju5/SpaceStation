using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketBehaviour : MonoBehaviour
{
    private GameObject attachedSocket;
    private bool isAttached;

    private void Start()
    {
        isAttached = false;
        attachedSocket = null;
    }

    public bool IsAttached()
    {
        return isAttached;
    }
    public void SetAttachedSocked(GameObject socket)
    {
        isAttached = true;
        attachedSocket = socket;
    }
    public void DeleteAttachedSocked()
    {
        isAttached = false;
        attachedSocket = null;
    }
}
