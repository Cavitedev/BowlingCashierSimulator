
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI givenShoes;
    [SerializeField] private TextMeshProUGUI returnedShoes;
    

    private void Start()
    {
        GameManager.Instance.onGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        givenShoes.SetText(ShoesGivenManager.Instance.shoesGiven.ToString());
        returnedShoes.SetText(ShoesGivenManager.Instance.shoesReturn.ToString());
        panel.SetActive(true);
    }
    
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
