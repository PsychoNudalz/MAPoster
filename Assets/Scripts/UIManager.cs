using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Animator gameOverAnimator;


    public static UIManager current;

    private void Awake()
    {
        current = this;
    }


    public static void GameOver_S()
    {
        current.GameOver();
    }

    public void GameOver()
    {
        if (gameOverAnimator)
        {
            gameOverAnimator.Play("GameOver");
        }
    }
}
