using ImpulseControl.Modifiers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl {
	public class SkillNode : MonoBehaviour {
		[Header("References")]
		[SerializeField] private List<SkillNode> parentSkillNodes;
		[SerializeField] private List<SkillNode> childSkillNodes;
		[SerializeField] private GameObject nodeConnection;
		[SerializeField] private PlayerExperience playerExperience;
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
		public bool IsUnlocked { get => _isUnlocked; set => _isUnlocked = value; }

		/// <summary>
		/// Check to see if this skill node is currently already bought
		/// </summary>
		public bool IsBought { get => _isBought; private set => _isBought = value; }

		/// <summary>
		/// The number of skill points that this skill node costs
		/// </summary>
		public int SkillPointCost => _skillPointCost;

		private void OnMouseDown ( ) {
			// If the skill node is not unlocked or has already been bought, then return and do nothing
			if (!IsUnlocked || IsBought) {
				return;
			}

			// If the player does not have enough skill points to buy this skill node, then return and do nothing
			if (playerExperience.SkillPoints < SkillPointCost) {
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
