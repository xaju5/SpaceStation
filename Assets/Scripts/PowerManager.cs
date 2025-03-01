using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager: MonoBehaviour
{
    [SerializeField]
    private float power;

    public float GetPower(){
        return this.power;
    }

    public void SetPower(float power){
        this.power = power;
    }

    public void DecreasePower(float cost){
        this.power -= cost;
    }

    public void IncreasePower(float cost){
        this.power += cost;
    }

    public bool CanAfford(float cost){
        return this.power >= cost;
    }

}
