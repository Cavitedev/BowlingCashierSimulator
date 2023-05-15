using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMovementJeff : MonoBehaviour
{

    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Forward", 0);
        _animator.SetFloat("Turn", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
