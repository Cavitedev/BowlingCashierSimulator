using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shoe : MonoBehaviour
{
    public int shoeNumber;
    public TextMeshPro tmPro;

    private void OnValidate()
    {
        tmPro.SetText(shoeNumber.ToString());
    }
}
