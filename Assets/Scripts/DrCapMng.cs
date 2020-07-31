using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrCapMng : MonoBehaviour
{
    public int gridPosX;
    public int gridPosY;
    public Material[] capColors;
    ScoreCtrl scoreCtrl;
    void Start()
    {
        GetComponent<MeshRenderer>().material = capColors[Random.Range(0, capColors.Length)];
        scoreCtrl = GameObject.FindGameObjectWithTag("LvlBox").GetComponent<ScoreCtrl>();
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 60) * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            //Se borra también de la cuadrícula de la zona para que no reaparezca cuando se carga la partida
            gameObject.GetComponentInParent<ZoneGeneration>().zoneSurface[gridPosX, gridPosY] = 0;
            scoreCtrl.AddPoint(1);
        }
    }
}
