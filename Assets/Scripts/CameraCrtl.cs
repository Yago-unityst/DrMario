using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCrtl : MonoBehaviour
{
    Transform cameraTarget;
    Vector3 posDif;
    float distanceToTarget;
    GameObject[] CameraHooks;
    GameObject closestHook;
    void Update()
    {
        if (cameraTarget == null) cameraTarget = GameObject.FindGameObjectWithTag("Player").transform;
        else
        {
            transform.LookAt(cameraTarget);
            posDif = transform.position - cameraTarget.position;
            distanceToTarget = posDif.magnitude;
            if (distanceToTarget > 70)
            {
                CameraHooks = GameObject.FindGameObjectsWithTag("Hook");
                foreach (GameObject hook in CameraHooks)
                {
                    if ((hook.transform.position - cameraTarget.position).magnitude < distanceToTarget)
                    {
                        closestHook = hook;
                        distanceToTarget = (hook.transform.position - cameraTarget.position).magnitude;
                    }
                }
                transform.SetParent(closestHook.transform);
                transform.position = closestHook.transform.position;
            }
        }
    }
}
