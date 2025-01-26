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
		[SerializeField] private Camera mainCamera;
		[SerializeField] private LiveModifiers liveModifiers;
		[SerializeField] private GameInputReader inputReader;
		[SerializeField] private GameObject skillTreeContainer;
		[SerializeField] private SpriteRenderer backgroundSpriteRenderer;

		private bool isPanning;
		private Vector2 startingMousePosition;
		private Vector2 startingPosition;

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
				// Calculate the position of the center of the skill tree based on the panning offset
				Vector2 panPosition = startingPosition + (Vector2) mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue( )) - startingMousePosition;

				// Calculate the camera width and height
				float cameraHeight = 2f * mainCamera.orthographicSize;
				float cameraWidth = cameraHeight * mainCamera.aspect;

				// Clamp the panning position to make sure the skill tree always stays on the screen
				panPosition.x = Mathf.Clamp(panPosition.x, (-backgroundSpriteRenderer.size.x + cameraWidth) / 2f, (backgroundSpriteRenderer.size.x - cameraWidth) / 2f);
				panPosition.y = Mathf.Clamp(panPosition.y, (-backgroundSpriteRenderer.size.y + cameraHeight) / 2f, (backgroundSpriteRenderer.size.y - cameraHeight) / 2f);

				// Set the position of the skill tree
				transform.position = panPosition;
			}
		}

		/// <summary>
		/// Pan the skill tree
		/// </summary>
		private void PanSkillTree (bool started) {
			isPanning = started;

			if (started) {
				startingPosition = transform.position;
				startingMousePosition = (Vector2) mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue( ));
			}
		}
	}
}
