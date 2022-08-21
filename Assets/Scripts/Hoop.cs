using System.Collections;
using UnityEngine;

public class Hoop : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] hoopSprites;
    Color redHoop = new Color(0.7882353f, 0.345098f, 0.2196078f);
    Color greyHoop = new Color(0.6666667f, 0.6666667f, 0.6666667f);
    public bool isFirstHoop;
    public GameObject starPrefab;

    [Range(0f, 1f)]
    public float StarChance;

    void Awake()
    {
        foreach (var item in hoopSprites)
        {
            item.color = redHoop;
        }
        if (!isFirstHoop && Random.Range(0f, 1f) <= StarChance)
        {
            Instantiate(starPrefab, transform);
        }
    }
    public void HoopCompleted()
    {
        foreach (var item in hoopSprites)
        {
            item.color = greyHoop;
        }
    }
    public void SetDefaultRotation()
    {
        gameObject.transform.rotation = Quaternion.identity;
    }

    public void Disappear()
    {
        StartCoroutine(DisappearCoroutine());
    }

    IEnumerator DisappearCoroutine()
    {
        while (gameObject.transform.localScale.x > 0)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - 0.01f, gameObject.transform.localScale.y - 0.01f);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
