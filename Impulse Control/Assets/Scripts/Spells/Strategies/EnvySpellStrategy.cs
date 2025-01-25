using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    [CreateAssetMenu(fileName = "Envy Spell", menuName = "Spells/Envy Spell")]
    public class EnvySpellStrategy : SpellStrategy
    {
        /// <summary>
        /// Link the Envy Spell to the Spell System
        /// </summary>
        public override void Link(SpellSystem spellSystem, PlayerMovement playerMovement)
        {
            // Call the parent Link()
            base.Link(spellSystem, playerMovement);

            Debug.Log("Linked the Envy Spell");
        }

        /// <summary>
        /// Cast the Envy Spell
        /// </summary>
        public override void Cast()
        {
            Debug.Log("Cast the Envy Spell");
        }
    }
}
