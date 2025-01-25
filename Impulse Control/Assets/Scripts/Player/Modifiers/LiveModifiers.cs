using UnityEngine;

namespace ImpulseControl.Modifiers
{
    public class LiveModifiers : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ModifierPreset preset;

        [Header("Properties")]
        [SerializeField] private AngerSpellModifiers angerSpellModifiers;
        [SerializeField] private FearSpellModifiers fearSpellModifiers;
        [SerializeField] private EnvySpellModifiers envySpellModifiers;
        [SerializeField] private PlayerModifiers playerModifiers;

        public AngerSpellModifiers Anger { get => angerSpellModifiers; }
        public FearSpellModifiers Fear { get => fearSpellModifiers; }
        public EnvySpellModifiers Envy { get => envySpellModifiers; }
        public PlayerModifiers Player {  get => playerModifiers; }

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
            angerSpellModifiers = preset.AngerSpellModifiers;
            fearSpellModifiers = preset.FearSpellModifiers;
            envySpellModifiers = preset.EnvySpellModifiers;
            playerModifiers = preset.PlayerModifiers;
        }
    }
}
