using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Teleport : MonoBehaviour
{
    public Transform teleTarget;
    public GameObject spawnParticlePrefab;
    NavMeshAgent pJAgent;
    PlayerSpawn spawn;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pJAgent = other.gameObject.GetComponent<NavMeshAgent>();
            pJAgent.Warp(teleTarget.position);
            pJAgent.SetDestination(teleTarget.position);
            spawn = GameObject.FindGameObjectWithTag("LvlBox").GetComponent<PlayerSpawn>();
            spawn.spawnPoint = teleTarget;
            Instantiate(spawnParticlePrefab, teleTarget.position, Quaternion.identity, transform);
        }
    }
}
