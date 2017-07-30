using System;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GameOver {
    [RequireComponent(typeof(Text))]
    public class GameTimeDisplay : MonoBehaviour {
        private Text _text;

        [UsedImplicitly]
        private void Start() {
            this.AssignComponent(out _text);
            SetText(GameManager.Instance.TimeElapsed.Value);
            GameManager.Instance.TimeElapsed.Subscribe(SetText);
        }

        private void SetText(float seconds) {
            var span = TimeSpan.FromSeconds(seconds);
            _text.text = $"{span.Hours:D1}:{span.Minutes:D2}:{span.Seconds:D2}";
        }
    }
}