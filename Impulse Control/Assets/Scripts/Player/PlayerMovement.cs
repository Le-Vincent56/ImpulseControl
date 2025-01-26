using ImpulseControl.Input;
using ImpulseControl.Modifiers;
using ImpulseControl.Timers;
using UnityEngine;

namespace ImpulseControl
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private GameInputReader gameInputReader;
        private Rigidbody2D rigidbody2d;
        private LiveModifiers liveModifiers;
        private EmotionSystem emotionSystem;
        private Emotion anger;
        private Emotion fear;
        
        [Header("Dashing")]
        [SerializeField] private bool dashing;
        [SerializeField] private CountdownTimer dashTime;
        [SerializeField] private Vector2 dashDirection;
        [SerializeField] private float dashSpeed;

        private void Awake()
        {
            // Get components
            rigidbody2d = GetComponent<Rigidbody2D>();
            liveModifiers = GetComponent<LiveModifiers>();
            emotionSystem = GetComponent<EmotionSystem>();
            anger = emotionSystem.Anger;
            fear = emotionSystem.Fear;
            

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
            
            if (fear.EmotionState == EmotionStates.ExhaustedFear)
            {
                Move(0, 0,0);
                return;
            }

            if (anger.EmotionState == EmotionStates.Exhausted)
            {
                Move(gameInputReader.NormMoveX, gameInputReader.NormMoveY, liveModifiers.Anger.exhaustionMoveSpeed);
                return;
            }

            if (fear.EmotionState == EmotionStates.CrashingOut)
            {
                Move(gameInputReader.NormMoveX, gameInputReader.NormMoveY, liveModifiers.Player.moveSpeed + liveModifiers.Fear.crashOutMoveSpeedIncrease);
                return;
            }

            Move(gameInputReader.NormMoveX, gameInputReader.NormMoveY, liveModifiers.Player.moveSpeed);
        }
        
        private void Move(float xNorm, float yNorm, float speed)
        {
            rigidbody2d.velocity = new Vector2(xNorm * speed,
                yNorm * speed); 
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
