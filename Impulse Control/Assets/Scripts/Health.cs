using System;
using UnityEngine;

namespace ImpulseControl
{
    public class Health : MonoBehaviour
    {
        [SerializeField] protected float startingHealth = 100f;
        protected float currentHealth;
        public Action Death;

        /// <summary>
        /// The current health of this object
        /// </summary>
        public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
        
        // Start is called before the first frame update
        protected virtual void Start()
        {
            currentHealth = startingHealth;
        }

        public virtual void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Death?.Invoke();
                if (this.transform.gameObject.tag == "Player") { Debug.Log("Player died a sussy death"); }
            }
        }
    }
}
