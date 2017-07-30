using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ZoomSpeed : MonoBehaviour {
    public Rigidbody Target;
    public float MinimumSize = 1;
    public float SpeedFactor = 1;
    public float Speed = 1;

    private Camera _camera;

    [UsedImplicitly]
    private void Start() {
        this.AssignComponent(out _camera);
    }

    [UsedImplicitly]
    private void Update() {
        var speed = Target.velocity.magnitude * SpeedFactor;
        var next = Mathf.Lerp(_camera.orthographicSize, MinimumSize + speed, Speed);
        _camera.orthographicSize = next;
    }
}