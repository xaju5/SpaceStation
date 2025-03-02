using UnityEngine;

public class Mount : MonoBehaviour
{
    private GameObject mountedModule;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetMountedModule(GameObject newModule)
    {
        if(mountedModule != null){
            Destroy(mountedModule);
        }
        mountedModule = newModule;
        meshRenderer.enabled = false;
    }

    public GameObject GetMountedModule()
    {
        return mountedModule;
    }

    public void DestroyMountedModule(){
        Destroy(mountedModule);
        meshRenderer.enabled = true;
    }
}
