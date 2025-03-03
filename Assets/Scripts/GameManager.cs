using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float cameraOffset;

    void Awake(){
        Instance = this;
    }

    public void SetCameraOffset(float newOfsset){
        if(newOfsset > cameraOffset)
            cameraOffset = newOfsset;
    }

    public float GetCameraOffset(){
        return cameraOffset;
    }

}
