using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoesGivenManager : MonoBehaviour
{

    public static ShoesGivenManager Instance;

    private List<GameObject> _shoesList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddShoe(Pickable shoe)
    {
        Debug.Log("Add Shoe");
        Transform parentObj = shoe.pickTransform;
        parentObj.name += _shoesList.Count;
        parentObj.SetParent(transform);
        parentObj.gameObject.SetActive(false);
        _shoesList.Add(parentObj.gameObject);
    }
}
