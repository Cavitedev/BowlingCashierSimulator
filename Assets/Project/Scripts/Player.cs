using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instance;
    private bool _pickingSth;
    public float pickDst = 1f;

    private Pickable _pickedObject;

    public Pickable PickedObject
    {
        get => _pickedObject;
        private set
        {
            _pickedObject?.OnDetach();
            
            _pickedObject = value;
            _pickedObject?.OnAttach();
        }
    }
    
    private void Awake()
    {
        Instance = this;
    }

    public bool IsPicking() => _pickedObject != null;
    public bool IsPicking(Pickable pickable) => _pickedObject == pickable;
    
    public void Attach(Pickable pickable)
    {
        PickedObject = pickable;
        Transform otherTransform = pickable.pickTransform;
        
        otherTransform.SetParent(transform);
        otherTransform.position = transform.position + transform.forward * pickDst;
    }
    
    public void Detach(Transform parent)
    {
        PickedObject?.pickTransform.SetParent(parent);
        PickedObject = null;
    }
}
