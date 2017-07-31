using Builder;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager> {
    public string GameOverScene;
    public ReactiveProperty<float> LevelDurationSeconds = new ReactiveProperty<float>(60);
    public GameObject[] AllPrefabs;

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
        TimeElapsed
            .Where(duration => duration >= LevelDurationSeconds.Value)
            .Subscribe(_ => {
                Level.Instance.LevelIndex++;
            });
    }

    [UsedImplicitly]
    private void Update() {
        //Fake no time progress when editing
        if (LevelEditor.Instance.Editing) {
            return;
        }
        if (!LossReason.Value.HasValue) {
            _endTime.Value += Time.deltaTime;
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