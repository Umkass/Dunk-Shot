using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryDrawer : MonoBehaviour
{
    [SerializeField] int dotsNumber;
    [SerializeField] GameObject dotsParent;
    [SerializeField] GameObject dotPrefab;
    [SerializeField] float dotSpacing;
    [SerializeField] [Range(0.75f, 1f)] float dotMinScale;
    [SerializeField] [Range(1f, 1.5f)] float dotMaxScale;

    TrajectoryDot[] dotsList;
    Vector2 dotPos;

    float timeStamp;
    // Start is called before the first frame update
    void Start()
    {
        HideTrajectory();
        PrepareDots();
    }

    void PrepareDots()
    {
        dotsList = new TrajectoryDot[dotsNumber];
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;
        float currentScale = dotMaxScale;
        float scaleToMinus = currentScale / dotsNumber;
        for (int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotPrefab, null).GetComponent<TrajectoryDot>();
            dotsList[i].transform.parent = dotsParent.transform;
            dotsList[i].transform.localScale = Vector3.one * currentScale;
            if (currentScale > dotMinScale)
                currentScale -= scaleToMinus;
        }
    }

    public void UpdateDots(Vector3 ballPos, Vector2 force, Vector2 forceReflection)
    {
        timeStamp = dotSpacing;
        for (int i = 0; i < dotsNumber; i++)
        {
            dotPos.x = ballPos.x + force.x * timeStamp;
            dotPos.y = (ballPos.y + force.y * timeStamp) - Physics2D.gravity.magnitude * Mathf.Pow(timeStamp, 2) /2f;
            if (forceReflection != Vector2.zero)
            {
                Vector2 ballPosX = new Vector2(ballPos.x, 0);
                if (FieldBounds.OutOfLeftBounds(dotPos))
                {
                    Vector2 hitPointX = new Vector2(-FieldBounds.bounds.x, 0);
                    float DistanceX = Vector2.Distance(hitPointX, ballPosX);
                    dotPos.x = (-FieldBounds.bounds.x - DistanceX) + forceReflection.x * timeStamp;
                }
                else if (FieldBounds.OutOfRightBounds(dotPos))
                {
                    Vector2 hitPointX = new Vector2(FieldBounds.bounds.x, 0);
                    float DistanceX = Vector2.Distance(hitPointX, ballPosX);
                    dotPos.x = FieldBounds.bounds.x + DistanceX + forceReflection.x * timeStamp;
                }
                dotPos.y = (ballPos.y + forceReflection.y * timeStamp) - Physics2D.gravity.magnitude * Mathf.Pow(timeStamp, 2) /2f;
            }
            dotsList[i].transform.position = dotPos;
            timeStamp += dotSpacing;
        }
    }
    public void ShowTrajectory(float alpha)
    {
        dotsParent.SetActive(true);
        for (int i = 0; i < dotsNumber; i++)
        {
            Color temp = dotsList[i].sr.color;
            temp.a = alpha;
            dotsList[i].sr.color = temp;
        }
    }
    public void HideTrajectory()
    {
        dotsParent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
