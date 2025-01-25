using UnityEngine;
using UnityEngine.InputSystem;

namespace ImpulseControl.Spells
{
    public class SpellAimer : MonoBehaviour
    {
        private Camera mainCamera;
        [SerializeField] private Vector2 cursorPosition;
        [SerializeField] private Bounds cursorBounds;

        public Vector2 AimDirection { get => (cursorPosition - (Vector2)transform.position).normalized; }

        private void Start()
        {
            mainCamera = Camera.main;

            // Calculate the camera bounds
            CalculateCameraBounds();
        }

        private void Update()
        {
            // Calculate the Camera Bounds
            CalculateCameraBounds();

            // Set the cursor position to the mouse position
            cursorPosition = Camera.main.ScreenToWorldPoint(
                new Vector2(
                    Mouse.current.position.ReadValue().x,
                    Mouse.current.position.ReadValue().y
                )
            );

            // Bind the cursor within the screen
            BindCursor();
        }

        /// <summary>
        /// Calculate the bounds of the Camera
        /// </summary>
        private void CalculateCameraBounds()
        {
            float height = 2f * mainCamera.orthographicSize;
            float width = height * mainCamera.aspect;

            Vector3 center = mainCamera.transform.position;
            Vector3 size = new Vector3(width, height);

            cursorBounds = new Bounds(center, size);
        }

        private void BindCursor()
        {
            // Clamp the cursor position into the bounds of the screen
            cursorPosition.x = Mathf.Clamp(cursorPosition.x, cursorBounds.min.x, cursorBounds.max.x);
            cursorPosition.y = Mathf.Clamp(cursorPosition.y, cursorBounds.min.y, cursorBounds.max.y);
        }
    }
}
