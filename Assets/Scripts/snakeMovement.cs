using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector2 direction;
    public void ControlDirection()
    {
        if (Keyboard.current.aKey.isPressed) direction = new Vector2(-0.5f, 0.0f);
        if (Keyboard.current.wKey.isPressed) direction = new Vector2(0.0f, 0.5f);
        if (Keyboard.current.dKey.isPressed) direction = new Vector2(0.5f, 0.0f);
        if (Keyboard.current.sKey.isPressed) direction = new Vector2(0.0f, -0.5f);
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
