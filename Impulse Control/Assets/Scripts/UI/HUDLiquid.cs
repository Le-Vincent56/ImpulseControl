using ImpulseControl.Modifiers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl {
	public class HUDLiquid : MonoBehaviour {
		[Header("References - HUDLiquid")]
		[SerializeField] protected LiveModifiers liveModifiers;
		[SerializeField] private RectTransform rectTransform;
		[Header("Properties - HUDLiquid")]
		[SerializeField] private Vector2 basePosition;
		[SerializeField, Min(0f)] private float liquidHeight;
		[SerializeField, Range(0f, 1f)] private float _progress;

		/// <summary>
		/// The progress of the height of the liquid filling up
		/// </summary>
		public float Progress {
			get => _progress;
			set {
				_progress = value;

				rectTransform.anchoredPosition = Vector2.Lerp(basePosition, basePosition + new Vector2(0f, liquidHeight), _progress);
			}
		}

		private void OnValidate ( ) {
			rectTransform = GetComponent<RectTransform>( );
			rectTransform.anchoredPosition = basePosition + new Vector2(0f, liquidHeight);

			liveModifiers = FindObjectOfType<LiveModifiers>( );
		}

		private void Awake ( ) {
			OnValidate( );
		}
	}
}
