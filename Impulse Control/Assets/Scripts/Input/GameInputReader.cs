using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static ImpulseControl.Input.GameInputActions;

namespace ImpulseControl.Input
{
    [CreateAssetMenu(fileName = "GameInputReader", menuName = "Input/GameInputReader")]
    public class GameInputReader : ScriptableObject, IGameplayActions
    {
        public event UnityAction<Vector2, bool> Move = delegate { };
        public event UnityAction<int, bool> SwapSpell = delegate { };
        public event UnityAction<bool> CastSpell = delegate { };

        public int NormMoveX { get; private set; }
        public int NormMoveY { get; private set; }

        public int NormSwapX { get; private set; }

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
                inputActions.Gameplay.SetCallbacks(this);
            }

            // Enable the input actions
            inputActions.Enable();
        }

        /// <summary>
        /// Disable the input actions
        /// </summary>
        public void Disable() => inputActions.Disable();

        /// <summary>
        /// Callback function for movement handling
        /// </summary>
        public void OnMove(InputAction.CallbackContext context)
        {
            // Get the raw movement input from the control
            Vector2 rawMovementInput = context.ReadValue<Vector2>();

            // Invoke the movement event
            Move.Invoke(rawMovementInput, context.started);

            // Set variables
            NormMoveX = (int)(rawMovementInput * Vector2.right).normalized.x;
            NormMoveY = (int)(rawMovementInput * Vector2.up).normalized.y;
        }

        /// <summary>
        /// Callback function for Spell Swapping
        /// </summary>
        public void OnSwapSpell(InputAction.CallbackContext context)
        {
            // Get the raw swap input from the control
            Vector2 rawSwapInput = context.ReadValue<Vector2>();

            // Extract the swap direction
            int swapDirection = (int)(rawSwapInput * Vector2.right).normalized.x;

            // Check the context phase
            switch (context.phase)
            {
                // If starting, invoke with true
                case InputActionPhase.Started:
                    // Invoke the swap event
                    SwapSpell.Invoke(swapDirection, true);
                    break;

                // If canceled, invoke with false
                case InputActionPhase.Canceled:
                    // Invoke the swap event
                    SwapSpell.Invoke(swapDirection, false);
                    break;
            }
        }

        /// <summary>
        /// Callback function for Spell Casting
        /// </summary>
        public void OnCastSpell(InputAction.CallbackContext context)
        {
            // Check the context phase
            switch (context.phase)
            {
                // If starting, invoke with true
                case InputActionPhase.Started:
                    CastSpell.Invoke(true);
                    break;

                // If canceled, invoke with false
                case InputActionPhase.Canceled:
                    CastSpell.Invoke(false);
                    break;
            }
        }
    }
}
