using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    [CreateAssetMenu(fileName = "Envy Spell", menuName = "Spells/Envy Spell")]
    public class EnvySpellStrategy : SpellStrategy
    {
        public override void Link()
        {
            Debug.Log("Linked the Envy Spell");
        }

        public override void Cast()
        {

        }
    }
}
