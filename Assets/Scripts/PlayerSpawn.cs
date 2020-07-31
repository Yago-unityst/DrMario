using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject marioPrefab;
    public Transform spawnPoint;

    void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        Instantiate(marioPrefab, spawnPoint.position, Quaternion.identity, transform);
    }
}
