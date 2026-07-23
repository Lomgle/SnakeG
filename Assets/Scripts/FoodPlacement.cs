using UnityEngine;

public class FoodPlacement : MonoBehaviour
{
    public BoxCollider2D foodGrid;

    private void RandomizePosition()
    {
        Bounds bounds = foodGrid.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        gameObject.transform.position = new Vector3(
            Mathf.Round(x),
            Mathf.Round(y),
            0.0f
        );
    }
    void Start()
    {
        RandomizePosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            RandomizePosition();
        }
    }
}
