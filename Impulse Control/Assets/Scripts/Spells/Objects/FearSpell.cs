using ImpulseControl.Timers;
using UnityEngine;

namespace ImpulseControl.Spells.Objects
{
    public class FearSpell : SpellObject
    {
        protected CountdownTimer livingTimer;
        [SerializeField] private float livingTime;
        [SerializeField] private Vector2 direction;

        protected override void OnDestroy()
        {
            // Dispose of the Timer
            livingTimer.Dispose();
        }

        /// <summary>
        /// Initialize the Fear Spell
        /// </summary>
        public override void Initialize(SpellPool spellPool)
        {
            // Call the parent Initialize()
            base.Initialize(spellPool);

            // Initialize the Timer
            livingTimer = new CountdownTimer(livingTime);

            livingTimer.OnTimerStop += Deactivate;
        }

        /// <summary>
        /// Activate the Fear Spell
        /// </summary>
        public override void Activate(Vector2 direction)
        {
            // Set the direction
            this.direction = direction;

            // Start the living timer
            livingTimer.Start();
        }

        /// <summary>
        /// Deactivate the Fear Spell
        /// </summary>
        public override void Deactivate()
        {
            // Ensure the living timer is stopped
            livingTimer.Stop();

            // Call the parent Deactivate()
            base.Deactivate();
        }

        /// <summary>
        /// Update the Fear Spell
        /// </summary>
        public override void TickUpdate(float time, float delta)
        {
            transform.Translate(2f * delta * direction);
        }
    }
}
