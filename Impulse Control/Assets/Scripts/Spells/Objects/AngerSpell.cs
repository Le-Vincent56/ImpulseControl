using ImpulseControl.AI;
using ImpulseControl.Timers;
using UnityEngine;

namespace ImpulseControl.Spells.Objects
{
    public class AngerSpell : SpellObject
    {
        protected CountdownTimer livingTimer;
        [SerializeField] private bool crashing;
        [SerializeField] private float livingTime;
        [SerializeField] private Vector2 direction;
        [SerializeField] private Vector2 translateVector;
        [SerializeField] private float projectileSpeed;
        private Vector3 initialScale;
        private Vector3 initialPosition;
        [SerializeField] private LayerMask enemyLayer;

        protected override void OnDestroy()
        {
            // Dispose of the Timer
            livingTimer.Dispose();
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.layer == enemyLayer)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
        }

        /// <summary>
        /// Initialize the Anger Spell
        /// </summary>
        public override void Initialize(SpellPool spellPool)
        {
            // Call the parent Initialize()
            base.Initialize(spellPool);

            // Set variables
            initialScale = transform.localScale;

            // Initialize the Timer
            livingTimer = new CountdownTimer(livingTime);

            livingTimer.OnTimerStop += Deactivate;
        }

        /// <summary>
        /// Activate the Anger Spell
        /// </summary>
        public override void Activate(Vector2 direction)
        {
            // Set the direction
            this.direction = direction;

            // Set the initial scale
            transform.localScale = initialScale;

            // Start the living timer
            livingTimer.Start();
        }

        /// <summary>
        /// Deactivate the Anger Spell
        /// </summary>
        public override void Deactivate()
        {
            // Ensure the living timer is stopped
            livingTimer.Stop();

            // Call the parent Deactivate()
            base.Deactivate();
        }

        /// <summary>
        /// Update the Anger Spell
        /// </summary>
        public override void TickUpdate(float time, float delta)
        {
            if(crashing)
            {
                transform.Translate(projectileSpeed * delta * translateVector);

                return;
            }

            // Calculate the length
            float currentLength = Mathf.Lerp(0f, 3f, 1 - livingTimer.Progress);

            // Adjust the height and the position
            AdjustHeight(currentLength);
            AdjustPosition(currentLength);
        }

        /// <summary>
        /// Set the position of the spell
        /// </summary>
        public void SetTransform(float offset)
        {
            // Offset towards the direction
            transform.Translate(direction * offset);

            Quaternion rotation = Quaternion.LookRotation(Vector3.back, direction);

            // Rotate towards the direction
            transform.rotation = rotation;
            translateVector = rotation * direction;

            // Set the initial position
            initialPosition = transform.position;
        }

        /// <summary>
        /// Set the Attributes for the Anger Spell
        /// </summary>
        public void SetAttributes(Emotion emotion, PlayerMovement playerMovement, float damage, float dashDistance, float projectileSpeed,
            float offset, float crashOffset, float crashOutLifetime)
        {
            this.emotion = emotion;
            this.damage = damage;

            // Check if crashing
            crashing = emotion.EmotionState == EmotionStates.CrashingOut;
            Debug.Log($"Crashing: {crashing}");
            //crashing = true;

            // Exit case - if crashing
            if (crashing)
            {
                // Set the projectile speed
                this.projectileSpeed = projectileSpeed;

                // Set the transform
                SetTransform(crashOffset);

                // Set the new lifetime
                livingTimer.Reset(crashOutLifetime);

                return;
            }

            // Set the projectile speed
            this.projectileSpeed = 0f;

            // Set the transform
            SetTransform(offset);

            // Set the lifetime
            livingTimer.Reset(livingTime);

            // Translate the player
            playerMovement.SetDash(direction, dashDistance);
        }

        /// <summary>
        /// Adjust the height of the hitbox
        /// </summary>
        private void AdjustHeight(float height)
        {
            // Retrieve the original scale
            Vector3 newScale = initialScale;

            // Scale the y-value based on the calculated height
            newScale.y = height;

            // Set the new scale
            transform.localScale = newScale;
        }

        /// <summary>
        /// Adjust the position of the hitbox
        /// </summary>
        private void AdjustPosition(float height)
        {
            // Set the initial position
            Vector3 newPosition = initialPosition;

            // Get the length to adjust by
            float length = height / 2f;

            // Multiply the length by the normalized direction components
            newPosition.x += length * direction.x;
            newPosition.y += length * direction.y;

            // Set the new position
            transform.position = newPosition;
        }
    }
}
