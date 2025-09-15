using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), _rigidbody.velocity.y, Input.GetAxis("Vertical"));
        direction *= _speed;

        _rigidbody.velocity = direction;
        _rigidbody.velocity += Physics.gravity;
    }
}
