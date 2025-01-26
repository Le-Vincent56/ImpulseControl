using ImpulseControl.Timers;
using UnityEngine;

namespace ImpulseControl.Spells.Objects
{
    public class AngerSpell : SpellObject
    {
        protected CountdownTimer livingTimer;
        [SerializeField] private float livingTime;
        [SerializeField] private Vector2 direction;
        private Vector3 initialScale;
        private Vector3 initialPosition;

        protected override void OnDestroy()
        {
            // Dispose of the Timer
            livingTimer.Dispose();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
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

            livingTimer.OnTimerTick += ExtendHitbox;
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
        public override void TickUpdate(float time, float delta) { /* Noop */ }

        /// <summary>
        /// Set the position of the spell
        /// </summary>
        public void SetTransform(Vector2 upVector, float offset)
        {
            // Offset towards the direction
            transform.Translate(direction * offset);

            // Rotate towards the direction
            transform.rotation = Quaternion.LookRotation(Vector3.back, direction);

            // Set the initial position
            initialPosition = transform.position;
        }

        public void SetAttributes(float damage)
        {
            this.damage = damage;
        }

        /// <summary>
        /// Extend the length of the hitbox
        /// </summary>
        private void ExtendHitbox()
        {
            // Calculate the length
            float currentLength = Mathf.Lerp(0f, 3f, 1 - livingTimer.Progress);

            // Adjust the height and the position
            AdjustHeight(currentLength);
            AdjustPosition(currentLength);
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
