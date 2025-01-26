using ImpulseControl.Input;
using ImpulseControl.Timers;
using UnityEngine;

namespace ImpulseControl
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private GameInputReader gameInputReader;
        [SerializeField] private Rigidbody2D rigidbody2d;

        [Header("Attributes")] 
        [SerializeField] private float speed = 1.0f;

        [Header("Dashing")]
        [SerializeField] private bool dashing;
        [SerializeField] private CountdownTimer dashTime;
        [SerializeField] private Vector2 dashDirection;
        [SerializeField] private float dashSpeed;

        private void Awake()
        {
            // Get components
            rigidbody2d = GetComponent<Rigidbody2D>();

            dashTime = new CountdownTimer(0.25f);

            dashTime.OnTimerStart += () => dashing = true;
            dashTime.OnTimerTick += () =>
            {
                transform.Translate(dashDirection * dashSpeed * Time.deltaTime);
            };
            dashTime.OnTimerStop += () => dashing = false;

            
        }

        private void OnDestroy()
        {
            // Dispose of the Dash Timer
            dashTime.Dispose();
        }

        private void FixedUpdate()
        {
            // Exit case - if dashing
            if (dashing) return;

            Move(gameInputReader.NormMoveX, gameInputReader.NormMoveY);
        }
        
        private void Move(float xNorm, float yNorm)
        {
            rigidbody2d.velocity = new Vector2(xNorm * speed, yNorm * speed);
        }

        /// <summary>
        /// Set the Player to Dash
        /// </summary>
        public void SetDash(Vector2 dashDirection, float dashSpeed)
        {
            // Set variables
            this.dashDirection = dashDirection;
            this.dashSpeed = dashSpeed;

            // Start the dash timer
            dashTime.Start();
        }
    }
}
