using ImpulseControl.Timers;
using System;
using UnityEngine;

namespace ImpulseControl
{
    public class Health : MonoBehaviour
    {
        [SerializeField] protected float startingHealth = 100f;
        protected float currentHealth;
        public Action Death;

        private CountdownTimer damageCooldownTimer;

        /// <summary>
        /// The current health of this object
        /// </summary>
        public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
        
        protected virtual void OnDestroy()
        {

        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            // Create th cooldown timer
            damageCooldownTimer = new CountdownTimer(0.25f);

            currentHealth = startingHealth;
        }

        public virtual void TakeDamage(float damage)
        {
            // Exit case - within the damage buffer
            if (damageCooldownTimer.IsRunning) return;

            // Take damage
            currentHealth -= damage;

            // Check for death case
            if (currentHealth <= 0)
            {
                Death?.Invoke();
                if (this.transform.gameObject.tag == "Player") { Debug.Log("Player died a sussy death"); }

                return;
            }

            // Start the damage coldown timer
            damageCooldownTimer.Start();
        }
    }
}
