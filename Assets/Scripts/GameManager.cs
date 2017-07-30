using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager> {
    public string GameOverScene;
    public float LevelDurationSeconds = 60;

    public string LostLevel { get; private set; }

    public readonly ReactiveProperty<LossReason?> LossReason = new ReactiveProperty<LossReason?>();
    private readonly ReactiveProperty<float> _startTime = new FloatReactiveProperty();
    private readonly ReactiveProperty<float> _endTime = new FloatReactiveProperty();

    public ReadOnlyReactiveProperty<float> TimeElapsed => new ReadOnlyReactiveProperty<float>(
        _startTime
            .CombineLatest(_endTime, (start, end) => end - start)
    );

    [UsedImplicitly]
    private void Start() {
        Play();
    }

    [UsedImplicitly]
    private void Update() {
        if (!LossReason.Value.HasValue) {
            _endTime.Value = Time.time;
        }
    }

    public void Play() {
        _startTime.Value = 0;
        _endTime.Value = 0;
        LossReason.Value = null;
    }

    public void Lose(LossReason reason) {
        LossReason.Value = reason;
        LostLevel = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(GameOverScene);
    }
}