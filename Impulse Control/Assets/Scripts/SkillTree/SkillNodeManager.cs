using ImpulseControl.Input;
using ImpulseControl.Modifiers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ImpulseControl {
	public class SkillNodeManager : MonoBehaviour {
		[Header("References")]
		[SerializeField] private LiveModifiers liveModifiers;
		[SerializeField] private GameInputReader inputReader;
		[SerializeField] private GameObject skillTreeContainer;

		private bool isPanning;
		private Vector2 startingScreenPosition;

		/// <summary>
		/// A list of all the functions that correspond to the skill nodes. Each skill node has an "id" that corresponds to an index in this array. When the skill node is bought, it will run the function at its id index
		/// </summary>
		public List<Action> SkillNodeFunctionList { get; private set; }

		private void OnEnable ( ) {
			inputReader.PanSkillTree += PanSkillTree;
		}

		private void OnDisable ( ) {
			inputReader.PanSkillTree -= PanSkillTree;
		}

		private void Awake ( ) {
			inputReader = FindObjectOfType<GameInputReader>( );
			liveModifiers = FindObjectOfType<LiveModifiers>( );

			// Initialize the list of functions with everything that each skill node will do
			SkillNodeFunctionList = new List<Action>( ) {
				() => {
					// liveModifiers.Anger.spellDashDistance = 0;
				},
				() => {
					// liveModifiers.Envy.spellBaseDamage += 1;
				}
			};
		}

		private void Update ( ) {
			if (isPanning) {
				transform.position += (Vector3) (Mouse.current.position.ReadValue( ) - startingScreenPosition);
			}
		}

		/// <summary>
		/// Pan the skill tree
		/// </summary>
		private void PanSkillTree (bool started) {
			isPanning = started;

			if (!started) {
				startingScreenPosition = Mouse.current.position.ReadValue( );
			}
		}
	}
}
