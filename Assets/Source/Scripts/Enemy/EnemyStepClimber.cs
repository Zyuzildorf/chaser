using UnityEngine;

namespace Source.Scripts.Enemy
{
    public class EnemyStepClimber : MonoBehaviour
    {
        [SerializeField] private float _stepHeight = 0.5f; 
        [SerializeField] private float _stepCheckDistance = 0.5f; 
        [SerializeField] private float _stepSmoothness = 0.1f;
        
        [SerializeField] private float _raycastOffset = 0.1f;
        [SerializeField] private float _raycastDistanceExtension = 0.2f;
        [SerializeField] private float _minHeightDifference = 0.05f;
        [SerializeField] private float _forwardCheckOffset = 0.1f;

        private Rigidbody _rigidbody;
        private Transform _transform;
        
        public void Initialize(Rigidbody enemyRigidbody, Transform enemyTransform)
        {
            _rigidbody = enemyRigidbody;
            _transform = enemyTransform;
        }
        
        public void HandleStepClimbing()
        {
            if (CheckForSteps(out Vector3 stepTargetPosition))
            {
                ClimbStep(stepTargetPosition);
            }
        }
        
        private void ClimbStep(Vector3 stepTargetPosition)
        {
            Vector3 newPosition = Vector3.Lerp(_transform.position, stepTargetPosition, _stepSmoothness);
            
            _rigidbody.MovePosition(newPosition);
        }
        
        private bool CheckForSteps(out Vector3 targetPosition)
        {
            targetPosition = Vector3.zero;
        
            Vector3 rayStart = transform.position + transform.forward * _stepCheckDistance + Vector3.up * _raycastOffset;
        
            if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, _stepHeight + _raycastDistanceExtension))
            {
                float heightDifference = transform.position.y - hit.point.y;
            
                if (heightDifference < _stepHeight && heightDifference > _minHeightDifference)
                {
                    if (!Physics.Raycast(transform.position + Vector3.up * _forwardCheckOffset, transform.forward, _stepCheckDistance))
                    {
                        targetPosition = new Vector3(transform.position.x, hit.point.y, transform.position.z);
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}