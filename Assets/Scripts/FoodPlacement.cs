using UnityEngine;

public class FoodPlacement : MonoBehaviour
{
    public BoxCollider2D foodGrid;

    private void RandomizePosition()
    {
        Bounds bounds = foodGrid.bounds;
        float x, y;
        int iter = 0;
        Vector2 temp;
        do
        {
            x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));
            temp.x = x;
            temp.y = y;
            iter++;
        } while (Physics2D.OverlapPoint(temp));
        Debug.Log("Takes " + iter);
        iter = 0;
        gameObject.transform.position = new Vector3(
            x,
            y,
            0.0f
        );
    }
    void Start()
    {
        RandomizePosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        RandomizePosition(); 
        if (collision.tag == "Player")
        {
            RandomizePosition();
        }
    }
}
