using ImpulseControl.Modifiers;
using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    [CreateAssetMenu(fileName = "Anger Spell", menuName = "Spells/Anger Spell")]
    public class AngerSpellStrategy : SpellStrategy
    {
        /// <summary>
        /// Cast the Anger Spell
        /// </summary>
        public override void Cast()
        {
            Debug.Log("Cast the Anger Spell");
        }
    }
}
