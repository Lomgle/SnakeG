using UnityEngine;

public class FoodPlacement : MonoBehaviour
{
    public BoxCollider2D foodGrid;
    public LayerMask snakeLayer;
    public AudioSource eatFX;
    private void RandomizePosition()
    {
        Bounds bounds = foodGrid.bounds;
        float x, y;
        Vector2 temp;
        do
        {
            x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));
            temp.x = x;
            temp.y = y;
        } while (Physics2D.OverlapPoint(temp, snakeLayer));
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
            eatFX.Play();
        }
    }
}
