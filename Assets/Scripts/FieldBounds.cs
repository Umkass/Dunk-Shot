using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBounds : MonoBehaviour
{
    public static Vector2 bounds;
    private static float offsetX = 0.7f;
    private static float offsetY = 4f;
    public static float left { get { return -bounds.x + offsetX; } }
    public static float right { get { return bounds.x - offsetX; } }
    public static float top { get { return bounds.y - offsetY; } }
    public static float bottom { get { return -bounds.y + offsetY; } }

    public static Vector2 fieldCenter;
    void Start()
    {
        UpdateFieldBounds();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFieldBounds();
    }

    void UpdateFieldBounds()
    {
        bounds = new Vector2(transform.position.x + transform.localScale.x/2, transform.position.y + transform.localScale.y/2);
        fieldCenter = transform.position;
        //Debug.Log(bounds);
        //Debug.Log(fieldCenter);
    }
    public static bool OutOfRightBounds(Vector2 position)
    {
        return (position.x > bounds.x);
    }
    public static bool OutOfLeftBounds(Vector2 position)
    {
        return (position.x < -bounds.x);
    }

    public static bool OnLeftSide(Vector2 position)
    {
        return (position.x > -bounds.x && position.x > 0);
    }
    public static bool OnRightSide(Vector2 position)
    {
        return (position.x < bounds.x && position.x <= 0);
    }
    public static Vector2 RandomTopLeftPosition()
    {
        float horizontalPosition = Random.Range(left, fieldCenter.x - 0.5f);
        float verticalPosition = Random.Range(fieldCenter.y, top);
        Debug.Log(new Vector2(horizontalPosition, verticalPosition));
        return new Vector2(horizontalPosition, verticalPosition);
    }
    public static Vector2 RandomTopRightPosition()
    {
        float horizontalPosition = Random.Range(fieldCenter.x + 0.5f, right);
        float verticalPosition = Random.Range(fieldCenter.y, top);
        Debug.Log(new Vector2(horizontalPosition, verticalPosition));
        return new Vector2(horizontalPosition, verticalPosition);
    }
    public static bool OutOfFieldBounds(Vector2 position)
    {
        float x = Mathf.Abs(position.x);
        float y = Mathf.Abs(position.y);

        return (x > bounds.x || y > bounds.y - offsetY/2);
    }
}
