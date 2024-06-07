using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrototype : MonoBehaviour
{
    public  float       moveSpeed = 5.0f;
    private Rigidbody2D _rigidBody2D;
    private Vector2     _moveAmount;
    private Animator    _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator    = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal");
        float deltaY = Input.GetAxis("Vertical");

        Vector3 velocity = Vector3.zero;
        velocity.x            = deltaX;
        velocity.y            = deltaY;
        _rigidBody2D.velocity = velocity * moveSpeed;

        if (velocity.x != 0.0f)
        {
            bool flipped = velocity.x > 0.0f;
            this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, ( flipped ? 180.0f : 0.0f ), 0.0f));
        }

        _animator.SetFloat("Speed",Mathf.Abs(velocity.magnitude * moveSpeed));
    }
    
}
