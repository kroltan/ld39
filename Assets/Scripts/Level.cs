using JetBrains.Annotations;
using UnityEngine;

public class Level : MonoBehaviour {
    public float Duration;
    public float TileSize;

    [UsedImplicitly]
    private void Start() {
        GameManager.Instance.LevelDurationSeconds = Duration;
    }
}