using JetBrains.Annotations;
using UnityEngine;

namespace Menu {
	public class MouseSmoothDelta : MonoBehaviour {
		public float Speed;

		private Vector3 _center;

		[UsedImplicitly]
		private void Start() {
			_center = transform.position;
		}

		[UsedImplicitly]
		private void Update() {
			var delta = Vector3.Scale(
				Input.mousePosition,
				new Vector3(1f / Screen.width, 1f / Screen.height)
			);
			
			transform.position = Vector3.Lerp(
				transform.position,
				_center + delta,
				Time.deltaTime * Speed
			);
		}
	}
}
