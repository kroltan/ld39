using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AvoidCollisions : MonoBehaviour {
	public float ResponseRate = 1;
	public bool SpeedScaling;

	private Rigidbody _rigidbody;

	[UsedImplicitly]
	private void Start () {
		this.AssignComponent(out _rigidbody);
	}

	[UsedImplicitly]
	private void OnTriggerStay(Collider other) {
		var otherPoint = _rigidbody.ClosestPointOnBounds(other.transform.position);
		var direction = (transform.position - otherPoint).normalized;
		var speed = SpeedScaling ? _rigidbody.velocity.magnitude : 1;
		_rigidbody.AddForce(direction * ResponseRate * speed);
	}
}
