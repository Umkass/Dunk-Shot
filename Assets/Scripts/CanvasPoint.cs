using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPoint : MonoBehaviour
{
    [SerializeField] RectTransform point;
    [SerializeField] RectTransform bestScore;
    [SerializeField] RectTransform bestScoreName;
    RectTransform canvas;
    void Start()
    {
        canvas = GetComponent<RectTransform>();
        canvas.transform.position = ScreenBounds.screenCenter;
        canvas.sizeDelta = new Vector2(Screen.width - Screen.width * (ScreenBounds.bounds.x - 2.7f) / ScreenBounds.bounds.x, canvas.sizeDelta.y);
        point.sizeDelta = new Vector2(1.25f, 1.25f);
        point.position = new Vector3(point.position.x, point.position.y + 2.4f, -1f);
        bestScore.sizeDelta = new Vector2(0.625f, 0.625f);
        bestScore.position = new Vector3(bestScore.position.x, bestScore.position.y + 3.2f, -1f);
        //bestScoreName.sizeDelta = new Vector2(1.5f, 0.5f);
        //bestScoreName.position = new Vector3(bestScore.position.x, bestScore.position.y, -1f);
    }
}
