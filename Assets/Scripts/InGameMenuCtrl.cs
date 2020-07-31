using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuCtrl : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.Find("InGameMenu").gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        transform.Find("InGameMenu").gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void SaveGame()
    {
        GameState game = new GameState();
        game.Save();
    }
    public void BackToTitle()
    {
        Resume();
        SceneManager.LoadScene(0);
    }
}
