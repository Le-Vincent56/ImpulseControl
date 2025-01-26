using ImpulseControl.Spells.Objects;
using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    [CreateAssetMenu(fileName = "Envy Spell", menuName = "Spells/Envy Spell")]
    public class EnvySpellStrategy : SpellStrategy
    {
        private EnvySpell spell;
        private bool activated;

        /// <summary>
        /// Cast the Envy Spell
        /// </summary>
        public override void Cast()
        {
            // Check if the spell is already active
            if(activated)
            {
                // Nullify the spell's target
                spell.SetTarget(null);

                // Release the Spell
                spellPool.Pool.Release(spell);

                // Set deactivated
                activated = false;
            } 
            // Otherwise
            else
            {
                // Get an envy spell
                spell = (EnvySpell)spellPool.Pool.Get();

                // Set the follow transform of the Envy Spell
                spell.SetTarget(spellSystem.transform);

                // Set activated
                activated = true;
            }
        }
    }
}
