using UnityEngine;

namespace Source.Scripts.Player
{
    [System.Serializable]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _strafeSpeed = 4f;
        [SerializeField] private float _sprintSpeed = 8f;
        
        private Transform _cameraTransform;
        private InputReader _inputReader;

        public void Initialize(Transform cameraTransform, InputReader inputReader)
        {
            _cameraTransform = cameraTransform;
            _inputReader = inputReader;
        }

        public Vector3 HandleMovement()
        {
            Vector3 forward = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
            Vector3 right = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized;

            float verticalInput = _inputReader.GetVerticalAxis();
            float horizontalInput = _inputReader.GetHorizontalAxis();
            
            Vector3 movement = forward * verticalInput + right * horizontalInput;
            
            float currentSpeed = _inputReader.IsSprintPressed() ? _sprintSpeed : _speed;
            Vector3 playerSpeed = movement * currentSpeed;

            return playerSpeed;
        }
    }
}