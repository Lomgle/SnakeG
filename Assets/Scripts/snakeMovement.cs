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
    public List<Transform> segmentList;
    public Transform segmentPrefab;

    public float moveRate = 10f;
    private float moveTimer = 0;
    public Logic logic;
    public int score = 0;
    public bool flagged = false;
    public bool paused = false;
    public bool hasMoved = true;
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
        if (Keyboard.current.aKey.isPressed && hasMoved && (direction != Vector2.right || segmentList.Count <= 1)) {
            direction = Vector2.left; hasMoved = false;
        }
        if (Keyboard.current.dKey.isPressed && hasMoved && (direction != Vector2.left || segmentList.Count <= 1)) {
            direction = Vector2.right; hasMoved = false;
        }
        if (Keyboard.current.sKey.isPressed && hasMoved && (direction != Vector2.up || segmentList.Count <= 1)) {
            direction = Vector2.down; hasMoved = false;
        }
        if (Keyboard.current.wKey.isPressed && hasMoved && (direction != Vector2.down || segmentList.Count <= 1)) {
            direction = Vector2.up; hasMoved = false;
        }
    }
    void FixedUpdate()
    {
        if (direction != temp) logic.hint_text.alpha = 0.0f;
        else logic.hint_text.alpha = 67f;
    }
    void Update()
    {     
        ControlDirection();
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveRate)
        {
            moveTimer = 0.0f;
            Move();
            hasMoved = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
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
    }
}
