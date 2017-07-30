using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class ChargeWhenStillCondition : MonoBehaviour, IChargingCondition {
	public float SpeedThreshold;

	private Rigidbody _rigidbody;
	private Animator _animator;

	public bool CanCharge => 
		_rigidbody.velocity.magnitude < SpeedThreshold
		|| _animator.GetBool("Sliding");

	[UsedImplicitly]
	private void Start() {
		this.AssignComponent(out _rigidbody);
		this.AssignComponent(out _animator);
	}
}