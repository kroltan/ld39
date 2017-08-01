using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class RandomEvent : MonoBehaviour {
    public float MinimumInterval;
    public float RandomRangeSeconds;
    public UnityEvent Emit;

    private float _nextEvent;

    [UsedImplicitly]
    private void Start() {
        SetNextInterval();
    }

    [UsedImplicitly]
    private void Update() {
        if (!(Time.time > _nextEvent)) {
            return;
        }

        Emit.Invoke();
        SetNextInterval();
    }

    private void SetNextInterval() {
        _nextEvent = 
            Time.time + 
            MinimumInterval + 
            UnityEngine.Random.Range(0, RandomRangeSeconds);
    }
}