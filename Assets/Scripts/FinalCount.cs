using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCount : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMesh>().text += ScoreCtrl.drCaps;
    }
}
