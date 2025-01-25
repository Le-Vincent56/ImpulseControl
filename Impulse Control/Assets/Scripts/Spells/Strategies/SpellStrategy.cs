using ImpulseControl.Timers;
using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    public abstract class SpellStrategy : ScriptableObject
    {
        [SerializeField] protected string spellName;
        [SerializeField] protected string spellDescription;
        [SerializeField] protected int spellLevel;

        // Set a reference to the Spell Modifiers
        protected CountdownTimer cooldownTimer;

        /// <summary>
        /// Link the Spell to a Spell Modifier and Emotion
        /// </summary>
        public abstract void Link();

        /// <summary>
        /// Cast the Spell
        /// </summary>
        public abstract void Cast();
    }
}
