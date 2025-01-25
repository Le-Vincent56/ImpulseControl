using ImpulseControl.Timers;
using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    public abstract class SpellStrategy : ScriptableObject
    {

        protected PlayerMovement playerMovement;
        protected SpellSystem spellSystem;
        [SerializeField] protected string spellName;
        [SerializeField] protected string spellDescription;
        [SerializeField] protected int spellLevel;

        // Set a reference to the Spell Modifiers
        protected CountdownTimer cooldownTimer;

        /// <summary>
        /// Link the Spell to a Spell Modifier and Emotion
        /// </summary>
        public virtual void Link(SpellSystem spellSystem, PlayerMovement playerMovement)
        {
            this.spellSystem = spellSystem;
            this.playerMovement = playerMovement;
        }

        /// <summary>
        /// Cast the Spell
        /// </summary>
        public abstract void Cast();
    }
}
