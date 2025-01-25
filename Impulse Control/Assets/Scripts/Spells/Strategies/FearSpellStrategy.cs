using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    [CreateAssetMenu(fileName = "Fear Spell", menuName = "Spells/Fear Spell")]
    public class FearSpellStrategy : SpellStrategy
    {
        /// <summary>
        /// Link the Fear Spell to the Spell System
        /// </summary>
        public override void Link(SpellSystem spellSystem, PlayerMovement playerMovement)
        {
            // Call the parent Link()
            base.Link(spellSystem, playerMovement);

            Debug.Log("Linked the Fear Spell");
        }

        /// <summary>
        /// Cast the Fear Spell
        /// </summary>
        public override void Cast()
        {
            Debug.Log("Cast the Fear Spell");
        }
    }
}
