using UnityEngine;

public class CanvasScorePosition : MonoBehaviour //Set positions and size to score and bestScore on the World Space
{
    [SerializeField] RectTransform rtScore;
    [SerializeField] RectTransform rtBestScore;
    RectTransform canvas;
    void Start()
    {
        canvas = GetComponent<RectTransform>();
        canvas.transform.position = ScreenBounds.screenCenter;
        canvas.sizeDelta = new Vector2(Screen.width - Screen.width * (ScreenBounds.bounds.x - 2.7f) / ScreenBounds.bounds.x, canvas.sizeDelta.y);
        rtScore.sizeDelta = new Vector2(1.25f, 1.25f);
        rtScore.position = new Vector3(rtScore.position.x, rtScore.position.y + 2.4f, -1f);
        rtBestScore.sizeDelta = new Vector2(0.625f, 0.625f);
        rtBestScore.position = new Vector3(rtBestScore.position.x, rtBestScore.position.y + 3.2f, -1f);
    }
}
