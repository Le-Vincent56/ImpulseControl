using UnityEngine;
using UnityEngine.Pool;

namespace ImpulseControl.Spells.Objects
{
    public class SpellPool : MonoBehaviour
    {
        private SpellSystem spellSystem;
        private ObjectPool<SpellObject> pool;
        [SerializeField] private SpellObject spellPrefab;

        public ObjectPool<SpellObject> Pool { get => pool; }

        /// <summary>
        /// Create the Spell Pool
        /// </summary>
        public void CreateSpellPool(SpellSystem spellSystem)
        {
            // Set the Spell System
            this.spellSystem = spellSystem;

            // Create the Spell Pool
            pool = new ObjectPool<SpellObject>(
                CreateSpell,
                OnTakeSpellFromPool,
                OnReturnSpellToPool,
                OnDestroySpell,
                true,
                10,
                1000
            );
        }

        /// <summary>
        /// Create a Spell in the Spell Pool
        /// </summary>
        private SpellObject CreateSpell()
        {
            // Instantiate the Spell
            SpellObject spell = Instantiate(spellPrefab);

            // Initialize the Spell
            spell.Initialize(this);

            return spell;
        }

        /// <summary>
        /// Take a Spell from the Spell Pool
        /// </summary>
        private void OnTakeSpellFromPool(SpellObject spell)
        {
            // Set the transform and rotation
            spell.transform.position = transform.parent.position;
            spell.transform.rotation = transform.parent.rotation;

            // Register the Spell within the Spell System
            spellSystem.RegisterSpell(spell);

            // Activate the Spell
            spell.Activate(spellSystem.SpellDirection);

            // Activate the Spell
            spell.gameObject.SetActive(true);
        }

        /// <summary>
        /// Return a Spell to the Spell Pool
        /// </summary>
        private void OnReturnSpellToPool(SpellObject spell)
        {
            // Deregister the Spell from the Spell System
            spellSystem.DeregisterSpell(spell);

            // Deactivate the Spell
            spell.gameObject.SetActive(false);
        }

        /// <summary>
        /// Handle the destruction of the Spell
        /// </summary>
        private void OnDestroySpell(SpellObject spell)
        {
            // Deregister the Spell from the Spell System
            spellSystem.DeregisterSpell(spell);

            // Destroy the Spell
            Destroy(spell.gameObject);
        }
    }
}
