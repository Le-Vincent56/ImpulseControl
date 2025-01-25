using ImpulseControl.Modifiers;
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
            Debug.Log("Cast the Envy Spell");
        }
    }
}
