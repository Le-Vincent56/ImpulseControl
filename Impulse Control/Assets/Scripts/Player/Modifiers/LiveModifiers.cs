using UnityEngine;

namespace ImpulseControl.Modifiers
{
    public class LiveModifiers : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ModifierPreset preset;

        [Header("Properties")]
        [SerializeField] public AngerSpellModifiers Anger;
        [SerializeField] public FearSpellModifiers Fear;
        [SerializeField] public EnvySpellModifiers Envy;
        [SerializeField] public PlayerModifiers Player;

        private void Awake()
        {
            // Load the chosen preset
            LoadValuesFromPreset(preset);
        }

        /// <summary>
        /// Load the Modifier Preset values
        /// </summary>
        private void LoadValuesFromPreset(ModifierPreset preset)
        {
			Anger = preset.AngerSpellModifiers;
			Fear = preset.FearSpellModifiers;
			Envy = preset.EnvySpellModifiers;
			Player = preset.PlayerModifiers;
        }
    }
}
