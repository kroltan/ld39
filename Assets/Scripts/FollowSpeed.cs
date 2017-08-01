using JetBrains.Annotations;
using UnityEngine;

public class FollowSpeed : MonoBehaviour {
    public Rigidbody Target;
    public float Speed;
    public float ArrivalDistance;
    public float SpeedCutoff = 0.5f;

    [UsedImplicitly]
    private void Update() {
        if (Target == null) {
            Target = Utils.GetPlayer().GetComponent<Rigidbody>();
        }

        var velocity = Target.velocity;
        if (velocity.sqrMagnitude < SpeedCutoff * SpeedCutoff) {
            velocity = Vector3.zero;
        }
        var target = Target.transform.position
                          + velocity;
        var delta = target - transform.position;
        if (delta.magnitude < ArrivalDistance) {
            return;
        }
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * Speed);
    }
}