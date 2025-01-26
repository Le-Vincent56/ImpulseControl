using UnityEngine;

namespace ImpulseControl.Modifiers
{
    [System.Serializable]
    public struct AngerSpellModifiers
    {
        [Tooltip("Percentage of the anger emotion that should fill per second (0f - 1f)")]
        public float angerFillPerSecond;
        [Space]
        [Tooltip("The base damage of the spell")]
        public float spellBaseDamage;
        [Tooltip("Multiplied by the base damage to get the full damage of the attack")]
        public float spellDamagePercentageIncrease;
        [Tooltip("The range of the spear dash")]
        public float spellDashDistance;
        [Tooltip("The offset of the spear at the beginning of the attack")]
        public float spellStartingOffset;
        [Tooltip("The radius around each attack that also attacks enemies")]
        public float spellRadius;
        [Tooltip("The amount of anger that the spell costs to cast")]
        public float spellAngerCost;
        [Tooltip("The cooldown time of the spell")]
        public float spellCooldownTime;
        [Space]
        [Tooltip("The base damage that fire does each tick")]
        public float fireBaseDamage;
        [Tooltip("Multiplied by the base fire damage to get the full damage")]
        public float fireDamagePercentageIncrease;
        [Tooltip("The number of seconds that an enemy is effected by the fire effect")]
        public float fireEffectLength;
        [Tooltip("The number of seconds in between doing fire ticks")]
        public float fireTickSpeed;
        [Space]
        [Tooltip("The base damage of the explosion")]
        public float explosionBaseDamage;
        [Tooltip("Multiplied by the base explosion damage to get the full damage")]
        public float explosionDamagePercentageIncrease;
        [Tooltip("The radius of explosions when they are created")]
        public float explosionRadius;
        [Space]
        [Tooltip("The number of seconds that the crash out happens for")]
        public float crashOutDuration;
        [Tooltip("The number of seconds in between the character automatically attacking")]
        public float crashOutAttackSpeed;
        [Tooltip("The maximum range at which you can throw a spear")]
        public float crashOutRange;
        [Tooltip("Percentage that is multiplied by the incoming damage from an enemy. Should reduce damage")]
        public float crashOutDamageReduction;
        [Tooltip("The offset of the crash out spell")]
        public float crashOutOffset;
        [Tooltip("The life time of the crash out spell")]
        public float crashOutLifetime;
        [Tooltip("The projectile speed of the crash out spell")]
        public float crashOutProjectileSpeed;
        [Space]
        [Tooltip("The number of seconds that the exhaustion happens for")]
        public float exhaustionDuration;
        [Tooltip("The speed of the player when they are exhausted")]
        public float exhaustionMoveSpeed;
        [Tooltip("Percentage that is multiplied by the incoming damage from an enemy")]
        public float exhaustionWeaknessMultiplier;
    }
}
