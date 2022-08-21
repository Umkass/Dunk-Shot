using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    private BallMovement ball;
    [SerializeField] private TrajectoryDrawer trajectoryDrawer;
    [SerializeField] float pushForce = 15f;

    [SerializeField] Vector2 startPoint;
    [SerializeField] Vector2 endPoint;
    [SerializeField] Vector3 direction;
    [SerializeField] Vector2 force;
    [SerializeField] float distance;
    [SerializeField] EventSystem es;
    float maxForce = 12.5f;
    float minForceToPush = 5f;
    LayerMask layerMask = 1<< 8;
    public bool isDrugging = false;

    void Awake()
    {
        ball = GetComponent<BallMovement>();
        es = FindObjectOfType<EventSystem>();
        trajectoryDrawer = GetComponent<TrajectoryDrawer>();
    }
    void Update()
    {
        if(es.currentSelectedGameObject == null || (es.currentSelectedGameObject != null && es.currentSelectedGameObject.layer != 5))
        {
            if (ball.canPush && GameManager.Instance.state == GameStates.Play)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnDragStart();
                }
                if (Input.GetMouseButton(0))
                {
                    OnDrag();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    OnDragEnd();
                }
            }
        }
    }

    void OnDragStart()
    {
        isDrugging = true;
        ball.MakeKinematic();
        startPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }
    void OnDrag()
    {
        endPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint)*3f;
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;
        if (force.magnitude > maxForce)
        {
            force = force.normalized * maxForce;
        }
        ball.currentHoop.GetComponentInChildren<Net>().StretchNet(force.magnitude,maxForce); // натянуть сетку
        trajectoryDrawer.ShowTrajectory(force.magnitude-5f); //если >=5 начинаем показывать тректорию, если =6 прозрачность = 1
        ball.currentHoop.gameObject.transform.up = direction; //rotate hoop to direction
        Vector2 forceReflection;
        Raycast(force,force.magnitude, out forceReflection);
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
        ball.currentHoop.GetComponentInChildren<Net>().SetDefaultTransform(); //отпустить сетку
        trajectoryDrawer.HideTrajectory();
    }

    void Raycast(Vector2 directionForce,float distance, out Vector2 forceReflection)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, layerMask);
        Debug.DrawRay(transform.position, direction,Color.red);
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
