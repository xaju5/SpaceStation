using UnityEngine;

public class Module : MonoBehaviour
{
    private Mount attachedMount;

    public void SetAttachedMount( Mount mount){
        attachedMount = mount;
    }

    public void RemoveAttachedMount(){
        attachedMount = null;
    }

    public Mount GetAttachedMount(){
        return attachedMount;
    }
}
