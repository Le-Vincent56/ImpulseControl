using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl {
	public class HUDLiquid : MonoBehaviour {
		[SerializeField] private RectTransform rectTransform;
		[SerializeField] private Vector2 basePosition;
		[SerializeField, Min(0f)] private float liquidHeight;
		[SerializeField, Range(0f, 1f)] private float _progress;

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
		}

		private void Awake ( ) {
			OnValidate( );
		}
	}
}
