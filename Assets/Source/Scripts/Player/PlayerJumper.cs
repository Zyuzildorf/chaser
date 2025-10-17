using UnityEngine;

namespace Source.Scripts.Player
{
    public class PlayerJumper : MonoBehaviour
    {
        [SerializeField] private float _jumpSpeed = 8f;
        [SerializeField] private float _gravityFactor = 2f;

        private InputReader _inputReader;
        private Vector3 _verticalVelocity;
        private Vector3 _gravityValue;

        public void Initialize(InputReader inputReader)
        {
            _inputReader = inputReader;
            
            _verticalVelocity = Vector3.zero;
            _gravityValue = Physics.gravity;
        }

        public Vector3 HandleJumpAndGravity(bool isGrounded)
        {
            if (isGrounded)
            {
                if (_verticalVelocity.y < 0)
                {
                    _verticalVelocity.y = _gravityValue.y;
                }

                if (_inputReader.IsJumpPressed())
                {
                    _verticalVelocity.y = _jumpSpeed;
                }
            }
            else
            {
                _verticalVelocity.y += _gravityValue.y * _gravityFactor * Time.deltaTime;
            }

            return _verticalVelocity;
        }
    }
}