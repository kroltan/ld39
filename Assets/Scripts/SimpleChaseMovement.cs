using JetBrains.Annotations;
using UnityEngine;

public class SimpleChaseMovement : MonoBehaviour, IActorMovementSource {
    public Transform Target;

    public Vector3 GetDirection() {
        if (Target == null) {
            return Vector3.zero;
        }
        return (Target.position - transform.position).normalized;
    }

    [UsedImplicitly]
    private void OnCollisionEnter(Collision collision) {
        if (collision.transform == Target) {
            GameManager.Instance.Lose(LossReason.Caught);
        }
    }
}