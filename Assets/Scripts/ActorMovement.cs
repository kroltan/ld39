using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class ActorMovement : MonoBehaviour {
    public float Acceleration = 1;
    public float MaxSpeed = 1;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private IActorMovementSource _movement;
    private Vector3 _input;
    private int _slidingCounter;

    [UsedImplicitly]
    private void Start() {
        this.AssignComponent(out _rigidbody);
        this.AssignComponent(out _animator);
        this.AssignComponent(out _movement);
    }

    [UsedImplicitly]
    private void Update() {
        _input = _slidingCounter != 0 && _movement.CanSlide
            ? _rigidbody.velocity.normalized
            : _movement.GetDirection();

        var direction = _input;
        if (direction.sqrMagnitude < 0.25) {
            direction = -_rigidbody.velocity;
        }

        direction.Normalize();
        
        _rigidbody.AddForce(direction * Acceleration);
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, MaxSpeed);
        _animator.SetFloat("MovementSpeed", _rigidbody.velocity.magnitude);
        _animator.SetFloat("Facing", _input.x);
        _animator.SetBool("Sliding", _slidingCounter != 0);
    }

    [UsedImplicitly]
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<SlidableTriggerMarker>() != null) {
            _slidingCounter++;
        }
    }

    [UsedImplicitly]
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<SlidableTriggerMarker>() != null) {
            _slidingCounter--;
        }
    }
}
