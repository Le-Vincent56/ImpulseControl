using ImpulseControl.Spells.Objects;
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
            // Translate the player
            playerMovement.SetDash(spellSystem.SpellDirection, 5f);

            // Get an Anger Spell
            AngerSpell angerSpell = (AngerSpell)spellPool.Pool.Get();

            // Set the transform of the Anger Spell
            angerSpell.SetTransform(Vector2.right, 1f);
        }
    }
}
