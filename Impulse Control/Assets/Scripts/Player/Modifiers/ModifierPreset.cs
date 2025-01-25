using UnityEngine;

namespace ImpulseControl.Modifiers {
	[CreateAssetMenu(fileName = "Modifier Preset")]
	public class ModifierPreset : ScriptableObject {
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
		public void LoadValuesFromPreset (ModifierPreset modifiersPreset) {
			angerSpellModifiers = modifiersPreset.angerSpellModifiers;
			fearSpellModifiers = modifiersPreset.fearSpellModifiers;
			envySpellModifiers = modifiersPreset.envySpellModifiers;
			playerModifiers = modifiersPreset.playerModifiers;
		}
	}
}
