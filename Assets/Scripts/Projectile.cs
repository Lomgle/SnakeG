using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float spinSpeed = 5f;
    public Rigidbody2D bullet;
    public SnakeMovement snake;

    void Start()
    {
        snake = GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeMovement>();
        
        Vector2 direction = (Vector2)snake.transform.position - (Vector2)transform.position;
        direction.Normalize();

        bullet.linearVelocity = direction * speed;
    }

    void Update()
    {
        transform.Rotate(0.0f, 0.0f, spinSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall") Destroy(gameObject);
    }
}
