using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDamage : MonoBehaviour
{
    CharHealth health;
    float timerMax = 1f;
    float timer = 1f;
    private void OnTriggerStay(Collider other)
    {
        timer += Time.deltaTime;
        if (timer >= timerMax && other.gameObject.tag == "Player")
        {
            health = other.gameObject.GetComponent<CharHealth>();
            health.TakeDamage();
            timer = 0;
        }
    }
}
