using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    SpriteRenderer srField;
    [SerializeField] SpriteRenderer[] srBounds;
    Color lightColorField = new Color(0.9137256f, 0.9137256f, 0.9137256f);
    Color lightColorBounds = new Color(0.7725491f, 0.7725491f, 0.7725491f);
    Color darkColorField = new Color(0.2235294f, 0.2235294f, 0.2235294f);
    Color darkColorBounds = new Color(0.1568628f, 0.1568628f, 0.1568628f);
    bool isLight = true;
    // Start is called before the first frame update
    void Start()
    {
        srField = GetComponent<SpriteRenderer>();
        if (PlayerPrefs.HasKey("LightMode"))
        {
            isLight = (PlayerPrefs.GetInt("LightMode") != 0);
        }
        else
        {
            PlayerPrefs.SetInt("LightMode", (isLight ? 1 : 0));
        }
        if (isLight)
        {
            srField.color = lightColorField;
            foreach (var item in srBounds)
            {
                item.color = lightColorBounds;
            }
        }
        else
        {
            srField.color = darkColorField;
            foreach (var item in srBounds)
            {
                item.color = darkColorBounds;
            }
        }
    }

    public void ChangeLightMode()
    {
        isLight = !isLight;
        PlayerPrefs.SetInt("LightMode", (isLight ? 1 : 0));
        if (isLight)
        {
            srField.color = lightColorField;
            foreach (var item in srBounds)
            {
                item.color = lightColorBounds;
            }
        }
        else
        {
            srField.color = darkColorField;
            foreach (var item in srBounds)
            {
                item.color = darkColorBounds;
            }
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("LightMode", (isLight ? 1 : 0));
    }
}
