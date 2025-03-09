using UnityEngine;

public class Mount : MonoBehaviour
{
    private GameObject mountedModule;
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void SetMountedModule(GameObject newModule)
    {
        if(mountedModule != null){
            Destroy(mountedModule);
        }
        mountedModule = newModule;
        SetMountTrigger(false);
    }

    public GameObject GetMountedModule()
    {
        return mountedModule;
    }

    public void DestroyMountedModule(){
        Destroy(mountedModule);
        SetMountTrigger(true);
    }

    private void SetMountTrigger(bool isEnabled){
        meshRenderer.enabled = isEnabled;
        boxCollider.enabled = isEnabled;
    }
}
