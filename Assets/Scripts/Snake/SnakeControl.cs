using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Snake))]
public class SnakeControl : MonoBehaviour
{
    private Vector2 Direction
    {
        set => snake.direction = value;
    }

    private Snake snake;

    private void Awake()
    {
        snake = GetComponent<Snake>();
    }

    public void HandleMovement(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                direction.y = 0;
            else
                direction.x = 0;
            direction.Normalize();
            Direction = direction;
        }
    }
}
