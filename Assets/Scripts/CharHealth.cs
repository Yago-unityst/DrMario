using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharHealth : MonoBehaviour
{
    int maxHealth;
    int currentHealth;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite spentHeart;
    GameObject [] hearts;
    GameObject lvlMng;

    void Start()
    {
        maxHealth = 6;
        if (GameState.newGame == true) currentHealth = maxHealth;
        else currentHealth = GameState.health;
        lvlMng = GameObject.FindGameObjectWithTag("LvlBox");
        hearts = GameObject.FindGameObjectsWithTag("UIHeart");
        SetUIHearts();
    }

    public void TakeDamage()
    {
        currentHealth--;
        SetUIHearts();
        if (currentHealth <= 0) CharDies();
    }

    void SetUIHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if ((i + 1) * 2 <= currentHealth) hearts[i].GetComponent<Image>().sprite = fullHeart;
            else if (((i + 1) * 2) -1 == currentHealth) hearts[i].GetComponent<Image>().sprite = halfHeart;
            else hearts[i].GetComponent<Image>().sprite = spentHeart;
        }
    }
    void CharDies()
    {
        // To Do: animación, música

        lvlMng.GetComponent<ScoreCtrl>().RemovePoint(3);
        lvlMng.GetComponent<PlayerSpawn>().Spawn();
        Destroy(gameObject);
    }
}
