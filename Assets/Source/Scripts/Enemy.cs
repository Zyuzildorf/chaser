using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _playerTarget;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _stoppingDistance = 2f;
    [SerializeField] private float _stepOffset = 1f;
    [SerializeField] private LayerMask _ground;
    private Rigidbody _rigidbody;
    public float stepHeight = 0.5f; // Максимальная высота ступеньки
    public float stepCheckDistance = 0.5f; // Дистанция проверки ступеньки
    public float stepSmoothness = 0.1f; // Плавность подъема

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ChasePlayer();
    }


    private void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _playerTarget.position);
        float currentSpeed = Mathf.Lerp(0, _moveSpeed, (distanceToPlayer - _stoppingDistance) / _stoppingDistance);
        
        Vector3 chaseSpeed = (_playerTarget.position - transform.position).normalized;
        chaseSpeed *= currentSpeed;
        chaseSpeed.y = _rigidbody.velocity.y; 
        
       _rigidbody.velocity = chaseSpeed;
       

        // Поворачиваем противника в сторону движения
        if (chaseSpeed != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(chaseSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                _moveSpeed * Time.fixedDeltaTime);
        }
    }
    void HandleStepClimbing()
    {
        // Проверяем наличие ступеньки впереди
        if (CheckForSteps(out Vector3 stepTargetPosition))
        {
            ClimbStep(stepTargetPosition);
        }
    }
    void ClimbStep(Vector3 stepTargetPosition)
    {
        // Плавное перемещение к целевой позиции на ступеньке
        Vector3 newPosition = Vector3.Lerp(transform.position, stepTargetPosition, stepSmoothness);
        _rigidbody.MovePosition(newPosition);
        
    }
    private bool CheckForSteps(out Vector3 targetPosition)
    {
        targetPosition = Vector3.zero;
        
        // Луч вниз от позиции немного впереди и выше
        Vector3 rayStart = transform.position + transform.forward * stepCheckDistance + Vector3.up * 0.1f;
        
        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, stepHeight + 0.2f))
        {
            // Проверяем высоту препятствия
            float heightDifference = transform.position.y - hit.point.y;
            
            if (heightDifference < stepHeight && heightDifference > 0.05f)
            {
                // Проверяем, что перед ступенькой нет препятствия
                if (!Physics.Raycast(transform.position + Vector3.up * 0.1f, transform.forward, stepCheckDistance))
                {
                    targetPosition = new Vector3(transform.position.x, hit.point.y, transform.position.z);
                    return true;
                }
            }
        }
        
        return false;
    }
    
}