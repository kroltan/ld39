using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Animator))]
public class AnimateOnTouch : MonoBehaviour {
    public string Trigger;

    private Animator _animator;

    [UsedImplicitly]
    private void Start() {
        this.AssignComponent(out _animator);
    }

    [UsedImplicitly]
    private void OnTriggerEnter(Collider other) {
        if (other.isTrigger) {
            return;
        }
        _animator.SetTrigger(Trigger);
    }

    public void PlaySound() {
        GetComponent<AudioSource>().Play();
    }
}