using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Spawner spawner;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float defaultForce;
    Queue<Hoop> hoops = new Queue<Hoop>();
    public Hoop currentHoop;
    bool firstHoopCompleted = false; //not initial
    public bool canPush = true;

    bool isTouchedBound = false;
    bool isTouchedHoopBound = false;
    int multiBounce = 2; //число на которое умножить при отскоке
    int PerfectX = 0; //комбо перфект
    float timeToStuck = 5f;
    public bool wasPushed = true;

    public void Push(Vector2 force)
    {
        timeToStuck = 5f;
        isTouchedBound = false;
        isTouchedHoopBound = false;
        rb.AddForce(force * defaultForce, ForceMode2D.Impulse);
    }

    public void MakeKinematic()
    {
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.isKinematic = true;
    }
    public void MakeNonKinematic()
    {
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints2D.None;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bound"))
        {
            isTouchedBound = true;
        }
        if (collision.gameObject.CompareTag("HoopBound"))
        {
            isTouchedHoopBound = true;
            PerfectX = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hoop"))
        {
            Hoop hoop = collision.GetComponent<Hoop>();
            canPush = true;
            if (!hoop.isFirstHoop && hoop != currentHoop)
            {
                hoops.Enqueue(hoop);
                currentHoop = hoop;
                firstHoopCompleted = true;
                hoop.HoopCompleted();
                spawner.SpawnNewHoop(transform.position);
                hoops.Dequeue().Disappear();
                if (!isTouchedHoopBound)
                {
                    PerfectX++;
                }
                ScorePoints();
            }
            else if(hoop.isFirstHoop && hoop != currentHoop)
            {
                hoops.Enqueue(hoop);
                currentHoop = hoop;
            }
            hoop.SetDefaultRotation();
        }
        if (collision.CompareTag("Star") && !collision.GetComponent<Star>().isScored)
        {
            Star star = collision.GetComponent<Star>();
            if (!star.isScored)
            {
                star.isScored = true;
                UIManager.Instance.AddStarPoints();
                Destroy(collision.gameObject);
            }
        }
    }

    private void ScorePoints() //посчитать очки
    {
        if (isTouchedBound)
        {
            if (!isTouchedHoopBound)
                UIManager.Instance.AddScorePoints(multiBounce * (PerfectX + 1));
            else
                UIManager.Instance.AddScorePoints(1 * multiBounce);
        }
        else
        {
            if (!isTouchedHoopBound)
                UIManager.Instance.AddScorePoints(1 + PerfectX);
            else
                UIManager.Instance.AddScorePoints(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Hoop hoop = collision.GetComponent<Hoop>();
        if (hoop != null)
        {
            canPush = false;
            wasPushed = true;
        }
    }

    public void Unstuck()
    {
        gameObject.transform.position = new Vector2(currentHoop.transform.position.x, currentHoop.transform.position.y + 1f);
    }
    private void Update()
    {
        if (transform.parent == null) // летит
        {
            transform.Rotate(Vector3.forward * 270 * Time.deltaTime);
            timeToStuck-=Time.deltaTime;
            if (timeToStuck <= 0)
            {
                UIManager.Instance.ShowBtnStuck();
                timeToStuck = 5f;
            }
        }
        if (FieldBounds.OutOfFieldBounds(gameObject.transform.position))
        {
            if (firstHoopCompleted)
            {
                Destroy(gameObject);
                UIManager.Instance.SetupUIGameOver();
            }
            else
            {
                Destroy(gameObject);
                spawner.CreateStartBall();
                currentHoop.transform.rotation = Quaternion.identity;
            }
        }
    }
}
