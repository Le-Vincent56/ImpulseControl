using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ImpulseControl {
	public class SkillNodeInfoBox : MonoBehaviour {
		[Header("References")]
		[SerializeField] private RectTransform rectTransform;
		[SerializeField] private GameObject infoBoxBackground;
		[SerializeField] private TextMeshProUGUI infoText;

		/// <summary>
		/// Set this info box to be next to a skill node and display its data
		/// </summary>
		/// <param name="skillNode">The skill node to link to</param>
		public void LinkToSkillTreeNode (SkillNode skillNode) {
			// Calculate the screen position to set the info box to
			// We want it to be to the right of the skill node
			//Vector3 infoBoxOffsetPosition = Camera.main.WorldToScreenPoint(skillNode.transform.position);
			//Debug.Log(skillNode.transform.position + " -> " + infoBoxOffsetPosition);
			//infoBoxOffsetPosition.x += infoBoxBackground.GetComponent<RectTransform>( ).sizeDelta.x / 2f;
			//rectTransform.anchoredPosition = infoBoxOffsetPosition;

			infoText.text = skillNode.Title + "\n" + skillNode.Description;
			infoBoxBackground.SetActive(true);
		}

		/// <summary>
		/// Stop the info box from being visible
		/// </summary>
		public void UnlinkFromSkillNode ( ) {
			infoBoxBackground.SetActive(false);
		}
	}
}
