using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates
{
    Menu,
    Play,
    Pause,
    GameOver
}
public class GameManager : Singleton<GameManager>
{
    public GameStates state = GameStates.Menu;

    public void RestartGame()
    {
        state = GameStates.Menu;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        state = GameStates.Menu;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
