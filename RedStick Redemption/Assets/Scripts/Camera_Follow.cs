using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform player;
    public float cameraDistance = 30.0f;
    public Camera camera;
    float deltacumul;

    private void Awake()
    {
        camera = GetComponent<UnityEngine.Camera>();
        camera.orthographicSize = ((Screen.height / 2) / cameraDistance);
    }

    private void FixedUpdate()
    {
        HandleCamera();

    }

    private void HandleCamera()
    {

        float minCameraX = -23.50f;
        float maxCameraX = 2000 - minCameraX;

        // camera.transform.position = new Vector3(Mathf.Min(maxCameraX, Mathf.Max(transform.position.x, minCameraX)), Mathf.Min(maxCameraY, Mathf.Max(camera.transform.position.y, minCameraY)), transform.position.z);

        deltacumul += Time.deltaTime;

        Vector3 posTemp = camera.transform.position;

        posTemp.x += (player.transform.position.x - camera.transform.position.x) * 10f * Time.deltaTime;


        camera.transform.position = new Vector3(Mathf.Min(maxCameraX, Mathf.Max(posTemp.x, minCameraX)),
            transform.position.y,
            transform.position.z);
    }
}
