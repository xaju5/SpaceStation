using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private float cameraOffset;
    private Vector3 initialPosition;

    void Start()
    {
        cameraOffset = 0;
        initialPosition = transform.position;
    }

    void Update()
    {
        Vector3 offset = new Vector3(0, 0, cameraOffset);
        transform.position = initialPosition - offset;
    }

    public void SetCameraOffset(float newOfsset){
        if(newOfsset > cameraOffset){
            cameraOffset = newOfsset;
        }
    }

    public float GetCameraOffset(){
        return cameraOffset;
    }
}
