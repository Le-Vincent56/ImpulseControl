using ImpulseControl.AI;
using ImpulseControl.Timers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImpulseControl.Spells.Objects
{
    public class FearSpell : SpellObject
    {
        protected SpriteRenderer spriteRenderer;
        protected BoxCollider2D boxCollider;
        protected CountdownTimer livingTimer;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private float livingTime;
        [SerializeField] private Vector2 direction;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float chainDistance;
        [SerializeField] private int chainCount;
        private List<GameObject> hitEnemies;


        protected override void OnDestroy()
        {
            // Dispose of the Timer
            livingTimer.Dispose();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Exit case - if the collision is not an enemy
            if (!collision.gameObject.TryGetComponent(out Enemy enemy)) return;

            // Disable the sprite renderer
            spriteRenderer.enabled = false;

            // Prevent further collisions
            boxCollider.enabled = false;

            // Add the collision to the hit enemies list
            hitEnemies.Add(enemy.gameObject);

            // Damage the hit enemy
            enemy.GetComponent<Health>().TakeDamage(damage);

            // Start chaining through enemies
            ChainThroughEnemies(1);
        }

        private void ChainThroughEnemies(int timesToChain)
        {
            if (timesToChain > chainCount) return;
            else
            {
                // Get all enemies within a circle
                List<Collider2D> collisions = Physics2D.OverlapCircleAll(transform.position, chainDistance, enemyLayer)
                                                .Where(collision => !hitEnemies.Contains(collision.gameObject))
                                                .ToList();

                // Exit case - the number of remaining collisions is less than the chain index
                if (collisions.Count < timesToChain) return;

                // Set default values
                GameObject enemyToChain = collisions[0].gameObject;
                float closestDistance = float.MaxValue;

                // Iterate through each collision
                foreach (Collider2D collision in collisions)
                {
                    // Skip over already hit enemies
                    if (hitEnemies.Contains(collision.gameObject)) continue;

                    // Get the distance to the enemy
                    float distanceToEnemy = Vector2.Distance(transform.position, collision.transform.position);

                    // Check if the distance is lower than the closest distance
                    if (distanceToEnemy < closestDistance)
                    {
                        // Update data
                        enemyToChain = collision.gameObject;
                        closestDistance = distanceToEnemy;
                    }
                }

                // Add the enemy to the hit enemies list
                hitEnemies.Add(enemyToChain);

                // Damage the enemy
                enemyToChain.GetComponent<Health>().TakeDamage(damage);

                // Continue to chain through enemies
                ChainThroughEnemies(timesToChain + 1);
            }
        }

        /// <summary>
        /// Initialize the Fear Spell
        /// </summary>
        public override void Initialize(SpellPool spellPool)
        {
            // Call the parent Initialize()
            base.Initialize(spellPool);

            // Get components
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();

            // Initialize the list
            hitEnemies = new List<Enemy>();

            // Initialize the Timer
            livingTimer = new CountdownTimer(livingTime);

            livingTimer.OnTimerStop += Deactivate;
        }

        /// <summary>
        /// Activate the Fear Spell
        /// </summary>
        public override void Activate(Vector2 direction)
        {
            // Enable the sprite renderer
            spriteRenderer.enabled = true;

            // Enable the box collider
            boxCollider.enabled = true;

            // Set the direction
            this.direction = direction;

            // Clear the hit enemies list
            hitEnemies.Clear();

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
        /// Set the Fear Spell object-relevant attributes
        /// </summary>
        public void SetAttributes(float damage, float projectileSpeed, int chainCount, float chainDistance)
        {
            // Set attributes
            this.damage = damage;
            this.projectileSpeed = projectileSpeed;
            this.chainCount = chainCount;
            this.chainDistance = chainDistance;
        }

        /// <summary>
        /// Update the Fear Spell
        /// </summary>
        public override void TickUpdate(float time, float delta)
        {
            transform.Translate(projectileSpeed * delta * direction);
        }
    }
}
