using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    [CreateAssetMenu(fileName = "Fear Spell", menuName = "Spells/Fear Spell")]
    public class FearSpellStrategy : SpellStrategy
    {
        public override void Link()
        {
            Debug.Log("Linked the Fear Spell");
        }

        public override void Cast()
        {
        }
    }
}
