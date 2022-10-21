using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

public class PasswordController : MonoBehaviour
{
    [SerializeField]
    private string password = "1234";

    [SerializeField]
    private string input = "";

    [SerializeField]
    private float timeInterval = .5f;

    private float lastTime = -10f;


    [SerializeField]
    private GameObject[] passwordGOs;

    [SerializeField]
    private TextMeshProUGUI[] texts;

    [Space(10)]
    [SerializeField]
    private GameObject incorrectGO;

    [SerializeField]
    private GameObject correctGO;

    [Header("On Complete")]
    [SerializeField]
    private float timer = 10;

    private float timeNow = 0;

    [SerializeField]
    private GameObject canvas;
    
    
    private IDisposable _eventListener;

    private void Awake()
    {
        SetDisplay();
        incorrectGO?.SetActive(false);
        correctGO?.SetActive(false);
        timeNow = timer;

    }

    private void Update()
    {
        if (timeNow < timer&&timeNow>0)
        {
            timeNow -= Time.deltaTime;
        }else if (timeNow <= 0)
        {
            canvas.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _eventListener = InputSystem.onEvent.ForDevice(Keyboard.current).Call(OnKeyPressed);
    }

    private void OnDisable()
    {
        _eventListener.Dispose();
    }

    private void OnKeyPressed(InputEventPtr eventPtr)
    {
        if ((!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>()) || Time.time - lastTime < timeInterval)
        {
            return;
        }

        lastTime = Time.time;

        foreach (var control in eventPtr.EnumerateChangedControls())
        {
            print(control.name);
            string s = control.name;
            if (s.Contains("numpad"))
            {
                s = s[^1].ToString();
            }
            else
            {
                s = s[0].ToString();
            }

            AddInput(s);
        }
    }

    public void AddInput(string s)
    {
        if (Math.Abs(timeNow - timer) > Time.fixedDeltaTime)
        {
            return;
        }

        input += s;
        if (input.Length >= 4)
        {
            if (password.ToLower().Equals(input.ToLower()))
            {
                if (Math.Abs(timeNow - timer) < Time.fixedDeltaTime)
                {
                    CompletePuzzle();
                }
            }
            else
            {
                input = "";
                incorrectGO?.SetActive(true);
            }
        }

        SetDisplay();
    }

    private void CompletePuzzle()
    {
        print("PASSWORD DONE");
        incorrectGO?.SetActive(false);
        correctGO?.SetActive(true);
        SetDisplay();
        timeNow -= Time.fixedDeltaTime*2;
    }

    public void SetDisplay()
    {
        foreach (GameObject go in passwordGOs)
        {
            go.SetActive(false);
        }


        foreach (TextMeshProUGUI textMeshProUGUI in texts)
        {
            textMeshProUGUI.text = "";
        }

        for (int i = 0; i < input.Length; i++)
        {
            passwordGOs[i].SetActive(true);
            texts[i].text = input[i].ToString().ToUpper();
        }
    }
}