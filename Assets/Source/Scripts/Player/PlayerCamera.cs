using UnityEngine;

namespace Source.Scripts.Player
{
    [System.Serializable]
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private float _verticalTurnSensitivity = 10f;
        [SerializeField] private float _horizontalTurnSensitivity = 10f;
        [SerializeField] private float _verticalMinAngle = -89f;
        [SerializeField] private float _verticalMaxAngle = 89f;

        private Transform _cameraTransform;
        private Transform _playerTransform;
        private InputReader _inputReader;
        private float _cameraAngle;

        public void Initialize(Transform cameraTransform, Transform playerTransform, InputReader inputReader)
        {
            _cameraTransform = cameraTransform;
            _playerTransform = playerTransform;
            _inputReader = inputReader;
            _cameraAngle = _cameraTransform.localEulerAngles.x;
        }

        public void HandleRotation()
        {
            float mouseX = _inputReader.GetMouseX() * _horizontalTurnSensitivity;
            float mouseY = _inputReader.GetMouseY() * _verticalTurnSensitivity;

            _cameraAngle -= mouseY;
            _cameraAngle = Mathf.Clamp(_cameraAngle, _verticalMinAngle, _verticalMaxAngle);
            _cameraTransform.localEulerAngles = Vector3.right * _cameraAngle;

            _playerTransform.Rotate(Vector3.up * mouseX);
        }
    }
}