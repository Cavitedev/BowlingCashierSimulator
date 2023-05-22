using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instance;
    private bool _pickingSth;
    public float pickDst = 1f;
    public float minifyingFactor = 5f;
    
    private Pickable _pickedObject;

    public Pickable PickedObject
    {
        get => _pickedObject;
        private set
        {

            _pickedObject = value;

        }
    }
    
    private void Awake()
    {
        Instance = this; 
    }

    private void Update()
    {
        if (IsPicking())
        {
            PickedObject.pickTransform.rotation = Quaternion.LookRotation(transform.forward);
        }

    }

    public bool IsPicking() => _pickedObject != null;
    

    public bool IsPicking(Pickable pickable) => _pickedObject == pickable;
    
    public void Attach(Pickable pickable)
    {
        PickedObject = pickable;
        Transform otherTransform = pickable.pickTransform;
        
        otherTransform.SetParent(transform);
        otherTransform.position = transform.position + transform.forward * pickDst;
        otherTransform.localScale /= minifyingFactor;
    }
    
    public void Detach()
    {
        if (PickedObject?.pickTransform != null)
        {

            PickedObject.pickTransform.localScale *= minifyingFactor;
        }

        PickedObject = null;
    }

    public bool IsLookingForward() => transform.rotation.eulerAngles.y is >= 270 or <= 90;
}
