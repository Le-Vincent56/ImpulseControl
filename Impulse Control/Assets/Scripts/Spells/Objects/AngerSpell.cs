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
            // Damage
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

            livingTimer.OnTimerTick += Grow;
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
        public override void TickUpdate(float time, float delta) { /* Noop */}

        private void Grow()
        {
            float currentLength = Mathf.Lerp(0f, 3f, 1 - livingTimer.Progress);
            AdjustHeight(currentLength);
            AdjustPosition(currentLength);
        }

        public void SetPosition(Vector2 upVector, float offset)
        {
            // Offset towards the direction
            transform.Translate(direction * offset);

            // Rotate towards the direction
            transform.rotation = Quaternion.LookRotation(Vector3.back, direction);

            // Set the initial position
            initialPosition = transform.position;
        }

        private void AdjustHeight(float height)
        {
            // Retrieve the original scale
            Vector3 newScale = initialScale;

            // Scale the y-value based on the calculated height
            newScale.y = height;

            // Set the new scale
            transform.localScale = newScale;
        }

        private void AdjustPosition(float height)
        {
            Vector3 newPosition = initialPosition;

            float length = height / 2f;

            newPosition.x += length * direction.x;
            newPosition.y += length * direction.y;

            transform.position = newPosition;
        }
    }
}
