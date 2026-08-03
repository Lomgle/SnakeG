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
    public BoxCollider2D bombGrid1;
    private Bounds bombGrid1Bounds;

    public GameObject bomb_indicator;
    public GameObject bomb;
    void Start()
    {
        bombGrid1Bounds = bombGrid1.bounds;
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

        snake.direction = Vector2.right;
        snake.canMove = true;
        boss_wall1.SetActive(true);
        StartCoroutine(BombScatter());
    }

    IEnumerator BombScatter()
    {
        float explodeTime = 2f;

        StartCoroutine(SpawnBomb(bombGrid1Bounds, bossLayer, explodeTime));

        yield return new WaitForSeconds(4f);

        for (int i = 1; i <= 3; i++){
            StartCoroutine(SpawnBomb(bombGrid1Bounds, bossLayer, explodeTime));
        }

        yield return new WaitForSeconds(3f);
        for (int i = 1; i <= 5; i++)
        {
            StartCoroutine(SpawnBomb(bombGrid1Bounds, bossLayer, explodeTime));
        }

        yield return new WaitForSeconds(3f);
        for (int i = 1; i <= 5; i++)
        {
            StartCoroutine(SpawnBomb(bombGrid1Bounds, bossLayer, explodeTime/2));
        }

        yield return new WaitForSeconds(2f);
        for (int i = 1; i <= 5; i++)
        {
            StartCoroutine(SpawnBomb(bombGrid1Bounds, bossLayer, explodeTime));
        }
    }
    
    public Vector2 RandomizePos(Bounds bounds, LayerMask layer)
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
        return temp;
    }

    IEnumerator SpawnBomb(Bounds bounds, LayerMask layer, float explodeTime)
    {
        Vector2 bomb_pos = RandomizePos(bounds, layer);

        GameObject bomb_indicator1 = Instantiate(bomb_indicator);
        bomb_indicator1.transform.position = bomb_pos;

        yield return new WaitForSeconds(explodeTime);

        GameObject bomb1 = Instantiate(bomb);
        bomb1.transform.position = bomb_pos;

    }
}
