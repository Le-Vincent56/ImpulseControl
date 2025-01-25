using ImpulseControl.Modifiers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl {
	public class SkillNode : MonoBehaviour {
		[Header("References")]
		[SerializeField] private List<SkillNode> parentSkillNodes = new List<SkillNode>( );
		[SerializeField] private List<SkillNode> childSkillNodes = new List<SkillNode>( );
		[Header("Properties")]
		[SerializeField] private bool _isUnlocked;
		[SerializeField] private bool _isBought;
		[SerializeField] private string _title;
		[SerializeField] private string _description;
		[SerializeField] private int _skillPointCost;
		[SerializeField] private AngerSpellModifiers _angerSpellModifiers;
		[SerializeField] private FearSpellModifiers _fearSpellModifiers;
		[SerializeField] private EnvySpellModifiers _envySpellModifiers;
		[SerializeField] private PlayerModifiers _playerModifiers;

		/// <summary>
		/// Check to see if this skill node is currently unlocked
		/// </summary>
		public bool IsUnlocked {
			get => _isUnlocked;
			set {
				_isUnlocked = value;
			}
		}

		/// <summary>
		/// Check to see if this skill node is currently already bought
		/// </summary>
		public bool IsBought { get => _isBought; private set => _isBought = value; }

		/// <summary>
		/// The number of skill points that this skill node costs
		/// </summary>
		public int SkillPointCost => _skillPointCost;


		/// NOTE: Ignore the billion warnings that this on validate method throws, unity is just a crybaby
		private void OnValidate ( ) {
			// Round the skill nodes transform to be on a grid
			transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

			for (int i = 0; i < childSkillNodes.Count; i++) {
				// If there are no more node connections, then return from the loop
				if (transform.childCount == i) {
					Debug.LogWarning("There needs to be more node connections on skill node: " + name);
					break;
				}

				// Get the current index node connection
				Transform nodeConnection = transform.GetChild(i);

				// Get the direction from this skill node to the child skill node
				Vector2 direction = childSkillNodes[i].transform.position - transform.position;

				// Set the size of the node connection
				nodeConnection.GetComponent<SpriteRenderer>( ).size = new Vector2(0.25f, direction.magnitude);

				// Set the position and rotation of the node connection
				nodeConnection.localPosition = direction / 2f;
				nodeConnection.localRotation = Quaternion.LookRotation(Vector3.back, direction);
			}
		}

		private void Awake ( ) {
			OnValidate( );
		}

		private void OnMouseDown ( ) {
			// If the skill node is not unlocked or has already been bought, then return and do nothing
			if (!IsUnlocked || IsBought) {
				return;
			}

			PlayerExperience playerExperience = FindObjectOfType<PlayerExperience>( );

			// If the player does not have enough skill points to buy this skill node, then return and do nothing
			if (playerExperience == null || playerExperience.SkillPoints < SkillPointCost) {
				return;
			}

			// Since this parent is being bought, unlock the child nodes as well
			foreach (SkillNode childSkillNode in childSkillNodes) {
				childSkillNode.IsUnlocked = true;
			}

			playerExperience.SkillPoints -= SkillPointCost;
		}
	}
}
