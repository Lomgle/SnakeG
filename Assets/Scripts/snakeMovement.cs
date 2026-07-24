using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector2 direction;
    private List<Transform> segmentList;
    public Transform segmentPrefab;
    private bool hasntEaten = true;
    public Vector3 previousPosition;
    void Start()
    {
        segmentList = new List<Transform>();
        segmentList.Add(transform);
    }
    public void ControlDirection()
    {
        if (Keyboard.current.aKey.isPressed) direction = Vector2.left;
        if (Keyboard.current.dKey.isPressed) direction = Vector2.right;
        if (Keyboard.current.sKey.isPressed) direction = Vector2.down;
        if (Keyboard.current.wKey.isPressed) direction = Vector2.up;
    }
    void Update()
    {
        for (int i = segmentList.Count - 1; i > 0; i--)
        {
            segmentList[i].position = segmentList[i - 1].position;
        }
        ControlDirection();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (hasntEaten) previousPosition = transform.position;
        transform.position = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, 0.0f);
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
        }
    }
}
