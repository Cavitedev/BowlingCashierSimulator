using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instance;
    private bool _pickingSth;
    public float pickDst = 1f;
    
    private void Awake()
    {
        Instance = this;
    }

    public void Attach(Transform otherTransform,  Vector3 center)
    {
        otherTransform.SetParent(transform);
        otherTransform.position = transform.position + transform.forward * pickDst + center;
        
        
    }
}
