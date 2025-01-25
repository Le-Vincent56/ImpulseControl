using ImpulseControl.Modifiers;
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
            // Get an anger spell
            AngerSpell angerSpell = (AngerSpell)spellPool.Pool.Get();

            angerSpell.SetPosition(Vector2.right, 0.5f);
        }
    }
}
