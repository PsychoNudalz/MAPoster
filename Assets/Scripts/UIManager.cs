using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Animator gameOverAnimator;

    [SerializeField]
    private TextMeshProUGUI score;

    [SerializeField]
    private TextMeshProUGUI highScore;


    public static UIManager current;

    private void Awake()
    {
        current = this;
    }


    public static void GameOver_S(float s,float h)
    {
        current.GameOver(s,h);
    }

    public void GameOver(float s,float h)
    {
        if (gameOverAnimator)
        {
            gameOverAnimator.Play("GameOver");
        }
        score?.SetText($"SCORE:{s}");
        highScore?.SetText($"HIGHEST:{h}");
        
    }

    public void Restart()
    {
        GameManager.ResetGame();
    }
}
