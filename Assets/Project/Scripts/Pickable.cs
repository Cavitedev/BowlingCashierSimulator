using System;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public Transform pickTransform;

    [HideInInspector] public Vector3 defaultPos;
    public Shoe shoe;

    private void Start()
    {
        defaultPos = pickTransform.localPosition;
    }
}