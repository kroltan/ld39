using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace UI {
    public class TimeDisplay : MonoBehaviour {
        public RectTransform Bar;

        [UsedImplicitly]
        private void Start() {
            var game = GameManager.Instance;
            game.TimeElapsed.Subscribe(elapsed => {
                var scale = Bar.localScale;
                scale.x = elapsed / game.LevelDurationSeconds;
                Bar.localScale = scale;
            });
        }
    }
}