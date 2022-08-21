using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    #region Field Declarations

    public static Vector2 bounds;
    private static float spriteBorder = .5f;

    public static float left { get { return -bounds.x + spriteBorder; } }
    public static float right { get { return bounds.x - spriteBorder; } }
    public static float top { get { return bounds.y - spriteBorder; } }
    public static float bottom { get { return -bounds.y + spriteBorder; } }

    public static Vector2 screenCenter;

    #endregion
    private void Awake()
    {
        UpdateScreenBounds();
    }
    private static Vector2 GetScreenBounds()
    {
        Vector3 screenVector = new Vector2(Screen.width, Screen.height);
        screenVector.z = 10f; // camera offset
        screenCenter = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width/2, Screen.height/2));
        return Camera.main.ScreenToWorldPoint(screenVector);
    }

    public static void UpdateScreenBounds()
    {
        bounds = GetScreenBounds();
    }

    public static bool AboveMiddleScreen(Vector2 position)
    {
        return (position.y > screenCenter.y);
    }
    public static bool BelowMiddleScreen(Vector2 position)
    {
        return (position.y < screenCenter.y && position.y > bottom/2);
    }

    public static bool OutOfBoundsScreen(Vector2 position)
    {
        float x = Mathf.Abs(position.x);
        float y = Mathf.Abs(position.y);

        return (x > bounds.x || y > bounds.y);
    }

    private void Update()
    {
        UpdateScreenBounds();
    }
}
