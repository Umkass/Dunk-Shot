using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    SwitchToggle toggleSounds,
        toggleVibration,
        toggleNightMode;

    [SerializeField] bool isSoundOn = true;
    [SerializeField] bool isVibrationOn = true;
    [SerializeField] bool isNightModeOn = false;

    private void Start()
    {
        Debug.Log("Start");
        if (PlayerPrefs.HasKey("isSoundOn"))
        {
            isSoundOn = (PlayerPrefs.GetInt("isSoundOn") != 0);
        }
        else
        {
            PlayerPrefs.SetInt("isSoundOn", (isSoundOn ? 1 : 0));
        }

        if (PlayerPrefs.HasKey("isVibrationOn"))
        {
            isVibrationOn = (PlayerPrefs.GetInt("isVibrationOn") != 0);
        }
        else
        {
            PlayerPrefs.SetInt("isVibrationOn", (isVibrationOn ? 1 : 0));
        }

        if (PlayerPrefs.HasKey("LightMode"))
        {
            isNightModeOn = (PlayerPrefs.GetInt("LightMode") == 0);
        }
        else
        {
            PlayerPrefs.SetInt("LightMode", (isNightModeOn ? 0 : 1));
        }

        toggleSounds.toggle.onValueChanged.AddListener((bool isOn) =>
        {
            isSoundOn = isOn;
            PlayerPrefs.SetInt("isSoundOn", (isSoundOn ? 1 : 0));
            GameManager.Instance.MuteSounds(!isOn);
        });
        toggleVibration.toggle.onValueChanged.AddListener((bool isOn) =>
        {
            isVibrationOn = isOn;
            PlayerPrefs.SetInt("isVibrationOn", (isVibrationOn ? 1 : 0));
        });
        toggleNightMode.toggle.onValueChanged.AddListener((bool isOn) =>
        {
                isNightModeOn = isOn;
            if(isNightModeOn != (PlayerPrefs.GetInt("LightMode") == 0))
            {
                PlayerPrefs.SetInt("LightMode", (isNightModeOn ? 0 : 1));
                UIManager.Instance.gameField.ChangeLightMode();
                UIManager.Instance.ChangeLightMode();
            }
        });

        toggleSounds.toggle.isOn = isSoundOn;
        toggleVibration.toggle.isOn = isVibrationOn;
        toggleNightMode.toggle.isOn = isNightModeOn;

        GameManager.Instance.MuteSounds(!isSoundOn);
    }
    private void OnEnable()
    {
        Debug.Log("OnEnable");
        if (PlayerPrefs.HasKey("isSoundOn"))
        {
            isSoundOn = (PlayerPrefs.GetInt("isSoundOn") != 0);
        }
        else
        {
            PlayerPrefs.SetInt("isSoundOn", (isSoundOn ? 1 : 0));
        }

        if (PlayerPrefs.HasKey("isVibrationOn"))
        {
            isVibrationOn = (PlayerPrefs.GetInt("isVibrationOn") != 0);
        }
        else
        {
            PlayerPrefs.SetInt("isVibrationOn", (isVibrationOn ? 1 : 0));
        }

        if (PlayerPrefs.HasKey("LightMode"))
        {
            isNightModeOn = (PlayerPrefs.GetInt("LightMode") == 0);
        }
        else
        {
            PlayerPrefs.SetInt("LightMode", (isNightModeOn ? 0 : 1));
        }
        toggleSounds.toggle.isOn = isSoundOn;
        toggleVibration.toggle.isOn = isVibrationOn;
        toggleNightMode.toggle.isOn = isNightModeOn;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("isSoundOn", (isSoundOn ? 1 : 0));
        PlayerPrefs.SetInt("isVibrationOn", (isVibrationOn ? 1 : 0));
        PlayerPrefs.SetInt("LightMode", (isNightModeOn ? 0 : 1));
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("isSoundOn", (isSoundOn ? 1 : 0));
        PlayerPrefs.SetInt("isVibrationOn", (isVibrationOn ? 1 : 0));
        PlayerPrefs.SetInt("LightMode", (isNightModeOn ? 0 : 1));
    }
}
