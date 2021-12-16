using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Snake))]
public class SnakeFeedback : MonoBehaviour
{
    public GameObject gameOver;
    public TextMeshProUGUI textApplesEaten;

    private Snake snake;

    private void Awake()
    {
        snake = GetComponent<Snake>();
    }

    private void Start()
    {
        gameOver.SetActive(false);

        snake.EventGameOver += GameOver;
        snake.EventAppleEaten += AppleEaten;
    }

    private void OnDestroy()
    {
        snake.EventGameOver -= GameOver;
        snake.EventAppleEaten -= AppleEaten;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        gameOver.transform.DOShakeScale(1);
    }

    public void AppleEaten(int total)
    {
        textApplesEaten.text = $"Apples eaten: {total}";
    }
}
