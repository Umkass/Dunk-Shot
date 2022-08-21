using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] RectTransform UIScaler;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField]
    Button btnSettingsMainMenu,
        btnLightMode,
        btnBack,
        btnRestart,
        btnSettingsGameOver,
        btnStuck;
    public Button btnPause;
    [SerializeField]
    GameObject logo,
        bottomBar,
        finger;
    [SerializeField] EventSystem es;
    [SerializeField] Text txtStar;
    [SerializeField] int starPoint = 0;
    [SerializeField] TMP_Text txtScorePoint;
    public int scorePoint = 0;
    [SerializeField] TMP_Text bestScorePoint;
    [SerializeField] Text nameOfMenu;
    public Field field;

    public Image background;
    [SerializeField] Image settingsBackground;
    Color lightColorBackground = new Color(0.9137256f, 0.9137256f, 0.9137256f, 0.95f);
    Color darkColorBackground = new Color(0.2235294f, 0.2235294f, 0.2235294f, 0.95f);

    void Start()
    {
        if (PlayerPrefs.HasKey("star"))
        {
            starPoint = PlayerPrefs.GetInt("star");
            txtStar.text = starPoint.ToString();
        }
        else
        {
            PlayerPrefs.SetInt("star", starPoint);
            txtStar.text = starPoint.ToString();
        }

        if ((PlayerPrefs.GetInt("LightMode") != 0))
        {
            Debug.Log("lightColorBackground");
            background.color = lightColorBackground;
        }
        else
        {
            Debug.Log("darkColorBackground");
            background.color = darkColorBackground;
        }
        //if (ScreenBounds.bounds.x > 2.7f)
        UIScaler.sizeDelta = new Vector2(Screen.width - Screen.width * (ScreenBounds.bounds.x - 2.7f) / ScreenBounds.bounds.x, UIScaler.sizeDelta.y);

        SetupUIMenu();
        btnPause.onClick.AddListener(() =>
        {
            PauseGame();
        });

        btnLightMode.onClick.AddListener(() =>
        {
            field.ChangeLightMode();
            ChangeLightMode();
        });
        btnRestart.onClick.AddListener(() =>
        {
            GameManager.Instance.RestartGame();
        });
        btnStuck.onClick.AddListener(() =>
        {

            HideBtnStuck();
            BallMovement ball = FindObjectOfType<BallMovement>();
            ball.Unstuck();
        });
        btnSettingsGameOver.onClick.AddListener(() =>
        {
            Settings();
        });
        btnSettingsMainMenu.onClick.AddListener(() =>
        {
            Settings();
        });
        btnBack.onClick.AddListener(() =>
        {
            Back();
        });

    }

    void SetupUIMenu()
    {
        nameOfMenu.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        settingsBackground.gameObject.SetActive(false);
        pauseMenu.SetActive(false);
        btnBack.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(false);
        btnSettingsMainMenu.gameObject.SetActive(true);
        btnStuck.gameObject.SetActive(false);
        btnLightMode.gameObject.SetActive(true);
        btnPause.gameObject.SetActive(false);
        txtScorePoint.gameObject.SetActive(false);
        bestScorePoint.gameObject.SetActive(false);
        btnRestart.gameObject.SetActive(false);
        btnSettingsGameOver.gameObject.SetActive(false);
        logo.SetActive(true);
        bottomBar.SetActive(true);
        finger.SetActive(true);
        GameManager.Instance.state = GameStates.Menu;
    }
    public void SetupUIPlay()
    {
        btnSettingsMainMenu.gameObject.SetActive(false);
        btnLightMode.gameObject.SetActive(false);
        btnPause.gameObject.SetActive(true);
        txtScorePoint.gameObject.SetActive(true);
        bestScorePoint.gameObject.SetActive(false);
        btnRestart.gameObject.SetActive(false);
        btnSettingsGameOver.gameObject.SetActive(false);
        logo.SetActive(false);
        bottomBar.SetActive(false);
        finger.SetActive(false);
        GameManager.Instance.state = GameStates.Play;
        txtScorePoint.text = scorePoint.ToString();
    }
    void PauseGame()
    {
        pauseMenu.SetActive(true);
        btnPause.gameObject.SetActive(false);
        background.gameObject.SetActive(true);
        GameManager.Instance.state = GameStates.Pause;
        Time.timeScale = 0;
    }

    void Settings()
    {
        settingsMenu.SetActive(true);
        background.gameObject.SetActive(true);
        settingsBackground.gameObject.SetActive(true);
        nameOfMenu.text = "SETTINGS";
        nameOfMenu.gameObject.SetActive(true);
        btnBack.gameObject.SetActive(true);
        if (GameManager.Instance.state == GameStates.Menu)
        {
            btnSettingsMainMenu.gameObject.SetActive(false);
            btnLightMode.gameObject.SetActive(false);
        }
    }
    void Back()
    {
        if (GameManager.Instance.state == GameStates.Menu)
        {
            btnSettingsMainMenu.gameObject.SetActive(true);
            btnLightMode.gameObject.SetActive(true);
        }
        else if(GameManager.Instance.state == GameStates.GameOver)
        {

        }
        settingsMenu.SetActive(false);
        background.gameObject.SetActive(false);
        settingsBackground.gameObject.SetActive(false);
        nameOfMenu.gameObject.SetActive(false);
        btnBack.gameObject.SetActive(false);
    }
    public void ShowBtnStuck()
    {
        btnStuck.gameObject.SetActive(true);
    }

    public void HideBtnStuck()
    {
        btnStuck.gameObject.SetActive(false);
    }

    public void SetupUIGameOver()
    {
        GameManager.Instance.state = GameStates.GameOver;
        if (PlayerPrefs.HasKey("bestScore"))
        {
            if (PlayerPrefs.GetInt("bestScore") < scorePoint)
            {
                bestScorePoint.text = scorePoint.ToString();
                PlayerPrefs.SetInt("bestScore", scorePoint);
            }
            else
            {
                bestScorePoint.text = PlayerPrefs.GetInt("bestScore").ToString();
            }
        }
        else
        {
            bestScorePoint.text = scorePoint.ToString();
            PlayerPrefs.SetInt("bestScore", scorePoint);
        }
        bestScorePoint.gameObject.SetActive(true);
        btnPause.gameObject.SetActive(false);
        btnRestart.gameObject.SetActive(true);
        btnSettingsGameOver.gameObject.SetActive(true);
    }
    public void ChangeLightMode()
    {
        Debug.Log((PlayerPrefs.GetInt("LightMode") != 0));
        if ((PlayerPrefs.GetInt("LightMode") != 0))
        {
            Debug.Log("lightColorBackground");
            background.color = lightColorBackground;
        }
        else
        {
            Debug.Log("darkColorBackground");
            background.color = darkColorBackground;
        }
    }
    public void AddStarPoints()
    {
        starPoint++;
        txtStar.text = starPoint.ToString();
    }
    public void AddScorePoints(int scoreToAdd)
    {
        scorePoint +=scoreToAdd;
        txtScorePoint.text = scorePoint.ToString();
    }
    private void Update()
    {
        //Проверять на разных экранах
#if UNITY_EDITOR
        //ScreenBounds.UpdateScreenBounds();
        //if (ScreenBounds.bounds.x > 2.7f)
        UIScaler.sizeDelta = new Vector2(Screen.width - Screen.width * (ScreenBounds.bounds.x - 2.7f) / ScreenBounds.bounds.x, UIScaler.sizeDelta.y);
#endif
        if (es.currentSelectedGameObject == null || (es.currentSelectedGameObject != null && es.currentSelectedGameObject.layer != 5))
        {
            if (Input.GetMouseButtonDown(0) && GameManager.Instance.state == GameStates.Menu)
            {
                SetupUIPlay();
            }
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("star", starPoint);
        if (PlayerPrefs.HasKey("bestScore"))
        {
            if (PlayerPrefs.GetInt("bestScore") < scorePoint)
            {
                PlayerPrefs.SetInt("bestScore", scorePoint);
            }
        }
        else
        {
            PlayerPrefs.SetInt("bestScore", scorePoint);
        }
    }
}
