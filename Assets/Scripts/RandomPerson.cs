using System;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RandomPerson : MonoBehaviour, IActorMovementSource {
    [Serializable]
    public class PersonVariant {
        public Sprite[] AnimationParts;
    }

    public SpriteRenderer Renderer;
    public float StepTime = 1;
    private int _variant;

    public PersonVariant[] AllVariants;

    private Vector3 _direction;
    private Animator _animator;
    private bool _hurt;

    [UsedImplicitly]
    private void Start() {
        this.AssignComponent(out _animator);
        _variant = UnityEngine.Random.Range(0, AllVariants.Length - 1);
        Observable.Interval(TimeSpan.FromSeconds(StepTime)).Subscribe(_ => {
            var rand = UnityEngine.Random.onUnitSphere;
            rand.y = 0;
            _direction = rand;
        });
    }

    [UsedImplicitly]
    private void OnCollisionEnter(Collision collision) {
        if (_hurt || collision.gameObject != Utils.GetPlayer()) {
            return;
        }
        _hurt = true;
        _animator.SetTrigger("Hurt");
    }

    public void SetFrame(int frame) {
        Renderer.sprite = AllVariants[_variant].AnimationParts[frame];
    }

    public Vector3 GetDirection() => _hurt ? Vector3.zero : _direction;
    public bool CanSlide => false;
}