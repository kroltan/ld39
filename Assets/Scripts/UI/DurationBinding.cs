using System.Globalization;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace UI {
    [RequireComponent(typeof(InputField))]
    public class DurationBinding : MonoBehaviour {
        private InputField _input;

        [UsedImplicitly]
        private void Start() {
            this.AssignComponent(out _input);
            _input.onValueChanged.AddListener(value => {
                int intValue;
                if (!int.TryParse(value, out intValue)) {
                    _input.text = "0";
                    return;
                }
                GameManager.Instance.LevelDurationSeconds.Value = intValue;
            });
            GameManager.Instance.LevelDurationSeconds.Subscribe(duration => {
                _input.text = duration.ToString(CultureInfo.CurrentCulture);
            });
        }
    }
}
