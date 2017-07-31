using UnityEngine;

namespace Actors {
    internal interface IActorMovementSource {
        Vector3 GetDirection();
        bool CanSlide { get; }
    }
}