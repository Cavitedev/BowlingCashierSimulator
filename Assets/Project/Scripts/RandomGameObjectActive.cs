using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomGameObjectActive : MonoBehaviour
{

    [SerializeField] private GameObject[] gameObjects;
    
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        int chosenHat = Random.Range(0, gameObjects.Length);
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(i==chosenHat);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
