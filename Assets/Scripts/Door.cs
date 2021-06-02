using UnityEngine;

public class Door : MonoBehaviour
{
    float t;
    [SerializeField]
    private float doorOpen;

    [SerializeField] private float DoorOpenAngle = -90f;
    
    void Update()
    {
        doorOpen = Mathf.Lerp(0, DoorOpenAngle, t);
        transform.rotation = Quaternion.Euler(-90f,doorOpen,0f);
         t+= .5f * Time.deltaTime;
    }
}
