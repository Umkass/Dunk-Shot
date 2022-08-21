using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject firstHoopPrefab;
    [SerializeField] GameObject defaultHoopPrefab;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject gameFieldPrefab;
    [SerializeField] GameObject FieldsParent;
    [SerializeField] List<Vector2> startPositions = new List<Vector2>();
    private void Awake()
    {
        CreateStartHoops();
        CreateStartBall();
        CreateStartField();
    }

    void CreateStartHoops()
    {
        Instantiate(firstHoopPrefab, startPositions[0], Quaternion.identity);
        Instantiate(defaultHoopPrefab, startPositions[1], Quaternion.identity);
    }
    public void CreateStartBall()
    {
        GameObject ball = Instantiate(ballPrefab, new Vector2(startPositions[0].x, 0.5f), Quaternion.identity);
        ball.GetComponent<BallMovement>().spawner = this;
    }

    void CreateStartField()
    {
        GameObject field = Instantiate(gameFieldPrefab, FieldsParent.transform, true);
        if(ScreenBounds.bounds.x<=2.7f)
            field.transform.localScale = new Vector3(ScreenBounds.bounds.x * 2, ScreenBounds.bounds.y * 2);
        else
            field.transform.localScale = new Vector3(2.7f*2, ScreenBounds.bounds.y * 2);

        UIManager.Instance.field = field.GetComponent<Field>();
    }

    public void SpawnNewHoop(Vector2 ballPos)
    {
            
        int obstacleUnlock = Random.Range(0, UIManager.Instance.scorePoint / 10);
        if (FieldBounds.OnLeftSide(ballPos))
        {
            switch (obstacleUnlock)
            {
                case 0: Instantiate(defaultHoopPrefab, FieldBounds.RandomTopLeftPosition(), Quaternion.identity); break;
                case 1: Instantiate(defaultHoopPrefab, FieldBounds.RandomTopLeftPosition(), Quaternion.Euler(0f, 0f, Random.Range(-15, -30f))); break;
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                default:
                    break;
            }
        }
        else if (FieldBounds.OnRightSide(ballPos))
        {
            switch (obstacleUnlock)
            {
                case 0: Instantiate(defaultHoopPrefab, FieldBounds.RandomTopRightPosition(), Quaternion.identity); break;
                case 1: Instantiate(defaultHoopPrefab, FieldBounds.RandomTopRightPosition(), Quaternion.Euler(0f, 0f, Random.Range(15f, 30f))); break;
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                default:
                    break;
            }
        }
        //если больше 10 поинтов иногда с поворотом
        //Instantiate(defaultHoopPrefab, ScreenBounds.RandomTopPosition(), Quaternion.Euler(0f, 0f, Random.Range(-15f,15f)));

        //C ПРОТИВОПОЛОЖНОЙ СТОРОНЫ
        //если больше 10 поинтов иногда с поворотом
        //20 и больше с разными припятствиями (стенка сбоку корзины впритык справа)
        //20 прям рядом с тобой с той же стороны или растояние от тебя чуток
        // 25 ездит вверх вниз
        // 30 ездит вправо влево
        //30 круглешок сверху над корзиной
        //40 стенка сбоку корзины впритык слева
        // 50 сверху над корзиной

        //50+
        //НАД СОБОЙ С ПРИПЯТСТВИЕМ ПРАВО ИЛИ ВЛЕВО БОЛЬШИМ

        //80 вертикальная хуйня сверху над корзиной
        // разделять припятствия на правые левые

        //100+ ездит вправо влево над тобой спавн с поворотом
    }
}
