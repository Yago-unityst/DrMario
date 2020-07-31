using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstMover : MonoBehaviour
{
    public float obstMovSpeed = 0.2f;
    public float obstMovRange = 6f;
    float obstMovOffset = 0f;
    // Start is called before the first frame update
    void Start()
    {
        obstMovOffset = Random.Range(0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Sin(Time.time * obstMovSpeed + obstMovOffset) * obstMovRange;
        transform.position = pos;
    }
}
