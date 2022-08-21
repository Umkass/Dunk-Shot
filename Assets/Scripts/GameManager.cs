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
    [SerializeField] AudioSource audioSource;

    [Header("Clips")]
    [SerializeField] AudioClip hoopClip;
    [SerializeField] AudioClip wallClip;
    [SerializeField] AudioClip netClip;
    [SerializeField] AudioClip starClip;

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

    public void HoopSound()
    {
        audioSource.clip = hoopClip;
        audioSource.Play();
    }
    public void StarSound()
    {
        audioSource.clip = starClip;
        audioSource.Play();
    }
    public void NetSound()
    {
        audioSource.clip = netClip;
        audioSource.Play();
    }
    public void WallSound()
    {
        audioSource.clip = wallClip;
        audioSource.Play();
    }
}
