using UnityEngine;

public class InputHandler : MonoBehaviour
{
    BallMovement ball;
    TrajectoryDrawer trajectoryDrawer;

    [SerializeField] float pushForce = 15f;
    [SerializeField] Vector2 startPoint;
    [SerializeField] Vector2 endPoint;
    [SerializeField] Vector3 direction;
    [SerializeField] Vector2 force;
    [SerializeField] float distance;
    float maxForce = 12.5f;
    float minForceToPush = 5f;
    LayerMask layerMask = 1 << 8;

    public bool isDrugging = false;

    void Awake()
    {
        ball = GetComponent<BallMovement>();
        trajectoryDrawer = GetComponent<TrajectoryDrawer>();
    }
    void Update()
    {
#if UNITY_EDITOR
        if (GameManager.Instance.es.currentSelectedGameObject == null ||
            (GameManager.Instance.es.currentSelectedGameObject != null && GameManager.Instance.es.currentSelectedGameObject.layer != 5))
        {
            if (ball.canPush && GameManager.Instance.currentState == GameStates.Play)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnDragStart(Input.mousePosition);
                }
                if (Input.GetMouseButton(0))
                {
                    OnDrag(Input.mousePosition);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    OnDragEnd();
                }
            }
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (GameManager.Instance.es.currentSelectedGameObject == null ||
    (GameManager.Instance.es.currentSelectedGameObject != null && GameManager.Instance.es.currentSelectedGameObject.layer != 5))
        {
            if (ball.canPush && GameManager.Instance.currentState == GameStates.Play)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        OnDragStart(touch.position);
                    }
                    if (touch.phase == TouchPhase.Moved)
                    {
                        OnDrag(touch.position);
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        OnDragEnd();
                    }
                }
            }
        }
#endif
    }

    void OnDragStart(Vector3 position)
    {
        isDrugging = true;
        ball.MakeKinematic();
        startPoint = Camera.main.ScreenToViewportPoint(position);
    }
    void OnDrag(Vector3 position)
    {
        endPoint = Camera.main.ScreenToViewportPoint(position);
        distance = Vector2.Distance(startPoint, endPoint) * 3f;
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;
        if (force.magnitude > maxForce)
        {
            force = force.normalized * maxForce;
        }
        ball.currentHoop.GetComponentInChildren<Net>().StretchNet(force.magnitude, maxForce); // stretch net
        trajectoryDrawer.ShowTrajectory(force.magnitude - 5f); //if force.magnitude >=5 -> ShowTrajectory with alpha, if >=6 -> alpha = 1
        ball.currentHoop.gameObject.transform.up = direction; //rotate hoop to direction of force
        Vector2 forceReflection;
        RaycastReflecion(force, force.magnitude, out forceReflection);
        trajectoryDrawer.UpdateDots(ball.transform.position, force, forceReflection * force.magnitude);
    }
    void OnDragEnd()
    {
        isDrugging = false;
        if (force.magnitude >= minForceToPush)
        {
            ball.transform.parent = null;
            ball.MakeNonKinematic();
            ball.Push(force);
        }
        ball.currentHoop.GetComponentInChildren<Net>().SetDefaultTransform();
        trajectoryDrawer.HideTrajectory();
    }

    void RaycastReflecion(Vector2 directionForce, float distance, out Vector2 forceReflection) //reflects the force vector if it hits a wall
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, layerMask);
        Debug.DrawRay(transform.position, direction, Color.red);
        if (hit.collider != null)
        {
            forceReflection = Vector2.Reflect(direction, hit.normal);
        }
        else
        {
            forceReflection = Vector2.zero;
        }
    }
}
