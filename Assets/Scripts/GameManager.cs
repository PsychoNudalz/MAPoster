using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float score = 0;

    [SerializeField]
    private TextMeshPro scoreText;

    public static GameManager current;

    private void Awake()
    {
        if (!current)
        {
            current = this;
        }
    }

    public void AddScore(float f)
    {
        score += f;
        scoreText?.SetText(Math.Round(score).ToString());
    }

    public static void AddScore_S(float s)
    {
        current.AddScore(s);
    }

    public static void AddKill_S(float s, int c)
    {
        AddScore_S(s);
        EnemyManager.AddKill_S(c);
        PlayerController.Static_UpdateSpeed();

    }

    public void GameOver()
    {
        UIManager.GameOver_S();
    }

    public static void GameOver_S()
    {
        current.GameOver();
    }

    public static void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}