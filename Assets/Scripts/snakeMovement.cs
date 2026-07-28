using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeMovement : MonoBehaviour
{
    private Vector2 direction;
    public List<Transform> segmentList;
    public Transform segmentPrefab;

    public float moveRate = 10f;
    private float moveTimer = 0;
    public Logic logic;
    public int score = 0;
    public void Move()
    {
        for (int i = segmentList.Count - 1; i > 0; i--)
            segmentList[i].position = segmentList[i - 1].position;
        transform.position += (Vector3)direction;
    }
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
        segmentList = new List<Transform>();
        segmentList.Add(transform);
    }
    public void ControlDirection()
    {
        if (Keyboard.current.aKey.isPressed && (direction != Vector2.right || segmentList.Count <= 1)) direction = Vector2.left;
        if (Keyboard.current.dKey.isPressed && (direction != Vector2.left || segmentList.Count <= 1)) direction = Vector2.right;
        if (Keyboard.current.sKey.isPressed && (direction != Vector2.up || segmentList.Count <= 1)) direction = Vector2.down;
        if (Keyboard.current.wKey.isPressed && (direction != Vector2.down || segmentList.Count <= 1)) direction = Vector2.up;
    }
    void Update()
    {        
        ControlDirection();
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveRate)
        {
            moveTimer = 0.0f;
            Move();
        }
    }


    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segmentList[^1].position; //^1 = -1
        segmentList.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Grow();
            logic.AddScore();
        }
        if (collision.tag == "Wall")
        {
            Debug.Log("died");
            logic.GameOver();
        }
    }
}
