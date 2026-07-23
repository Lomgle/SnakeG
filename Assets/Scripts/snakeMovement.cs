using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector2 direction;
    public void ControlDirection()
    {
        if (Keyboard.current.aKey.isPressed) direction = Vector2.left;
        if (Keyboard.current.dKey.isPressed) direction = Vector2.right;
        if (Keyboard.current.sKey.isPressed) direction = Vector2.down;
        if (Keyboard.current.wKey.isPressed) direction = Vector2.up;
    }
    void Update()
    {
        ControlDirection();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        this.transform.position = new Vector3(this.transform.position.x + direction.x, this.transform.position.y + direction.y, 0.0f);
    }
}
