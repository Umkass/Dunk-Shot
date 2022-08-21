using UnityEngine;
using UnityEngine.EventSystems;
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
    public GameStates currentState = GameStates.Menu;
    [SerializeField] public EventSystem es;

    public void RestartGame()
    {
        currentState = GameStates.Menu;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        currentState = GameStates.Menu;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
