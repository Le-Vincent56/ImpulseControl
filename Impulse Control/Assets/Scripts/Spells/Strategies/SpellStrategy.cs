using ImpulseControl.Modifiers;
using ImpulseControl.Spells.Objects;
using ImpulseControl.Timers;
using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    public abstract class SpellStrategy : ScriptableObject
    {
        protected SpellPool spellPool;
        protected PlayerMovement playerMovement;
        protected EmotionSystem emotionSystem;
        protected SpellSystem spellSystem;
        protected LiveModifiers modifiers;
        [SerializeField] protected string spellName;
        [SerializeField] protected string spellDescription;
        [SerializeField] protected int spellLevel;
        [SerializeField] protected EmotionType correspondingEmotion;

        // Set a reference to the Spell Modifiers
        protected CountdownTimer cooldownTimer;

        /// <summary>
        /// Link the Spell to a Spell Modifier and Emotion
        /// </summary>
        public virtual void Link(SpellSystem spellSystem, PlayerMovement playerMovement, EmotionSystem emotionSystem, LiveModifiers modifiers, SpellPool spellPool)
        {
            this.spellSystem = spellSystem;
            this.playerMovement = playerMovement;
            this.emotionSystem = emotionSystem;
            this.modifiers = modifiers;
            this.spellPool = spellPool;

            // Set up the cooldown timer
            SetupCooldown();
        }

        protected abstract void SetupCooldown();

        protected abstract bool OnCooldown();

        /// <summary>
        /// Cast the Spell
        /// </summary>
        public abstract void Cast();
    }
}
