using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI {
    [RequireComponent(typeof(Toggle))]
    public class MixerToggle : MonoBehaviour {
        public AudioMixer Mixer;
        public string ExposedPropertyName;
        public float OffValue = 0;
        public float OnValue = 1;

        [UsedImplicitly]
        private void Start() {
            var toggle = GetComponent<Toggle>();
            SetState(toggle.isOn);
            toggle.onValueChanged.AddListener(SetState);
        }

        public void SetState(bool state) {
            var value = state ? OnValue : OffValue;
            Mixer.SetFloat(ExposedPropertyName, value);
        }
    }
}
