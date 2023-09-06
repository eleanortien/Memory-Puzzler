using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    public Vector2 minCameraChange;
    public Vector2 maxCameraChange;
    public Vector3 playerChange;
    public float fieldOfViewChange;
    private CameraMovement cam;
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
        
    }


    private void OnTriggerEnter2D (Collider2D roomCollision)
    {
        if (roomCollision.CompareTag("Player"))
        {
            cam.minPosition.x += minCameraChange.x;
            cam.minPosition.y += minCameraChange.y;
            cam.maxPosition.x += maxCameraChange.x;
            cam.maxPosition.y += maxCameraChange.y;
            if (fieldOfViewChange != 0)
            {
                Camera.main.fieldOfView = fieldOfViewChange;
            }
            roomCollision.transform.position += playerChange;
        }
    }
}
