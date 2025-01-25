using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    [CreateAssetMenu(fileName = "Anger Spell", menuName = "Spells/Anger Spell")]
    public class AngerSpellStrategy : SpellStrategy
    {
        public override void Link()
        {
            Debug.Log("Linked the Anger Spell");
        }

        public override void Cast()
        {

        }
    }
}
