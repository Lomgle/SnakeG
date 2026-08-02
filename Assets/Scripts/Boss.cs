using UnityEngine;

public class Boss : MonoBehaviour
{
    public Animator bossAnim;
    public AudioSource bossMusic;
    public SnakeMovement snake;
    public Logic logic;
    public GameObject food;
    void Start()
    {
        snake = GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeMovement>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void playMusic()
    {
        bossMusic.Play();
    }
    public void SpawnBoss()
    {
        logic.ClearSnake();
        food.SetActive(false);
        snake.direction = new Vector2(0.0f, 0.0f);
        snake.canMove = false;
        snake.transform.position = new Vector3(-6.0f, 0.0f);
        playMusic();
        bossAnim.SetTrigger("Intro");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
