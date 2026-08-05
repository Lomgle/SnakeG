using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeMovement : MonoBehaviour
{
    private Vector2 temp = new Vector2(0, 0);
    public Vector2 direction;
    public Vector2 next_direction;
    public List<Transform> segmentList;
    public Transform segmentPrefab;

    public float moveRate = 10f;
    private float moveTimer = 0;
    public Logic logic;
    public int score = 0;
    public bool flagged = false;
    public bool paused = false;
    public bool canMove = true;
    public void Move()
    {
        direction = next_direction;
        for (int i = segmentList.Count - 1; i > 0; i--)
            segmentList[i].position = segmentList[i - 1].position;
        transform.position += (Vector3)direction;
    }
    void Start()
    {
        InputSystem.EnableDevice(Keyboard.current);
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
        segmentList = new List<Transform>();
        segmentList.Add(transform);
    }
    public void ControlDirection()
    {
        if (Keyboard.current.aKey.isPressed  && (direction != Vector2.right || segmentList.Count <= 1)) {
            next_direction = Vector2.left;
        }
        if (Keyboard.current.dKey.isPressed  && (direction != Vector2.left || segmentList.Count <= 1)) {
            next_direction = Vector2.right;
        }
        if (Keyboard.current.sKey.isPressed  && (direction != Vector2.up || segmentList.Count <= 1)) {
            next_direction = Vector2.down;
        }
        if (Keyboard.current.wKey.isPressed  && (direction != Vector2.down || segmentList.Count <= 1)) {
            next_direction = Vector2.up;
        }
    }
    void FixedUpdate()
    {
        if (direction != temp) {
            logic.hint_text.alpha = 0.0f;
        }
    }
    void Update()
    {
        if (canMove){
            ControlDirection();
            moveTimer += Time.deltaTime;
            if (moveTimer >= moveRate)
            {
                moveTimer = 0.0f;
                Move();
            }            
        }


        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!paused)
            {
                logic.PauseGame();
                paused = true;
            } else
            {
                logic.ResumeGame();
                paused = false;
            }
        }
    }


    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segmentList[^1].position; //^1 = -1
        segmentList.Add(segment);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Grow();
            logic.AddScore();
        }
        if (collision.tag == "Wall")
        {
            logic.GameOver();
        }
        if (collision.tag == "SnakeSegment")
        {
            if (flagged)
            {
                flagged = false;
                logic.GameOver();
            }
            else flagged = true;
        }
        if (collision.tag == "Boss" || collision.tag == "Boss1")
        {
            logic.GameOver();
            PlayerPrefs.SetInt("DiedToBoss", 1);
        }
    }
}
