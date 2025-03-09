using UnityEngine;

public class SpaceStationCreator : MonoBehaviour
{
    static float RAY_LENGHT = 100f;
    static float FOLLOW_MODULE_DISTANCE = 15f;

    [SerializeField] private Camera creatorCamera;
    [SerializeField] private LayerMask mountLayer;
    [SerializeField] private GameObject[] allModules;
    [SerializeField] private GameObject spaceStation;

    private GameObject currentModule;
    private float currentModuleRotation;
    private GameObject hiddenModule;
    private int currentModuleIndex;

    void Start()
    {
        currentModule = Instantiate(allModules[0]);
        spaceStation.transform.SetParent(transform);
        currentModuleIndex = 0;
        currentModuleRotation = 0;
    }

    void Update()
    {
        RotateSpaceStation();

        Ray ray = creatorCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, RAY_LENGHT, mountLayer))
        {
            Mount mount = hitInfo.transform.GetComponent<Mount>();
            PlaceCurrentModule(mount);

            if (Input.GetMouseButtonDown(0))
                MountCurrentModule(mount);
            
            if (mount.GetMountedModule() != null)
            {
                if (hiddenModule != null && hiddenModule != mount.GetMountedModule())
                    ShowHidedModule();
                HideMountedModule(mount);
            }
        }
        else
        {
            FollowPointer();
            if (hiddenModule != null)
                ShowHidedModule();
            
        }

        int mouseScroll = Mathf.RoundToInt(Input.GetAxis("Mouse ScrollWheel") * 10f);
        if (mouseScroll != 0f)
            SwapCurrentModule(mouseScroll);
        
        if (Input.GetMouseButtonDown(1))
            RotateCurrentModule();
        
    }

    private void PlaceCurrentModule(Mount mount)
    {
        currentModule.transform.position = mount.transform.position;
        currentModule.transform.rotation = calculateModuleRotation(mount);
    }
    private void SwapCurrentModule(int mouseScroll)
    {
        Vector3 currentModulePosition = currentModule.transform.position;
        currentModuleIndex = Mathf.RoundToInt(Mathf.Repeat(currentModuleIndex + mouseScroll, allModules.Length));
        Destroy(currentModule);
        currentModule = Instantiate(allModules[currentModuleIndex], currentModulePosition, Quaternion.identity);
    }

    private void FollowPointer()
    {
        currentModule.transform.position = creatorCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, FOLLOW_MODULE_DISTANCE));
        currentModule.transform.rotation = Quaternion.identity;
    }

    private void ShowHidedModule()
    {
        hiddenModule.SetActive(true);
        hiddenModule = null;
    }
    private void HideMountedModule(Mount mount)
    {
        hiddenModule = mount.GetMountedModule();
        hiddenModule.SetActive(false);
    }

    private void MountCurrentModule(Mount mount)
    {
        GameObject copiedModuleObject = Instantiate(currentModule, spaceStation.transform);
        copiedModuleObject.transform.position = mount.transform.position;
        copiedModuleObject.transform.rotation = calculateModuleRotation(mount);

        mount.SetMountedModule(copiedModuleObject);
        Module copiedModule = copiedModuleObject.GetComponent<Module>();
        copiedModule.SetAttachedMount(mount);
        copiedModule.EnableModuleMounts();

        creatorCamera.GetComponent<CameraBehaviour>().SetCameraOffset(copiedModuleObject.transform.position.magnitude);
    }

    private Quaternion calculateModuleRotation(Mount mount)
    {
        return Quaternion.AngleAxis(currentModuleRotation, mount.transform.right) * mount.transform.rotation;
    }
    private void RotateCurrentModule()
    {
        currentModuleRotation = (currentModuleRotation + 90f) % 360f;
    }

    private void RotateSpaceStation()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * Time.deltaTime * 60f);
        transform.Rotate(Vector3.right * Input.GetAxis("Vertical") * Time.deltaTime * 60f);
    }
}
