using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolManage : MonoBehaviour
{
    Animator nPCAnimr;
    NavMeshAgent nPCAgent;
    GameObject[] wayPointObjects;
    Vector3[] wayPoints;
    public string zone;
    int wPArrayPos;
    float waitTime;
    float timer;
    Quaternion lookRotation;

    void Start()
    {
        waitTime = Random.Range(3, 7);
        timer = 0;
        nPCAgent = GetComponent<NavMeshAgent>();
        nPCAnimr = GetComponent<Animator>();
        wayPointObjects = GameObject.FindGameObjectsWithTag("DrCap"+zone);
        wayPoints = new Vector3[wayPointObjects.Length];
        for (int i = 0; i < wayPointObjects.Length; i++)
        {
            // Queremos los waypoints en las posiciones donde han aparecido las cápsulas, pero a
            // la misma altura que el virus. Los WP persistírán aunque el Dr Mario las recoja.
            wayPoints[i] = wayPointObjects[i].transform.position - new Vector3(0f, 0.5f, 0f);
        }
        // Inicia de camino hacia una primera cápsula. No queremos que se quede en la misma
        // posición, sino cerca, así que le restamos 2 metros basados en el forward.
        wPArrayPos = Random.Range(0, wayPoints.Length);
        nPCAgent.SetDestination(wayPoints[wPArrayPos] - (transform.forward * 2));
    }

    void Update()
    {
        nPCAnimr.SetFloat("Blend", nPCAgent.velocity.magnitude);
        // Le señalamos un nuevo destino antes de que llegue. Como al menos van a pasar 3 segundos
        // desde este punto y no va a recibir el siguiente SetDestination hasta que termine la
        // espera, esto ayuda a que los virus no se estorben unos a otros
        if (nPCAgent.remainingDistance < 1.5) WaitNGo();
    }
    void WaitNGo()
    {
        if (timer == 0)
        {
            // Selecciona la siguiente cápsula hacia la que irá. Se calcula la rotación para mirar
            // hacia esa nueva posición.
            wPArrayPos = Random.Range(0, wayPoints.Length);
            lookRotation = Quaternion.LookRotation(wayPoints[wPArrayPos] - transform.position).normalized;
        }
        timer += Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, timer * 0.3f);
        if (timer >= waitTime)
        {
            // Como se ha girado hacia el waypoint, en este caso se para siempre a dos metros antes
            // de llegar desde donde venía. A diferencia del start, cuando estaba mirando a su posición
            // original de instanciación
            nPCAgent.SetDestination(wayPoints[wPArrayPos] - (transform.forward * 2));
            timer = 0;
            waitTime = Random.Range(3, 7);
        }
    }
}