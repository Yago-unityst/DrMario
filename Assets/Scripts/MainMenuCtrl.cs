using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCtrl : MonoBehaviour
{
    public void PlayGame()
    {
        GameState.newGame = true;
        SceneManager.LoadScene(1);
    }
    public void LoadGame()
    {
        GameState.newGame = false;
        GameState game = new GameState();
        game.Load();
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
