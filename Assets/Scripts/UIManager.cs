using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] RectTransform UIScaler;
    [SerializeField]
    GameObject pauseMenu,
        settingsMenu,
        logo,
        bottomBar,
        finger;


    [SerializeField]
    Button btnSettingsMainMenu,
        btnLightMode,
        btnBack,
        btnStuck;

    [Header("GameOverBtns")]
    [Space]
    [SerializeField]
    Button btnSettingsGameOver;
    [SerializeField]
    Button btnRestart;

    [Header("PauseBtns")]
    [Space]
    [SerializeField]
    Button btnPause;
    [SerializeField]
    Button btnMainMenu,
        btnCustomize,
        btnLeaderBoards,
        btnResume;

    [Space]
    [SerializeField] Text txtStar;
    [SerializeField] int starPoint = 0;
    [SerializeField] TMP_Text txtScorePoint;
    public int scorePoint = 0;
    [SerializeField] TMP_Text bestScorePoint;
    [SerializeField] Text nameOfMenu;

    [HideInInspector] public GameField gameField;
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
            background.color = lightColorBackground;
        }
        else
        {
            background.color = darkColorBackground;
        }

        UIScaler.sizeDelta = new Vector2(Screen.width - Screen.width * (ScreenBounds.bounds.x - 2.7f) / ScreenBounds.bounds.x, UIScaler.sizeDelta.y);

        SetupUIMenu();

        #region btns
        btnLightMode.onClick.AddListener(() =>
        {
            gameField.ChangeLightMode();
            ChangeLightMode();
        });
        btnStuck.onClick.AddListener(() =>
        {
            HideBtnStuck();
            BallMovement ball = FindObjectOfType<BallMovement>();
            ball.Unstuck();
        });
        btnPause.onClick.AddListener(PauseGame);
        btnRestart.onClick.AddListener(GameManager.Instance.RestartGame);
        btnSettingsGameOver.onClick.AddListener(Settings);
        btnSettingsMainMenu.onClick.AddListener(Settings);
        btnBack.onClick.AddListener(Back);
        btnResume.onClick.AddListener(ResumeGame);
        btnMainMenu.onClick.AddListener(GameManager.Instance.GoToMainMenu);
        #endregion

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
        GameManager.Instance.currentState = GameStates.Menu;
    }
    void SetupUIPlay()
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
        txtScorePoint.text = scorePoint.ToString();
        GameManager.Instance.currentState = GameStates.Play;
    }

    public void SetupUIGameOver()
    {
        GameManager.Instance.currentState = GameStates.GameOver;
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
    void PauseGame()
    {
        pauseMenu.SetActive(true);
        background.gameObject.SetActive(true);
        btnPause.gameObject.SetActive(false);
        GameManager.Instance.currentState = GameStates.Pause;
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        pauseMenu.SetActive(false);
        background.gameObject.SetActive(false);
        btnPause.gameObject.SetActive(true);
        GameManager.Instance.currentState = GameStates.Play;
        Time.timeScale = 1;
    }

    void Settings()
    {
        settingsMenu.SetActive(true);
        background.gameObject.SetActive(true);
        settingsBackground.gameObject.SetActive(true);
        nameOfMenu.text = "SETTINGS";
        nameOfMenu.gameObject.SetActive(true);
        btnBack.gameObject.SetActive(true);
        if (GameManager.Instance.currentState == GameStates.Menu)
        {
            btnSettingsMainMenu.gameObject.SetActive(false);
            btnLightMode.gameObject.SetActive(false);
        }
    }
    void Back()
    {
        if (GameManager.Instance.currentState == GameStates.Menu)
        {
            btnSettingsMainMenu.gameObject.SetActive(true);
            btnLightMode.gameObject.SetActive(true);
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

    void HideBtnStuck()
    {
        btnStuck.gameObject.SetActive(false);
    }

    public void ChangeLightMode()
    {
        Debug.Log("ChangeLightMode");
        if ((PlayerPrefs.GetInt("LightMode") != 0))
            background.color = lightColorBackground;
        else
            background.color = darkColorBackground;
    }
    public void AddStarPoints()
    {
        starPoint++;
        txtStar.text = starPoint.ToString();
    }
    public void AddScorePoints(int scoreToAdd)
    {
        scorePoint += scoreToAdd;
        txtScorePoint.text = scorePoint.ToString();
    }
    private void Update()
    {
#if UNITY_EDITOR //To Check on different screens
        UIScaler.sizeDelta = new Vector2(Screen.width - Screen.width * (ScreenBounds.bounds.x - 2.7f) / ScreenBounds.bounds.x, UIScaler.sizeDelta.y);
        if (GameManager.Instance.es.currentSelectedGameObject == null
            || (GameManager.Instance.es.currentSelectedGameObject != null && GameManager.Instance.es.currentSelectedGameObject.layer != 5))
        {
            if (Input.GetMouseButtonDown(0) && GameManager.Instance.currentState == GameStates.Menu)
            {
                SetupUIPlay();
            }
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (GameManager.Instance.es.currentSelectedGameObject == null
            || (GameManager.Instance.es.currentSelectedGameObject != null && GameManager.Instance.es.currentSelectedGameObject.layer != 5))
        {
            if (Input.touchCount > 0 && GameManager.Instance.currentState == GameStates.Menu)
            {
                SetupUIPlay();
            }
        }
#endif
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("star", starPoint);
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
