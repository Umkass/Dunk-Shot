using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    float defaultScaleY = 1;
    float defaultPosY = 0;
    float maxScaleY = 0.469f;
    float maxPosY = -0.528f;
    float maxballPosY = -0.83f;
    public BallMovement ball;
    public void StretchNet(float force, float maxForce)
    {
        Vector3 newPosNet = transform.localPosition;
        newPosNet.y = force / maxForce * maxPosY;
        transform.localPosition = newPosNet;

        Vector3 newBallPos = ball.gameObject.transform.localPosition;
        newBallPos.y = force / maxForce * maxballPosY;
        ball.transform.localPosition = newBallPos;

        Vector3 newScaleNet = transform.localScale;
        newScaleNet.y = defaultScaleY + force / maxForce * maxScaleY;
        transform.localScale = newScaleNet;
    }
    public void SetDefaultTransform()
    {
        Vector3 newPos = transform.localPosition;
        newPos.y = defaultPosY;
        transform.localPosition = newPos;

        Vector3 newScale = transform.localScale;
        newScale.y = defaultScaleY;
        transform.localScale = newScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallMovement ball = collision.gameObject.GetComponent<BallMovement>();
        if(ball != null)
        {
            if (ball.wasPushed)
            {
                collision.gameObject.transform.position = transform.position;
                collision.gameObject.transform.SetParent(transform.parent, true);
                this.ball = ball;
                ball.MakeKinematic();
                ball.wasPushed = false;
            }
        }
    }
}
