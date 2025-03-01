using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUI : MonoBehaviour
{
    private TMP_Text powerCount;

    void Start(){
        powerCount = this.GetComponent<TMP_Text>();
        powerCount.text = "0";
    }

    void Update(){
        UpdatePowerCount();
    }

    private void UpdatePowerCount(){
        PowerManager powerManager = GameObject.Find("PowerManager").GetComponent<PowerManager>();
        powerCount.text = powerManager.GetPower().ToString();
    }
}
