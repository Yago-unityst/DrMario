using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavManage : MonoBehaviour
{
    public NavMeshAgent pJNav;
    public Animator pJAnimr;
    public Vector3 pJWaypoint;
    public float speed = 6f;
    public float turnSpeed = 1f;
    float pJRad;

    void Start()
    {
        pJNav = GetComponent<NavMeshAgent>();
        pJAnimr = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseClick = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouseClick);
            RaycastHit mouseClickWP;
            if (Physics.Raycast(castPoint, out mouseClickWP, Mathf.Infinity))
                pJNav.SetDestination(mouseClickWP.point);
        }
        pJAnimr.SetFloat("Blend", pJNav.velocity.magnitude);
    }
}
