using UnityEngine;

namespace ImpulseControl.Spells.Objects
{
    public abstract class SpellObject : MonoBehaviour
    {
        protected SpellPool spellPool;
        [SerializeField] protected float damage;

        protected virtual void OnDestroy() { }

        /// <summary>
        /// Initialize the Spell Object
        /// </summary>
        public virtual void Initialize(SpellPool spellPool)
        {
            // Set the Spell Pool
            this.spellPool = spellPool;
        }

        /// <summary>
        /// Activate the Spell Object
        /// </summary>
        public abstract void Activate(Vector2 direction);

        /// <summary>
        /// Deactivate the Spell Object
        /// </summary>
        public virtual void Deactivate()
        {
            // Release the Spell Object from the Spell Pool
            spellPool.Pool.Release(this);
        }

        /// <summary>
        /// Update the Spell Object
        /// </summary>
        public abstract void TickUpdate(float time, float deltaTime);
    }
}
