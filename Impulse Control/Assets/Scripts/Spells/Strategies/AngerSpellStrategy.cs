using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    [CreateAssetMenu(fileName = "Anger Spell", menuName = "Spells/Anger Spell")]
    public class AngerSpellStrategy : SpellStrategy
    {
        /// <summary>
        /// Link the Anger Spell to the Spell System
        /// </summary>
        public override void Link(SpellSystem spellSystem, PlayerMovement playerMovement)
        {
            // Call the parent Link()
            base.Link(spellSystem, playerMovement);

            Debug.Log("Linked the Anger Spell");
        }

        /// <summary>
        /// Cast the Anger Spell
        /// </summary>
        public override void Cast()
        {
            Debug.Log("Cast the Anger Spell");
        }
    }
}
