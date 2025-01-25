using ImpulseControl.Modifiers;
using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    [CreateAssetMenu(fileName = "Fear Spell", menuName = "Spells/Fear Spell")]
    public class FearSpellStrategy : SpellStrategy
    {
        /// <summary>
        /// Cast the Fear Spell
        /// </summary>
        public override void Cast()
        {
            Debug.Log("Cast the Fear Spell");
        }
    }
}
