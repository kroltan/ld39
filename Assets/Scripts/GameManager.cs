using System;
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
    private readonly ReactiveProperty<float> _elapsed = new FloatReactiveProperty();

    public ReadOnlyReactiveProperty<float> TimeElapsed => _elapsed.ToReadOnlyReactiveProperty();

    [UsedImplicitly]
    private void Start() {
        Play();
        TimeElapsed
            .Where(elapsed => elapsed >= LevelDurationSeconds.Value)
            .ThrottleFirst(TimeSpan.FromSeconds(1))
            .Subscribe(_ => {
                Level.Instance.LevelIndex++;
            });
    }

    [UsedImplicitly]
    private void Update() {
        //Fake no time progress when editing
        if (LevelEditor.Exists && LevelEditor.Instance.Editing) {
            return;
        }
        if (!LossReason.Value.HasValue) {
            _elapsed.Value += Time.deltaTime;
        }
    }

    public void Play() {
        _elapsed.Value = 0;
        LossReason.Value = null;
    }

    public void Lose(LossReason reason) {
        LossReason.Value = reason;
        LostLevel = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(GameOverScene);
    }
}