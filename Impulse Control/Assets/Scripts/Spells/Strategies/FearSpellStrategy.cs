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
        /// Cast the Fear Spell
        /// </summary>
        public override void Cast()
        {
            // Exit case - if on cooldown
            if (OnCooldown()) return;

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
        }
    }
}
