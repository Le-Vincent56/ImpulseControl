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
            // Dispose the timered up timer
            damageCooldownTimer.Dispose();
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            // Create th cooldown timer
            damageCooldownTimer = new CountdownTimer(0.25f);

            currentHealth = startingHealth;
        }

        public virtual bool TakeDamage(float damage)
        {
            // Exit case - within the damage buffer
            if (damageCooldownTimer.IsRunning) return false;

            // Take damage
            currentHealth -= damage;

            // Check for death case
            if (currentHealth <= 0)
            {
                Death?.Invoke();
                if (this.transform.gameObject.tag == "Player") { Debug.Log("Player died a sussy death"); }

                return true;
            }

            // Start the damage coldown timer
            damageCooldownTimer.Start();

            return true;
        }
    }
}
