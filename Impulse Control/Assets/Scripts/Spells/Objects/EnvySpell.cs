using UnityEngine;

namespace ImpulseControl.Spells.Objects
{
    public class EnvySpell : SpellObject
    {
        private Transform followTarget;

        /// <summary>
        /// Initialize the Envy Spell
        /// </summary>
        public override void Initialize(SpellPool spellPool)
        {
            // Call the parent Initialize()
            base.Initialize(spellPool);
        }

        /// <summary>
        /// Activate the Envy Spell
        /// </summary>
        public override void Activate(Vector2 direction)
        {

        }

        /// <summary>
        /// Deactivate the Envy Spell
        /// </summary>
        public override void Deactivate()
        {
            // Call the parent Deactivate()
            base.Deactivate();
        }

        /// <summary>
        /// Update the Envy Spell
        /// </summary>
        public override void TickUpdate(float time, float delta)
        {
            // Match the follow target's position
            transform.position = followTarget.position;
        }

        /// <summary>
        /// Set the target to follow
        /// </summary>
        public void SetTarget(Transform target)
        {
            // Set the follow target
            followTarget = target;
        }
    }
}
