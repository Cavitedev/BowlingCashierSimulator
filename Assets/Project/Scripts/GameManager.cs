using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void OnGameOver();

    public OnGameOver onGameOver;
    private bool _isGameOver;

    private bool _isGameRunning;

    // TIME
    private float _timeLeft;
    public float totalTime;
    public float TimeLeft
    {
        get { return _timeLeft; }
        set
        {
            _timeLeft = value;
        }
    }



    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {

        TimeLeft = totalTime;
        StartMenu.Instance.onGameStart += onGameStart;
    }



    private void onGameStart()
    {
        _isGameRunning = true;
    }
    private void Update()
    {

        if (!_isGameRunning) return;
        
        if (TimeLeft <= 0f)
        {
            TimeLeft = 0;
            if (!_isGameOver)
            {
                onGameOver?.Invoke();
                _isGameOver = true;
            }
        }
        else
        {
            TimeLeft -= Time.deltaTime;
        }
    }

    public void GameOver()
    {
        onGameOver();
    }
}
