using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Shoe : MonoBehaviour
{
    [FormerlySerializedAs("shoeNumber")] [SerializeField] private int shoeSize;
    public TextMeshPro tmPro;

    [SerializeField] private ShelfBox selfbox;
    [SerializeField] private GameObject mainObject;
    

    public void SetShoeSize(int number)
    {
        shoeSize = number;
        tmPro.SetText(number.ToString());
    }
    
    private void OnValidate()
    {
        SetShoeSize(shoeSize);
    }

    public void Destroy()
    {
        selfbox.restPickable = null;
        Destroy(mainObject);
    }
    
}