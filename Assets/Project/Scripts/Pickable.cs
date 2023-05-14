using System;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public Transform pickTransform;

    public Vector3 defaultPos;

    private void Start()
    {
        defaultPos = pickTransform.localPosition;
    }
}