using Builder;
using JetBrains.Annotations;
using UnityEngine;

public class SimpleChaseMovement : MonoBehaviour, IActorMovementSource {
    public Transform Target;

    public Vector3 GetDirection() {
        if (
            LevelEditor.Instance.Editing
            || Target == null
        ) {
            return Vector3.zero;
        }
        return (Target.position - transform.position).normalized;
    }

    public bool CanSlide => true;

    [UsedImplicitly]
    private void Start() {
        if (Target == null) {
            Target = Utils.GetPlayer().transform;
        }
    }

    [UsedImplicitly]
    private void OnCollisionEnter(Collision collision) {
        if (collision.transform == Target) {
            GameManager.Instance.Lose(LossReason.Caught);
        }
    }
}