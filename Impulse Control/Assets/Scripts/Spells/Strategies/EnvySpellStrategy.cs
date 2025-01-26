using ImpulseControl.Modifiers;
using ImpulseControl.Spells.Objects;
using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    [CreateAssetMenu(fileName = "Envy Spell", menuName = "Spells/Envy Spell")]
    public class EnvySpellStrategy : SpellStrategy
    {
        /// <summary>
        /// Cast the Envy Spell
        /// </summary>
        public override void Cast()
        {
            // Get an envy spell
            EnvySpell spell = (EnvySpell)spellPool.Pool.Get();

            // Set the follow transform of the Envy Spell
            spell.SetTarget(spellSystem.transform);
        }
    }
}
