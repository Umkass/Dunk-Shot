using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Camera cam;
    Vector3 offset;
    BallMovement target;
    InputHandler inputHandler;
    void Awake()
    {
        cam = Camera.main;
    }
    void Start()
    {
        target = FindObjectOfType<BallMovement>();
        if (target != null)
            inputHandler = target.GetComponent<InputHandler>();
        offset = new Vector3(target.transform.position.x, target.transform.position.y - 2.72f, target.transform.position.z);
    }

    void Update()
    {
        if (GameManager.Instance.currentState == GameStates.Play)
        {
            if (target != null)
            {
                if (!inputHandler.isDrugging && (ScreenBounds.AboveMiddleScreen(target.transform.position) || ScreenBounds.BelowMiddleScreen(target.transform.position)))
                    FollowTarget();
            }
            else
            {
                target = FindObjectOfType<BallMovement>();
                if (target != null)
                    inputHandler = target.GetComponent<InputHandler>();
            }
        }
    }

    void FollowTarget()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(cam.transform.position.x, target.transform.position.y - offset.y, cam.transform.position.z), Time.deltaTime);
    }
}
