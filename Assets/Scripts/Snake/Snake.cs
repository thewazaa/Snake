using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System.Linq;

[RequireComponent(typeof(SpriteShapeController))]
public class Snake : MonoBehaviour
{
    public delegate void dGameOver();
    public delegate void dAppleEaten(int total);

    public dGameOver EventGameOver;
    public dAppleEaten EventAppleEaten;

    public int growLeft = 2;
    public int growEat = 4;
    public int applesEaten = 0;
    public float speed = .25f;
    public float minSpeed = .1f;
    public bool isMoving = true;
    public Vector2 direction = new Vector2(1, 0);
    public List<Vector2> snakePositions = new List<Vector2>() { };

    private Vector2 Head => snakePositions[snakePositions.Count - 1];

    private SpriteShapeController spriteShapeController;
    private PoolInstance poolInstance;

    private void Awake()
    {
        spriteShapeController = GetComponent<SpriteShapeController>();
        poolInstance = GetComponent<PoolInstance>();
    }

    private void Start()
    {
        DrawSnake();
        if (!isMoving)
            return;
        StartCoroutine(CoroutineSnake());
    }

    private IEnumerator CoroutineSnake()
    {
        DrawSnake();
        InstanceNewApple();
        while (1 == 1)
        {
            yield return new WaitForSeconds(speed);
            GetNextHeadPosition();
            if (CheckHeadCollissions())
            {
                DrawSnake();
                EventGameOver?.Invoke();
                yield break;
            }
            CheckEat();
            RemoveTailPosition();
            DrawSnake();
        }
    }

    private void InstanceNewApple()
    {
        poolInstance.ItemInstance(new Vector3(Random.Range(-7, 8), Random.Range(-3, 4), 0), Quaternion.identity, null);
    }

    private void GetNextHeadPosition() => snakePositions.Add(snakePositions[snakePositions.Count - 1] + direction);

    private void CheckEat()
    {
        Vector2 head = Head;
        Apple apple = FindObjectsOfType<Apple>().FirstOrDefault(x => x.position.x == head.x && x.position.y == head.y);
        if (apple == null)
            return;
        applesEaten++;
        EventAppleEaten?.Invoke(applesEaten);
        speed -= .01f;
        if (speed < minSpeed)
            speed = minSpeed;
        apple.Eaten();
        for (int i = 0; i <= applesEaten / 4; i++)
            InstanceNewApple();
        growLeft = growEat;
    }

    private void RemoveTailPosition()
    {
        if (growLeft != 0)
        {
            growLeft--;
            return;
        }
        snakePositions.RemoveAt(0);
    }

    private void DrawSnake()
    {
        spriteShapeController.spline.Clear();
        int i = 0;
        foreach (Vector2 position in snakePositions)
        {
            Vector2 position2 = position;
            if (i > 1 && i == snakePositions.Count - 1)
                position2 = (position + snakePositions[snakePositions.Count - 2]) / 2;
            spriteShapeController.spline.InsertPointAt(i, position2 / 3);
            spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            i++;
        }
    }

    private bool CheckHeadCollissions()
    {
        Vector2 head = Head;
        if (head.x == 8 || head.x == -8 || head.y == 4 || head.y == -4) // Walls
            return true;

        return (snakePositions.FindIndex(x => x == head) != snakePositions.Count - 1);
    }
}