using UnityEngine;

public class Module : MonoBehaviour
{
    [SerializeField] private Mount[] moduleMounts; //Current Module's mounts
    private Mount attachedMount; //Other Module's mount

    public void SetAttachedMount( Mount mount){
        attachedMount = mount;
    }

    public void RemoveAttachedMount(){
        attachedMount = null;
    }

    public Mount GetAttachedMount(){
        return attachedMount;
    }

    public void EnableModuleMounts(){
        foreach (Mount mount in moduleMounts)
        {
            mount.SetMountTrigger(true);
        }
    }
}
