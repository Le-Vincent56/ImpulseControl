using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl {
	[System.Serializable]
	public struct AngerSpellModifiers {
		[Tooltip("Percentage of the anger emotion that should fill per second (0f - 1f)")]
		public float angerFillPerSecond;
		[Space]
		[Tooltip("The base damage of the spell")]
		public float spellBaseDamage; 
		[Tooltip("Multiplied by the base damage to get the full damage of the attack")]
		public float spellDamagePercentageIncrease; 
		[Tooltip("The range of the spear dash")]
		public float spellDashDistance; 
		[Tooltip("The radius around each attack that also attacks enemies")]
		public float spellRadius; 
		[Tooltip("The amount of anger that the spell costs to cast")]
		public float spellAngerCost; 
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
		[Space]
		[Tooltip("The number of seconds that the exhaustion happens for")]
		public float exhaustionDuration; 
		[Tooltip("The speed of the player when they are exhausted")]
		public float exhaustionMoveSpeed; 
		[Tooltip("Percentage that is multiplied by the incoming damage from an enemy")]
		public float exhaustionWeaknessMultiplier; 
	}

	[System.Serializable]
	public struct FearSpellModifiers {
		[Tooltip("Percentage of the fear emotion that should fill per second (0f - 1f)")]
		public float fearFillPerSecond; 
		[Space]
		[Tooltip("The base damage of the spell")]
		public float spellBaseDamage; 
		[Tooltip("Multiplied by the base damage to get the full damage")]
		public float spellDamagePercentageIncrease; 
		[Tooltip("How many enemies will the attack pierce")]
		public int spellEnemyPierceCount; 
		[Tooltip("How long to stun enemies for when they are hit by the spell")]
		public float spellEnemyStunDuration; 
		[Tooltip("The radius around each attack that also attacks enemies")]
		public float spellRadius; 
		[Tooltip("The amount of fear that the spell costs to cast")]
		public float spellFearCost; 
		[Space]
		[Tooltip("The number of seconds that the crash out happens")]
		public float crashOutDuration; 
		[Tooltip("How much faster to make the player when they are crashing out")]
		public float crashOutMoveSpeedIncrease; 
		[Tooltip("The base damage of running into an enemy when crashing out")]
		public float crashOutBaseDamage; 
		[Tooltip("Multiplied by the base crash out damage to get the full damage")]
		public float crashOutDamagePercentageIncrease; 
		[Tooltip("The radius around the attack that also attacks enemies")]
		public float crashOutDamageRadius; 
		[Space]
		[Tooltip("How long the player is stunned for when exhausted")]
		public float exhaustionDuration; 
		[Tooltip("The percentage of all fill rates for each emotion during exhaustion")]
		public float exhaustionEmotionFillPercentage; 
	}

	[System.Serializable]
	public struct EnvySpellModifiers {
		[Tooltip("Percentage of the envy emotion that should fill per second (0f - 1f)")]
		public float envyFillPerSecond;
		[Space]
		[Tooltip("The base damage of the spell")]
		public float spellBaseDamage; 
		[Tooltip("Multiplied by the base damage to get the full damage of the attack")]
		public float spellDamagePercentageIncrease; 
		[Tooltip("The range around the player that the spell effects")]
		public float spellRadius; 
		[Tooltip("The number of seconds in between enemies taking damage when within the range of the spell")]
		public float spellTickSpeed; 
		[Tooltip("Percentage of the damage done by each spell attack that heals the player")]
		public float spellHealingPercentage; 
		[Tooltip("The amount of envy that this spell costs to cast per second")]
		public float spellEnvyCostPerSecond; 
		[Space]
		[Tooltip("The number of seconds that the crash out happens for")]
		public float crashOutDuration; 
		[Tooltip("How much to increase the base damage of the spell while the player is crashing out")]
		public float crashOutSpellBaseDamageIncrease; 
		[Tooltip("How much to increase the radius of the spell by when crashing out")]
		public float crashOutSpellRadiusIncrease; 
		[Space]
		[Tooltip("The base damaged of all summoned enemies")]
		public float summonedBaseDamage; 
		[Tooltip("Multiplied by the base damage of summoned enemies to get the full damage")]
		public float summonedDamagePercentageIncrease; 
		[Space]
		[Tooltip("The number of seconds that the exhaustion happens for")]
		public float exhaustionDuration; 
		[Tooltip("The percentage of the player's total health to set the player's health to at the end of their exhaustion period")]
		public float exhaustionHealthPercentage; 
	}

	[System.Serializable]
	public struct PlayerModifiers {
		[Tooltip("How fast the player moves")]
		public float moveSpeed;
		[Tooltip("How much max health the player has")]
		public float maxHealth;
		[Tooltip("Buffer between casting spells (in seconds)")]
		public float spellBufferCooldown; 
		[Tooltip("The base defense stat that blocks direct incoming damage")]
		public float baseDefense; 
	}

	[CreateAssetMenu(fileName = "Modifiers")]
	public class Modifiers : ScriptableObject {
		[Header("Properties")]
		[SerializeField] private AngerSpellModifiers angerSpellModifiers;
		[SerializeField] private FearSpellModifiers fearSpellModifiers;
		[SerializeField] private EnvySpellModifiers envySpellModifiers;
		[SerializeField] private PlayerModifiers playerModifiers;

		/// <summary>
		/// All modifiers that pertain to the anger spell
		/// </summary>
		public AngerSpellModifiers AngerSpellModifiers => angerSpellModifiers;

		/// <summary>
		/// All modifiers that pertain to the fear spell
		/// </summary>
		public FearSpellModifiers FearSpellModifiers => fearSpellModifiers;

		/// <summary>
		/// All modifiers that pertain to the envy spell
		/// </summary>
		public EnvySpellModifiers EnvySpellModifiers => envySpellModifiers;

		/// <summary>
		/// All modifiers that pertain to the player
		/// </summary>
		public PlayerModifiers PlayerModifiers => playerModifiers;

		/// <summary>
		/// Set default data to this spell modifier based on a different scriptable object preset
		/// </summary>
		/// <param name="modifiersPreset">The scriptable object preset to load from</param>
		public void LoadValuesFromPreset (Modifiers modifiersPreset) {
			angerSpellModifiers = modifiersPreset.angerSpellModifiers;
			fearSpellModifiers = modifiersPreset.fearSpellModifiers;
			envySpellModifiers = modifiersPreset.envySpellModifiers;
			playerModifiers = modifiersPreset.playerModifiers;
		}
	}
}
