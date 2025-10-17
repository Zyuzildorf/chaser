using UnityEngine;

namespace Source.Scripts.Player
{
    public class InputReader
    {
        private const string XRotate = "Mouse X";
        private const string YRotate = "Mouse Y";
        private const string VerticalAxis = "Vertical";
        private const string HorizontalAxis = "Horizontal";
        
        public float GetMouseX() => Input.GetAxis(XRotate);
        public float GetMouseY() => Input.GetAxis(YRotate);
        public float GetVerticalAxis() => Input.GetAxis(VerticalAxis);
        public float GetHorizontalAxis() => Input.GetAxis(HorizontalAxis);
        public bool IsJumpPressed() => Input.GetKeyDown(KeyCode.Space);
        public bool IsSprintPressed() => Input.GetKey(KeyCode.LeftShift);
    }
}