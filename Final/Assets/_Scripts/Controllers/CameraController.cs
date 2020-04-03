using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // these field hold specific aspects of the camera attributes to obtain a proper camera following script.
    public Transform target;
    public Vector3 offset;
    public float pitch = 2f;
    public float zoomspeed = 4f;
    public float maxZoom = 25f;
    public float minZoom = 5f;
    public float yawSpeed = 200f;
    public float currentYaw = 0f;


    private float currentZoom = 10f;

    void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomspeed;// handles zoom functions from the scroll wheel.
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom); // restrictions to how much you can zoom in or out.
        currentYaw += Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime; // how fast the camera rotates.

    }

    void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom; // move the camera to the given position based on the player.
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, currentYaw); // handles left and right camera rotation.
    }
    
}
