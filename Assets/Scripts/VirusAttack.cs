using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusAttack : MonoBehaviour
{
    CharHealth health;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);
        Debug.Log(collision.collider.gameObject.tag);
        if (collision.collider.gameObject.tag == "Player")
        {
            health = collision.collider.GetComponent<CharHealth>();
            health.TakeDamage();
        }
    }
}
