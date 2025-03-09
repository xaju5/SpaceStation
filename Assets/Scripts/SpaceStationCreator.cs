using UnityEngine;

public class SpaceStationCreator : MonoBehaviour
{

    public Camera creatorCamera;
    public LayerMask mountLayer;
    public GameObject[] allModules;
    public GameObject spaceStation;

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
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, mountLayer))
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
        currentModule.transform.position = creatorCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 6f));
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
        GameObject copiedModule = Instantiate(currentModule, spaceStation.transform);
        copiedModule.transform.position = mount.transform.position;
        copiedModule.transform.rotation = calculateModuleRotation(mount);
        mount.SetMountedModule(copiedModule);
        creatorCamera.GetComponent<CameraBehaviour>().SetCameraOffset(copiedModule.transform.position.magnitude);
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
