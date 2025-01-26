using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float startingHealth = 100f;
        private float currentHealth;
        public Action Death;

        // Start is called before the first frame update
        void Start()
        {
            currentHealth = startingHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0) Death?.Invoke();
        }
    }
}
