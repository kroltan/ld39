using UnityEngine;

internal interface IActorMovementSource {
    Vector3 GetDirection();
    bool CanSlide { get; }
}