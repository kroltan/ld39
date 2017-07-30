using JetBrains.Annotations;
using UnityEngine;

public class CopyRotation : MonoBehaviour {
    public Transform Target;
    public Quaternion Offset;

    [UsedImplicitly]
    private void Start() {
        if (Target == null) {
            Target = Camera.main.transform;
        }
    }

    [UsedImplicitly]
    private void Update() {
        transform.rotation = Target.rotation;
    }
}