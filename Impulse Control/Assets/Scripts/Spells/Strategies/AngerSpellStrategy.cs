using ImpulseControl.Spells.Objects;
using ImpulseControl.Timers;
using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    [CreateAssetMenu(fileName = "Anger Spell", menuName = "Spells/Anger Spell")]
    public class AngerSpellStrategy : SpellStrategy
    {
        /// <summary>
        /// Set up the cooldown for the Anger Spell cooldown
        /// </summary>
        protected override void SetupCooldown()
        {
            cooldownTimer = new CountdownTimer(modifiers.Anger.spellCooldownTime);

            // Constantly check the cast time
            cooldownTimer.OnTimerStop += () => cooldownTimer.Reset(modifiers.Anger.spellCooldownTime);
        }

        /// <summary>
        /// Check if the Anger Spell is on cooldown
        /// </summary>
        protected override bool OnCooldown() => cooldownTimer.IsRunning;

        /// <summary>
        /// Cast the Anger Spell
        /// </summary>
        public override void Cast()
        {
            // Exit case - the Anger Spell is on cooldown
            if (OnCooldown()) return;

            // Translate the player
            playerMovement.SetDash(spellSystem.SpellDirection, modifiers.Anger.spellDashDistance);

            // Get an Anger Spell
            AngerSpell angerSpell = (AngerSpell)spellPool.Pool.Get();

            // Set the transform of the Anger Spell
            angerSpell.SetTransform(Vector2.right, modifiers.Anger.spellStartingOffset);

            // Set the attributes of the Anger Spell
            float damage = modifiers.Anger.spellBaseDamage * modifiers.Anger.spellBaseDamage;
            angerSpell.SetAttributes(emotionSystem.Anger, damage);

            // Start the cooldown timer
            cooldownTimer.Start();
        }
    }
}
