using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    private static StartMenu _instance;
    public static StartMenu Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }

    public delegate void OnGameStart();

    public OnGameStart onGameStart;

    // Start is called before the first frame update
    public void StartGame()
    {
        onGameStart?.Invoke();
        gameObject.SetActive(false);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
    }
}