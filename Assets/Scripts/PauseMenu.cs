using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button btnMainMenu,
        btnCustomize,
        btnLeaderBoards,
        btnResume;

    void Awake()
    {
        btnResume.onClick.AddListener(ResumeGame);
        btnMainMenu.onClick.AddListener(GameManager.Instance.GoToMainMenu);
    }

    void ResumeGame()
    {
        GameManager.Instance.state = GameStates.Play;
        Time.timeScale = 1;
        UIManager.Instance.background.gameObject.SetActive(false);
        gameObject.SetActive(false);
        UIManager.Instance.btnPause.gameObject.SetActive(true);
    }
}
