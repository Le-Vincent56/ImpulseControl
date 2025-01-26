using ImpulseControl.Events;
using ImpulseControl.Input;
using ImpulseControl.Modifiers;
using ImpulseControl.Spells;
using ImpulseControl.Spells.Objects;
using ImpulseControl.Spells.Strategies;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace ImpulseControl {
	public class SpellSystem : MonoBehaviour {
		[Header("References")]
		[SerializeField] private GameInputReader inputReader;
		[SerializeField] private PlayerMovement playerMovement;
		[SerializeField] private EmotionSystem emotionSystem;
		[SerializeField] private LiveModifiers modifiers;
		[SerializeField] private SpellAimer spellAimer;
        [SerializeField] private HealthPlayer playerHealth;

        [Header("Spells")]
        [SerializeField] private SpellPool[] spellPools;
        [SerializeField] private SpellStrategy[] availableSpells;
        [SerializeField] private List<SpellObject> livingSpells;
        [SerializeField] private SpellStrategy currentSpell;
        [SerializeField] private SpellStrategy angerSpell;
        [SerializeField] private SpellStrategy fearSpell;
        [SerializeField] private SpellStrategy envySpell;
        [SerializeField] private int currentSpellIndex;
        [SerializeField] private bool crashing;

		[Header("Time")]
		[SerializeField] private float time;
		[SerializeField] private float delta;

        private EventBinding<Event_CrashOut> onCrashOut;
        private EventBinding<Event_CrashOutEnd> onCrashOutEnd;

        public Vector2 SpellDirection { get => spellAimer.AimDirection; }
        public SpellStrategy CurrentSpell { get => currentSpell; }

        private void OnEnable()
        {
            onCrashOut = new EventBinding<Event_CrashOut>(CrashOut);
            EventBus<Event_CrashOut>.Register(onCrashOut);

            onCrashOutEnd = new EventBinding<Event_CrashOutEnd>(EndCrashOut);
            EventBus<Event_CrashOutEnd>.Register(onCrashOutEnd);

            inputReader.SwapSpell += SwapSpell;
            inputReader.CastSpell += CastSpell;
        }

        private void OnDisable()
        {
            EventBus<Event_CrashOut>.Deregister(onCrashOut);
            EventBus<Event_CrashOutEnd>.Deregister(onCrashOutEnd);

            inputReader.SwapSpell -= SwapSpell;
            inputReader.CastSpell -= CastSpell;
        }

		private void Start ( ) {
			// Get components
			playerMovement = GetComponent<PlayerMovement>( );
			modifiers = GetComponent<LiveModifiers>( );
			emotionSystem = GetComponent<EmotionSystem>( );
			spellPools = GetComponentsInChildren<SpellPool>( );
			spellAimer = GetComponent<SpellAimer>( );
            playerHealth = GetComponent<HealthPlayer>();

			// Limit the amount of Spell Pools by the number of available Spells
			spellPools = spellPools.Take(availableSpells.Length).ToArray( );

            // Iterate through each available Spell
            for(int i = 0; i < availableSpells.Length; i++)
            {
                spellPools[i].CreateSpellPool(this);
                availableSpells[i].Link(this, playerMovement, emotionSystem, modifiers, spellPools[i], playerHealth);

                // Assign spells
                switch (availableSpells[i].Emotion)
                {
                    case EmotionType.Anger:
                        angerSpell = availableSpells[i];
                        break;
                    case EmotionType.Fear:
                        fearSpell = availableSpells[i];
                        break;
                    case EmotionType.Envy:
                        envySpell = availableSpells[i];
                        break;
                }
            }

			// Set the first Spell
			SetSpell(0);
		}

		private void Update ( ) {
			// Exit case - there are no living Spells
			if (livingSpells.Count == 0)
				return;

			// Set time variables
			time = Time.time;
			delta = Time.deltaTime;

			// Iterate through each living Spell
			foreach (SpellObject spellObject in livingSpells) {
				// Tick the Spell
				spellObject.TickUpdate(time, delta);
			}
		}

		/// <summary>
		/// Set the current Spell given an index
		/// </summary>
		private void SetSpell (int spellIndex) {
			currentSpellIndex = spellIndex;
			currentSpell = availableSpells[currentSpellIndex];
		}

		/// <summary>
		/// Swap the current Spell
		/// </summary>
		private void SwapSpell (int direction, bool started) {
			// Exit case - the button has been lifted
			if (!started)
				return;

			// Calculate the swap index
			int swapIndex = (currentSpellIndex + availableSpells.Length + direction) % availableSpells.Length;

			// Set the Spell at the swap index
			SetSpell(swapIndex);
		}

        /// <summary>
        /// Cast a Spell
        /// </summary>
        private void CastSpell(bool started)
        {
            // Exit case - the button has been lifted or if in the middle of crashing
            if (!started || crashing) return;

			// Cast the current Spell
			currentSpell.Cast( );
		}

		/// <summary>
		/// Register a Spell to be tracked
		/// </summary>
		public void RegisterSpell (SpellObject spell) {
			// Exit case - if the Spell is already being tracked
			if (livingSpells.Contains(spell))
				return;

			// Add the spell
			livingSpells.Add(spell);
		}

		/// <summary>
		/// Deregister a Spell from being tracked
		/// </summary>
		public void DeregisterSpell (SpellObject spell) {
			// Exit case - if the Spell is not being tracked
			if (!livingSpells.Contains(spell))
				return;

            // Remove the spell
            livingSpells.Remove(spell);
        }

        private void CrashOut(Event_CrashOut eventData)
        {
            // Set to crashing
            crashing = true;

            // Crash out the specific Emotion
            switch (eventData.emotionType)
            {
                case EmotionType.Anger:
                    angerSpell.CrashOut();
                    break;
                case EmotionType.Fear:
                    fearSpell.CrashOut();
                    break;
                case EmotionType.Envy:
                    envySpell.CrashOut();
                    break;
            }
        }

        private void EndCrashOut(Event_CrashOutEnd eventData)
        {
            // Set to not crashing
            crashing = false;

            // Crash out the specific Emotion
            switch (eventData.emotionType)
            {
                case EmotionType.Anger:
                    angerSpell.Exhaust();
                    break;
                case EmotionType.Fear:
                    fearSpell.Exhaust();
                    break;
                case EmotionType.Envy:
                    envySpell.Exhaust();
                    break;
            }
        }
    }
}
