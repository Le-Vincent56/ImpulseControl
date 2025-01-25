using ImpulseControl.Input;
using ImpulseControl.Spells.Strategies;
using UnityEngine;

namespace ImpulseControl
{
    public class SpellSystem : MonoBehaviour
    {
        [SerializeField] private GameInputReader inputReader;
        [SerializeField] private SpellStrategy[] availableSpells;
        [SerializeField] private SpellStrategy currentSpell;
        [SerializeField] private int currentSpellIndex;

        private void OnEnable()
        {
            inputReader.SwapSpell += SwapSpell;
            inputReader.CastSpell += CastSpell;
        }

        private void OnDisable()
        {
            inputReader.SwapSpell -= SwapSpell;
            inputReader.CastSpell += CastSpell;
        }

        private void Start()
        {
            // Iterate through each Spell Strategy
            foreach (SpellStrategy spell in availableSpells)
            {
                // Link the Spell
                spell.Link();
            }

            // Set the current spell
            currentSpellIndex = 0;
            currentSpell = availableSpells[currentSpellIndex];
        }

        /// <summary>
        /// Swap spells
        /// </summary>
        private void SwapSpell(int direction, bool started)
        {
            // Exit case - the button has been lifted
            if (!started) return;

            // Set the Spell index
            currentSpellIndex = (currentSpellIndex + availableSpells.Length + direction) % availableSpells.Length;

            // Set the current Spell
            currentSpell = availableSpells[currentSpellIndex];
        }

        private void CastSpell(bool started)
        {
            // Exit case - the button has been lifted
            if (!started) return;

            // Cast the current Spell
            currentSpell.Cast();
        }
    }
}
