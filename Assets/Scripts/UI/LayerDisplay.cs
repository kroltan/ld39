using Builder;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace UI {
    [RequireComponent(typeof(Text))]
    public class LayerDisplay : MonoBehaviour {
        private Text _text;

        [UsedImplicitly]
        private void Start() {
            this.AssignComponent(out _text);
            LevelEditor.Instance.Layer.Subscribe(layer => {
                _text.text = layer.ToString();
            });
        }

        public void Increment() {
            LevelEditor.Instance.Layer.Value++;
        }

        public void Decrement() {
            LevelEditor.Instance.Layer.Value--;
        }
    }
}
