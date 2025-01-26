using ImpulseControl.Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static ImpulseControl.Input.GameInputActions;
using static ImpulseControl.UIInputReader;

namespace ImpulseControl
{
    [CreateAssetMenu(fileName = "UI Input Reader", menuName = "Input/UI Input Reader")]
    public class UIInputReader : ScriptableObject, IUIActions
    {
        public UnityAction<Vector2> Navigate = delegate { };
        public UnityAction<Vector2> Point = delegate { };
        public UnityAction<Vector2> Scrollwheel = delegate { };
        public UnityAction<bool> Submit = delegate { };
        public UnityAction<bool> Cancel = delegate { };
        public UnityAction<bool> Click = delegate { };
        public UnityAction<bool> RightClick = delegate { };
        public UnityAction<bool> MiddleClick = delegate { };
        public UnityAction<bool> AnyKeyPressed = delegate { };
        public UnityAction<bool> PanSkillTree = delegate { };

        private GameInputActions inputActions;

        private void OnEnable() => Enable();

        private void OnDisable() => Disable();

        /// <summary>
        /// Enable the input actions
        /// </summary>
        public void Enable()
        {
            // Check if the input actions have been initialized
            if (inputActions == null)
            {
                // Initialize the input actions and set callbacks
                inputActions = new GameInputActions();
                inputActions.UI.SetCallbacks(this);
            }

            // Enable the input actions
            inputActions.Enable();
        }

        /// <summary>
        /// Disable the input actions
        /// </summary>
        public void Disable() => inputActions.Disable();

        /// <summary>
        /// Set the UI Actions
        /// </summary>
        public void Set()
        {
            // Exit case - the UI actions are already enabled
            if (inputActions.UI.enabled) return;

            // Disable all other actions
            inputActions.Gameplay.Disable();

            // Enable the UI actions
            inputActions.UI.Enable();
        }


        public void OnNavigate(InputAction.CallbackContext context)
        {
            // Invoke the event and pass in the read value
            Navigate.Invoke(context.ReadValue<Vector2>());
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
            // Invoke the event and pass in the read value
            Point.Invoke(context.ReadValue<Vector2>());
        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {
            // Invoke the event and pass in the read value
            Scrollwheel.Invoke(context.ReadValue<Vector2>());
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
            // Check the context phase
            switch (context.phase)
            {
                // If starting, invoke with true
                case InputActionPhase.Started:
                    Submit.Invoke(true);
                    break;

                // If canceled, invoke with false
                case InputActionPhase.Canceled:
                    Submit.Invoke(false);
                    break;
            }
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            // Check the context phase
            switch (context.phase)
            {
                // If starting, invoke with true
                case InputActionPhase.Started:
                    Cancel.Invoke(true);
                    break;

                // If canceled, invoke with false
                case InputActionPhase.Canceled:
                    Cancel.Invoke(false);
                    break;
            }
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            // Check the context phase
            switch (context.phase)
            {
                // If starting, invoke with true
                case InputActionPhase.Started:
                    Click.Invoke(true);
                    break;

                // If canceled, invoke with false
                case InputActionPhase.Canceled:
                    Click.Invoke(false);
                    break;
            }
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            // Check the context phase
            switch (context.phase)
            {
                // If starting, invoke with true
                case InputActionPhase.Started:
                    RightClick.Invoke(true);
                    break;

                // If canceled, invoke with false
                case InputActionPhase.Canceled:
                    RightClick.Invoke(false);
                    break;
            }
        }

        public void OnMiddleClick(InputAction.CallbackContext context)
        {
            // Check the context phase
            switch (context.phase)
            {
                // If starting, invoke with true
                case InputActionPhase.Started:
                    MiddleClick.Invoke(true);
                    break;

                // If canceled, invoke with false
                case InputActionPhase.Canceled:
                    MiddleClick.Invoke(false);
                    break;
            }
        }

        public void OnAnyKeyPressed(InputAction.CallbackContext context)
        {
            // Check the context phase
            switch (context.phase)
            {
                // If starting, invoke with true
                case InputActionPhase.Started:
                    AnyKeyPressed.Invoke(true);
                    break;

                // If canceled, invoke with false
                case InputActionPhase.Canceled:
                    AnyKeyPressed.Invoke(false);
                    break;
            }
        }

        public void OnPanSkillTree(InputAction.CallbackContext context)
        {
            // Check the context phase
            switch (context.phase)
            {
                // If starting, invoke with true
                case InputActionPhase.Started:
                    PanSkillTree.Invoke(true);
                    break;

                // If canceled, invoke with false
                case InputActionPhase.Canceled:
                    PanSkillTree.Invoke(false);
                    break;
            }
        }

        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context) { }

        public void OnTrackedDevicePosition(InputAction.CallbackContext context) { }
    }
}
