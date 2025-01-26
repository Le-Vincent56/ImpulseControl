using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ImpulseControl {
	public class SkillNodeInfoBox : MonoBehaviour {
		[Header("References")]
		[SerializeField] private RectTransform infoBoxBackground;
		[SerializeField] private TextMeshPro infoText;

		/// <summary>
		/// Set this info box to be next to a skill node and display its data
		/// </summary>
		/// <param name="skillNode">The skill node to link to</param>
		public void LinkToSkillTreeNode (SkillNode skillNode) {
			// Set the position of the info box
			// This will make the info box to the right of the skill node
			transform.position = (Vector2) skillNode.transform.position + new Vector2(infoBoxBackground.sizeDelta.x / 2f + 0.75f, 0f);

			infoText.text = "<size=+1><b>" + skillNode.Title + "</b></size>\n<color=#c2c2c2>" + skillNode.Description + "</color>";
			infoBoxBackground.gameObject.SetActive(true);
		}

		/// <summary>
		/// Stop the info box from being visible
		/// </summary>
		public void UnlinkFromSkillNode ( ) {
			infoBoxBackground.gameObject.SetActive(false);
		}
	}
}
