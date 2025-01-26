using ImpulseControl.Events;
using ImpulseControl.Modifiers;
using UnityEngine;

namespace ImpulseControl.Spells.Objects
{
    public class EnvySpell : SpellObject
    {
        private Transform followTarget;
        [SerializeField] private LayerMask enemyLayer;

        private float damageIncreasePercentage;
        private float radiusIncrease;
        private bool crashedOut;
        
        private void OnTriggerStay2D(Collider2D collision)
        {
            Debug.Log($"Colliding with: {collision.gameObject.name}");
            if (collision.gameObject.layer == enemyLayer)
            {
                if(crashedOut)
                    collision.gameObject.GetComponent<Health>().TakeDamage(damage * Time.deltaTime);
                else
                    collision.gameObject.GetComponent<Health>().TakeDamage(damage * Time.deltaTime * damageIncreasePercentage);
            }
        }

        public void SetAttributes(Emotion emotion, float percentage, float radius)
        {
            this.emotion = emotion;
            crashedOut = emotion.EmotionState == EmotionStates.CrashingOut;
            if (crashedOut)
            {
                transform.localScale = new Vector3(transform.localScale.x + radiusIncrease, transform.localScale.y + radiusIncrease, 1.0f);
                return;
            }

  
        }

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
