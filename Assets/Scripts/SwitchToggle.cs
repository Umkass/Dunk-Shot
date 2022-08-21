using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform rtCheckmark;
    [SerializeField] Text label;
    Vector2 checkmarkPos;
    Toggle toggle;
    Color onColor = new Color(0.9921569f, 0.572549f, 0.0627451f);
    Color offColor = new Color(0.6705883f, 0.6705883f, 0.6705883f);

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        checkmarkPos = rtCheckmark.anchoredPosition;
        toggle.onValueChanged.AddListener(Switch);
        Switch(toggle.isOn);
    }

    void Switch(bool isOn)
    {
        rtCheckmark.anchoredPosition = isOn? checkmarkPos : checkmarkPos * -1;
        toggle.targetGraphic.color = isOn ? onColor : offColor;
        label.alignment = isOn ? TextAnchor.MiddleLeft : TextAnchor.MiddleRight;
        label.text = isOn ? "ON": "OFF";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
