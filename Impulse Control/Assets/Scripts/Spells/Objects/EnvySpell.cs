using ImpulseControl.Timers;
using UnityEngine;

namespace ImpulseControl.Spells.Objects
{
    public class EnvySpell : SpellObject
    {
        private HealthPlayer health;
        private Transform followTarget;
        private Vector3 scale;
        [SerializeField] private LayerMask enemyLayer;

        private float damageIncreasePercentage;
        private float radiusIncrease;
        private float healPercentage;
        private bool crashedOut;
        
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.Equals(health.gameObject)) return;
            if (!collision.gameObject.TryGetComponent(out Health enemyHealth)) return;

            // Deal damage
            if (!enemyHealth.TakeDamage(damage)) return;

            // Heal the player
            health.Heal(damage * healPercentage);
        }

        public void SetAttributes(Emotion emotion, float damage, float radius, float crashOutDamage, float crashOutRadius, float healPercentage)
        {
            // Reset scale 
            // transform.localScale = scale;
            this.emotion = emotion;

            crashedOut = emotion.EmotionState == EmotionStates.CrashingOut;
            if (crashedOut)
            {
                // Set radius and damage
                radiusIncrease = crashOutRadius;
                this.damage = crashOutDamage;

                // Set the radius
                transform.localScale = new Vector3(scale.x * radiusIncrease, scale.y * radiusIncrease, 1.0f);

                return;
            }

            // Set default radius and damage
            radiusIncrease = radius;
            this.damage = damage;

            // Set the radius
            transform.localScale = new Vector3(scale.x * radiusIncrease, scale.x * radiusIncrease, 1.0f);
        }

        /// <summary>
        /// Initialize the Envy Spell
        /// </summary>
        public override void Initialize(SpellPool spellPool)
        {
            // Call the parent Initialize()
            base.Initialize(spellPool);

            scale = transform.localScale;
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
        public void SetTarget(HealthPlayer target)
        {
            // Set the follow target
            health = target;
            followTarget = (health == null) ? null : target.transform;
        }
    }
}
