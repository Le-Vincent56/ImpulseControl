using ImpulseControl.Modifiers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl {
	public class SkillNode : MonoBehaviour {
		[Header("References")]
		[SerializeField] private List<SkillNode> childSkillNodes = new List<SkillNode>( );
		[SerializeField] private SpriteRenderer spriteRenderer;
		[SerializeField] private Transform nodeConnectorContainer;
		[Header("Properties")]
		[SerializeField] private bool _isVisible;
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
		/// Check to see if this skill node is visible on the skill tree
		/// </summary>
		public bool IsVisible {
			get => _isVisible;
			set {
				_isVisible = value;
				UpdateAlpha( );
			}
		}

		/// <summary>
		/// Check to see if this skill node is currently unlocked
		/// </summary>
		public bool IsUnlocked {
			get => _isUnlocked;
			set {
				_isUnlocked = value;
				UpdateAlpha( );
			}
		}

		/// <summary>
		/// Check to see if this skill node is currently already bought
		/// </summary>
		public bool IsBought {
			get => _isBought;
			set {
				_isBought = value;
				UpdateAlpha( );
			}
		}

		/// <summary>
		/// The number of skill points that this skill node costs
		/// </summary>
		public int SkillPointCost => _skillPointCost;

		/// <summary>
		/// The title of this skill node
		/// </summary>
		public string Title => _title;

		/// <summary>
		/// The description of this skill node
		/// </summary>
		public string Description => _description;

		/// <summary>
		/// A list of all the child skill nodes of this skill node
		/// </summary>
		public List<SkillNode> ChildSkillNodes => childSkillNodes;

		/// NOTE: Ignore the billion warnings that this on validate method throws, unity is just a crybaby
		private void OnValidate ( ) {
			// Round the skill nodes transform to be on a grid
			transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

			for (int i = 0; i < ChildSkillNodes.Count; i++) {
				// If there are no more node connections, then return from the loop
				if (nodeConnectorContainer.childCount == i) {
					Debug.LogWarning("There needs to be more node connections on skill node: " + name);
					break;
				}

				// Get the current index node connection
				Transform nodeConnection = nodeConnectorContainer.GetChild(i);

				// Get the direction from this skill node to the child skill node
				Vector2 direction = ChildSkillNodes[i].transform.position - transform.position;

				// Set the size, position, and rotation of the node connection
				nodeConnection.GetComponent<SpriteRenderer>( ).size = new Vector2(0.25f, direction.magnitude);
				nodeConnection.localPosition = direction / 2f;
				nodeConnection.localRotation = Quaternion.LookRotation(Vector3.back, direction);
			}
		}

		private void Awake ( ) {
			OnValidate( );
		}

		private void Start ( ) {
			// Update the alpha of this node
			UpdateAlpha( );

			// If this skill node is starting as bought, then update all child nodes as well
			if (IsBought) {
				UnlockChildNodes( );
			}
		}

		private void OnMouseEnter ( ) {
			// Show no tooltip if the skill node is not unlocked yet
			if (!IsUnlocked) {
				return;
			}

			FindObjectOfType<SkillNodeInfoBox>( )?.LinkToSkillTreeNode(this);
		}

		private void OnMouseExit ( ) {
			FindObjectOfType<SkillNodeInfoBox>( )?.UnlinkFromSkillNode( );
		}

		private void OnMouseDown ( ) {
			// If the skill node is not unlocked or has already been bought, then return and do nothing
			if (!IsUnlocked || IsBought) {
				return;
			}

			// PlayerExperience playerExperience = FindObjectOfType<PlayerExperience>( );

			// If the player does not have enough skill points to buy this skill node, then return and do nothing
			// if (playerExperience == null || playerExperience.SkillPoints < SkillPointCost) {
			// return;
			// }

			// playerExperience.SkillPoints -= SkillPointCost;
			IsBought = true;
			UnlockChildNodes( );
		}

		/// <summary>
		/// Update and unlock child nodes when this skill node is bought
		/// </summary>
		private void UnlockChildNodes ( ) {
			// Since this parent is being bought, unlock the child nodes as well
			foreach (SkillNode childSkillNode in ChildSkillNodes) {
				childSkillNode.IsUnlocked = true;

				// Set all of the child nodes of that skill node to be visible but not unlocked
				foreach (SkillNode subChildSkillNode in childSkillNode.ChildSkillNodes) {
					subChildSkillNode.IsVisible = true;
				}
			}
		}

		/// <summary>
		/// Update the alpha of both this node and the node connections based on this node's state
		/// </summary>
		private void UpdateAlpha ( ) {
			// Get the node alpha value based on this node's state
			float nodeAlpha = 0f;
			if (IsUnlocked) {
				nodeAlpha = 1f;
			} else if (IsVisible) {
				nodeAlpha = 0.1f;
			}

			spriteRenderer.color = new Color(1f, 1f, 1f, nodeAlpha);

			// Get the node connection alpha value based on this node's state
			float nodeConnectionAlpha = 0f;
			if (IsBought) {
				nodeConnectionAlpha = 1f;
			} else if (IsUnlocked) {
				nodeConnectionAlpha = 0.1f;
			}

			for (int i = 0; i < nodeConnectorContainer.childCount; i++) {
				nodeConnectorContainer.GetChild(i).GetComponent<SpriteRenderer>( ).color = new Color(1f, 1f, 1f, nodeConnectionAlpha);
			}
		}
	}
}
