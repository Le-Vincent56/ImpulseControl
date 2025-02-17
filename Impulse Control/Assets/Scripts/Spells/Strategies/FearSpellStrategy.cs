using ImpulseControl.Spells.Objects;
using ImpulseControl.Timers;
using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    [CreateAssetMenu(fileName = "Fear Spell", menuName = "Spells/Fear Spell")]
    public class FearSpellStrategy : SpellStrategy
    {
        /// <summary>
        /// Set up the cooldown for the Fear Spell
        /// </summary>
        protected override void SetupCooldown()
        {
            cooldownTimer = new CountdownTimer(modifiers.Fear.spellCooldownTime);

            // Constantly check the cast time
            cooldownTimer.OnTimerStop += () => cooldownTimer.Reset(modifiers.Fear.spellCooldownTime);
        }

        /// <summary>
        /// Check if the Fear Spell is on cooldown
        /// </summary>
        protected override bool OnCooldown() => cooldownTimer.IsRunning;

        /// <summary>
        /// Check if the Envy Spell can be cast
        /// </summary>
        /// <returns></returns>
        private bool CanCast() => emotionSystem.Fear.CurrentLevel >= modifiers.Fear.spellFearCost;

        /// <summary>
        /// Cast the Fear Spell
        /// </summary>
        public override void Cast()
        {
            // Exit case - if on cooldown
            if (OnCooldown()) return;

            // Exit case - not enough Envy
            if (!CanCast()) return;

            // Get a fear spell
            FearSpell spell = (FearSpell)spellPool.Pool.Get();

            float damage = modifiers.Fear.spellBaseDamage * modifiers.Fear.spellDamagePercentageIncrease;
            spell.SetAttributes(
                damage, 
                modifiers.Fear.spellProjectileSpeed, 
                modifiers.Fear.spellEnemyPierceCount, 
                modifiers.Fear.spellRadius
            );

            // Start the cooldown timer
            cooldownTimer.Start();

            // Remove bubbles
            emotionSystem.Fear.RemoveBubbles(modifiers.Fear.spellFearCost);
        }

        public override void CrashOut()
        {
            // Set the player can't take damage
            playerHealth.CanTakeDamage = false;
        }

        public override void Exhaust()
        {
            // Set the player can take damage
            playerHealth.CanTakeDamage = false;
        }
    }
}
