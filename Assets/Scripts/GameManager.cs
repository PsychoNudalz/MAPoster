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


    [Header("Chaining")]
    [SerializeField]
    [Min(1f)]
    private float chainBonus = 1f;

    [SerializeField]
    private float bonusPerKill;

    [SerializeField]
    private float bonusDecayPerSecond = .3f;

    [SerializeField]
    private TextMeshPro bonusText;

    public static GameManager current;

    private void Awake()
    {
        if (!current)
        {
            current = this;
        }
    }

    private void Start()
    {
        UpdateBonusText();
    }

    private void FixedUpdate()
    {
        if (chainBonus>1)
        {
            chainBonus -= bonusDecayPerSecond * Time.deltaTime;
            if (chainBonus < 1)
            {
                chainBonus = 1;
            }
            UpdateBonusText();
        }
    }

    public void AddScore(float f)
    {
        score += f*chainBonus;
        scoreText?.SetText(Math.Round(score).ToString());
    }

    public static void AddScore_S(float s)
    {
        current.AddScore(s);
    }

    public void AddKill(EnemyEntity e, float s, int c)
    {
        AddScore(s);
        ChangeBonus(current.bonusPerKill);
        EnemyManager.AddKill_S(e, c);
        PlayerController.Static_UpdateSpeed();
    }

    public static void AddKill_S(EnemyEntity e, float s, int c)
    {
       current.AddKill(e,s,c);
    }

    public void GameOver()
    {
        UpdateHighScore();
        UIManager.GameOver_S(score,PlayerPrefs.GetFloat("score"));
    }
    


    public static void GameOver_S()
    {
        current.GameOver();
    }

    public static void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeBonus(float b)
    {
        print("Increase bonus "+chainBonus+" by: "+b);
        chainBonus += b;
        chainBonus = Math.Max(chainBonus, 1);
        UpdateBonusText();

    }

    public static void ChangeBonus_S(float b)
    {
        current.ChangeBonus(b);
    }

    void UpdateBonusText()
    {
        bonusText?.SetText("x"+ $"{chainBonus:F1}");
    }

    void UpdateHighScore()
    {
        PlayerPrefs.SetFloat("score",(float) Math.Round(Math.Max(PlayerPrefs.GetFloat("score"),score)));
    }
}