using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCtrl : MonoBehaviour
{
    public static int drCaps;
    public Text scoreText;
    public void AddPoint(int n)
    {
        drCaps += n;
        UpdateScore();
    }
    public void RemovePoint(int n)
    {
        drCaps -= n;
        UpdateScore();
    }
    public void UpdateScore()
    {
        if (drCaps < 0) drCaps = 0;
        scoreText.text = (": " + drCaps);
    }
}
