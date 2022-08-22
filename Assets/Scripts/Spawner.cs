using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject firstHoopPrefab;
    [SerializeField] GameObject defaultHoopPrefab;
    [SerializeField] GameObject hoopWithLeftWallPrefab;
    [SerializeField] GameObject hoopWithRightWallPrefab;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject gameFieldPrefab;
    [SerializeField] GameObject gameFieldParent;
    [SerializeField] List<Vector2> startHoopPositions = new List<Vector2>();
    private void Awake()
    {
        CreateStartHoops();
        CreateStartBall();
        CreateStartField();
    }

    void CreateStartHoops()
    {
        Instantiate(firstHoopPrefab, startHoopPositions[0], Quaternion.identity);
        Instantiate(defaultHoopPrefab, startHoopPositions[1], Quaternion.identity);
    }
    public void CreateStartBall()
    {
        GameObject ball = Instantiate(ballPrefab, new Vector2(startHoopPositions[0].x, 0.5f), Quaternion.identity);
        ball.GetComponent<BallMovement>().spawner = this;
    }

    void CreateStartField()
    {
        GameObject field = Instantiate(gameFieldPrefab, gameFieldParent.transform, true);
        if (ScreenBounds.bounds.x <= 2.7f)
            field.transform.localScale = new Vector3(ScreenBounds.bounds.x * 2, ScreenBounds.bounds.y * 2);
        else
            field.transform.localScale = new Vector3(2.7f * 2, ScreenBounds.bounds.y * 2);

        UIManager.Instance.gameField = field.GetComponent<GameField>();
    }

    public void SpawnNextHoop(Vector2 ballPos)
    {

        int obstacleUnlock = Random.Range(0, UIManager.Instance.scorePoint / 10);
        if (FieldBounds.OnLeftSide(ballPos))
        {
            switch (obstacleUnlock)
            {
                case 0: Instantiate(defaultHoopPrefab, FieldBounds.RandomTopLeftPosition(), Quaternion.identity); break;
                case 1: Instantiate(defaultHoopPrefab, FieldBounds.RandomTopLeftPosition(), Quaternion.Euler(0f, 0f, Random.Range(-15, -30f))); break;
                case 2: Instantiate(hoopWithLeftWallPrefab, FieldBounds.RandomTopLeftPositionWithAdditionOffset(), Quaternion.identity); break;
                default:
                    Instantiate(defaultHoopPrefab, FieldBounds.RandomTopLeftPosition(), Quaternion.identity);
                    break;
            }
        }
        else if (FieldBounds.OnRightSide(ballPos))
        {
            switch (obstacleUnlock)
            {
                case 0: Instantiate(defaultHoopPrefab, FieldBounds.RandomTopRightPosition(), Quaternion.identity); break;
                case 1: Instantiate(defaultHoopPrefab, FieldBounds.RandomTopRightPosition(), Quaternion.Euler(0f, 0f, Random.Range(15f, 30f))); break;
                case 2: Instantiate(hoopWithRightWallPrefab, FieldBounds.RandomTopRightPositionWithAdditionOffset(), Quaternion.identity); break;
                default:
                    Instantiate(defaultHoopPrefab, FieldBounds.RandomTopRightPosition(), Quaternion.identity);
                    break;
            }
        }
        // 30+ moving right and left/moving up and down
        //40+ circle-obstacle above the basket
        //50+ wall-obstacle above the basket
        //60+ above ball with an wall-obstacleon the right or left (large)
        //70+ vertical wall-obstacle above the basket
        //80+ moving right left over ball (spawn with a turn)
    }
}
