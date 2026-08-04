using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Animator bossAnim;
    public Animator laserGroupAnim1;
    public Animator laserGroupAnim2;
    public Animator laserGroupAnim3;
    public Animator bossWall2Anim;
    public AudioSource bossMusic;
    public SnakeMovement snake;
    public Logic logic;
    public GameObject food;
    public GameObject boss_wall1;
    public GameObject laserGroup1;
    public GameObject laserGroup2;
    public GameObject laserGroup3;
    public LayerMask bombLayer;
    public BoxCollider2D bombGrid1;

    public List<GameObject> bombList;
    private Bounds bombGrid1Bounds;

    public TextMeshProUGUI hint1;
    public TextMeshProUGUI hint2;
    public TextMeshProUGUI hint3;
    public GameObject bomb_indicator;
    public GameObject bomb;
    public GameObject projectile;
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
        snake.next_direction = new Vector2(0.0f, 0.0f);

        snake.canMove = false;
        snake.transform.position = new Vector3(-6.0f, 0.0f);

        playMusic();
        bossAnim.SetTrigger("Intro");
        yield return new WaitForSeconds(introTime);

        snake.canMove = true;

        //yield return StartCoroutine(AroundTheWorld());
        boss_wall1.SetActive(true);
        StartCoroutine(IntroAttack());
    }

    IEnumerator ClearBomb()
    {
        foreach (GameObject b in bombList)
        {
            Destroy(b);
        }
        yield return new WaitForSeconds(0.0f);
    }
    IEnumerator ShowHint(TextMeshProUGUI text, int blink)
    {
        for (int i = 1; i <= blink; i++)
        {
            yield return new WaitForSeconds(0.5f);
            text.alpha = 255f;
            yield return new WaitForSeconds(0.5f);
            text.alpha = 0.0f;
        }
    }
    IEnumerator IntroAttack() //bomb scatter & random shoot
    {
        float explodeTime = 2f;
        StartCoroutine(SpawnBomb(bombGrid1Bounds, bombLayer, explodeTime));

        yield return new WaitForSeconds(4f);

        for (int i = 1; i <= 3; i++){
            StartCoroutine(SpawnBomb(bombGrid1Bounds, bombLayer, explodeTime/2));
        }

        StartCoroutine(ShootWithInterval(2.5f, 4));
        yield return new WaitForSeconds(3f);
        for (int i = 1; i <= 5; i++)
        {
            StartCoroutine(SpawnBomb(bombGrid1Bounds, bombLayer, explodeTime));
        }

        yield return new WaitForSeconds(3f);
        for (int i = 1; i <= 5; i++)
        {
            StartCoroutine(SpawnBomb(bombGrid1Bounds, bombLayer, explodeTime/2));
        }

        yield return new WaitForSeconds(2f);
        for (int i = 1; i <= 5; i++)
        {
            StartCoroutine(SpawnBomb(bombGrid1Bounds, bombLayer, explodeTime));
        }

        yield return new WaitForSeconds(3f);
        StartCoroutine(AroundTheWorld());
    }
    IEnumerator AroundTheWorld()
    {
        boss_wall1.SetActive(false);
        yield return new WaitForSeconds(1f);
        bossAnim.SetTrigger("ATW");

        for (int i = 0; i <= 7; i++)
        {
            StartCoroutine(SpawnBomb(bombGrid1Bounds, bombLayer, 2f));
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(ShowHint(hint1, 3));
        yield return new WaitForSeconds(3f);
        bossWall2Anim.SetTrigger("Appear");
        yield return new WaitForSeconds(3f);
        bossAnim.SetTrigger("CTW");
        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(CloseTheWorld());
    }

    IEnumerator CloseTheWorld()
    {
        StartCoroutine(ClearBomb());
        for (int k = 1; k <= 3; k++)
        {
            for (int i = 1; i <= 5; i++)
            {
                Instantiate(projectile, transform.position, transform.rotation);
                yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForSeconds(.3f);
        }
        yield return new WaitForSeconds(2f);
        bossAnim.SetTrigger("RAN");
        yield return new WaitForSeconds(4.7f);

        StartCoroutine(ShowHint(hint2, 2));

        bossWall2Anim.SetTrigger("Disappear");
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(Enraged());
    }

    IEnumerator Enraged()
    {
        bossAnim.SetTrigger("FINALPHASE");
        yield return new WaitForSeconds(2.5f);

        laserGroup1.SetActive(true);
        laserGroupAnim1.SetTrigger("Shoot");
        yield return new WaitForSeconds(1.5f);
        laserGroup1.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        laserGroup2.SetActive(true);
        laserGroupAnim2.SetTrigger("Shoot");
        yield return new WaitForSeconds(1.5f);
        laserGroup2.SetActive(false);

        yield return new WaitForSeconds(2f);

        laserGroup3.SetActive(true);
        laserGroupAnim3.SetTrigger("Shoot");
        yield return new WaitForSeconds(1.5f);
        laserGroup3.SetActive(false);
    }
    IEnumerator ShootWithInterval(float interval, int projectile_count)
    {
        for (int i = 1; i <= projectile_count; i++)
        {
            Instantiate(projectile, transform.position, transform.rotation);
            yield return new WaitForSeconds(interval);
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
        bombList.Add(bomb1);

        Destroy(bomb_indicator1);
    }
}
