using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl {
	public class CameraController : MonoBehaviour {
		[SerializeField] private Transform targetTransform;

		private Vector3 velocity;

		private void Update ( ) {
			Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref velocity, 0.5f);
			newPosition.z = transform.position.z;
			transform.position = newPosition;
		}
	}
}
