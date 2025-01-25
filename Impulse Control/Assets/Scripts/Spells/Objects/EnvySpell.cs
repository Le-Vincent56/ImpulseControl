using UnityEngine;

namespace ImpulseControl.Spells.Objects
{
    public class EnvySpell : SpellObject
    {
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

        }
    }
}
