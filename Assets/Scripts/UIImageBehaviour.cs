using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIImageBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private float UIBorderPosition;
    [SerializeField]
    private GameObject StationModule;
    [SerializeField]
    private float cost;
    [SerializeField]
    private bool generateEnergy;

    private bool canSpawnModule;
    private bool canAffordModule;
    private Vector3 initialPosition;
    private PowerManager powerManager;

    void Start()
    {
        powerManager = GameObject.Find("PowerManager").GetComponent<PowerManager>();
        this.initialPosition = transform.position;
        this.canSpawnModule = true;
        this.canAffordModule = true;
    }
    void Update()
    {
        CheckOutOfBorder();
        CheckAfford();
    }

    public void OnBeginDrag(PointerEventData _EventData)
    {
        //Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData _EventData)
    {
        MoveUIItem();
    }

    public void OnEndDrag(PointerEventData _EventData)
    {
        this.canSpawnModule = true;
    }

    private void MoveUIItem()
    {
        transform.position = Input.mousePosition;
    }

    private void CheckOutOfBorder()
    {
        if (transform.position.x >= this.UIBorderPosition) return;

        SpawnModule();
        transform.position = this.initialPosition;

    }

    private void SpawnModule()
    {
        if (!this.canSpawnModule) return;
        if (!this.canAffordModule) return;

        Vector3 mousePositionOnScreen = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        GameObject module = Instantiate(this.StationModule, mousePositionOnScreen, Quaternion.identity);
        //TODO: Make the instantiated object to drag
        //module.GetComponent<ModuleBehaviour>().ModuleSelection();
        this.canSpawnModule = false;
        ChangePower();
    }

    private void CheckAfford()
    {
        if(generateEnergy){
            this.canAffordModule = true;
        }
        else{
            this.canAffordModule = powerManager.CanAfford(this.cost);
        }
    }
    private void ChangePower()
    {
        if (generateEnergy)
        {
            powerManager.IncreasePower(this.cost);
        }
        else
        {
            powerManager.DecreasePower(this.cost);
        }
    }
}
