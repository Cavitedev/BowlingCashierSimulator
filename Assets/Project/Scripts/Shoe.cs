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


    public int GetShoeSize() => shoeSize;
    public void SetShoeSize(int number)
    {
        shoeSize = number;
        tmPro.SetText(number.ToString());
        transform.localScale =  new Vector3(1, (number / 39f), 1) ;
    }
    
    private void OnValidate()
    {
        SetShoeSize(shoeSize);
    }

    public void Destroy()
    {
        selfbox.RestPickable = null;
        Destroy(mainObject);
    }
    
}
