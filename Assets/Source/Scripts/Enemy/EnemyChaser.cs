using UnityEngine;

namespace Source.Scripts.Enemy
{
    public class EnemyChaser : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _stoppingDistance;

        private Rigidbody _rigidbody;
        private Transform _transform;
        private Vector3 _chaseSpeed;
        private Quaternion _targetRotation;
        
        public void Initialize(Rigidbody rigidbody, Transform transform)
        {
            _rigidbody = rigidbody;
            _transform = transform;
        }
        
        public void MoveToTarget(Transform target)
        {
            float distanceToPlayer = Vector3.Distance(_transform.position, target.position);
            float currentSpeed = Mathf.Lerp(0, _moveSpeed, (distanceToPlayer - _stoppingDistance) / _stoppingDistance);
        
            _chaseSpeed = (target.position - transform.position).normalized;
            _chaseSpeed *= currentSpeed;
            _chaseSpeed.y = _rigidbody.velocity.y; 
        
            _rigidbody.velocity = _chaseSpeed;
        }

        public void RotateToTarget(Transform target)
        {
            _targetRotation = Quaternion.LookRotation(_chaseSpeed);
            
            _transform.rotation = Quaternion.Slerp(_transform.rotation, _targetRotation,
                _moveSpeed * Time.fixedDeltaTime);
        }
    }
}