using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCameraRotator : MonoBehaviour
{
    public Camera mainCamera;

    void Update()
    {
        if (transform.position.y < 2.5f)
        {
            transform.Rotate (new Vector3 (0f, -45f, 0f) * Time.deltaTime);
            transform.Translate(0, Time.deltaTime /3, 0);
            mainCamera.transform.Translate(Vector3.forward * Time.deltaTime / 2);
        }
    }
}
