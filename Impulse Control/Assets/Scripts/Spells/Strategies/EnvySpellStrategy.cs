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
            SpellObject spell = spellPool.Pool.Get();
            
            // Set damage
        }
    }
}
