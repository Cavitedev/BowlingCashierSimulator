using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CircleLoader : MonoBehaviour
{
    

    
    public static CircleLoader Instance;
    private Image _circleImage;

    [SerializeField] private float pickTime = 2f; 
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _circleImage = GetComponent<Image>();
    }

    public void Pick(GameObject sender)
    {
        _circleImage.fillAmount = 0;
        StartCoroutine(FillPick(sender));
    }

    public void ExitPick()
    {
        StopAllCoroutines();
        _circleImage.fillAmount = 0;
    }

    private IEnumerator FillPick(GameObject sender)
    {
        while (_circleImage.fillAmount <= 0.99f)
        {
            _circleImage.fillAmount += Time.deltaTime / pickTime;
            yield return null;
        }

        _circleImage.fillAmount = 0f;
        sender.SendMessage("OnPointerClick");
        yield break;

    }
    
    
}
