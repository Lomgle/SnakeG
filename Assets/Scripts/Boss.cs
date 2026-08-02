using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Animator bossAnim;
    public AudioSource bossMusic;
    public SnakeMovement snake;
    public Logic logic;
    public GameObject food;
    public GameObject boss_wall1;
    public LayerMask bossLayer;
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

    public void TriggerSpawnBoss()
    {
        StartCoroutine(SpawnBoss());
    }
    IEnumerator SpawnBoss()
    {
        float introTime = 6.0f;

        logic.ClearSnake();
        food.SetActive(false);
        snake.direction = new Vector2(0.0f, 0.0f);
        snake.canMove = false;
        snake.transform.position = new Vector3(-6.0f, 0.0f);
        playMusic();
        bossAnim.SetTrigger("Intro");
        yield return new WaitForSeconds(introTime);
        snake.canMove = true;
        boss_wall1.SetActive(true);

        
    }
    
    public void RandomizePos(Bounds bounds, LayerMask layer)
    {
        float x, y;
        Vector2 temp;
        do
        {
            x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));
            temp.x = x;
            temp.y = y;
        } while (Physics2D.OverlapPoint(temp, layer));
    }
}
