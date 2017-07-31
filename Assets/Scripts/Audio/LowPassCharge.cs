using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;
using UniRx;

namespace Audio {
    [RequireComponent(typeof(Chargeable))]
    public class LowPassCharge : MonoBehaviour {
        public AudioMixer Mixer;
        public string MixerField;
        public float MinimumPoint = 10;
        public float MaximumPoint = 22000;
        public AnimationCurve ChargeCurve;

        [UsedImplicitly]
        private void Start() {
            GetComponent<Chargeable>().PercentCharge.Subscribe(percent => {
                var point = ChargeCurve.Evaluate(percent);
                var cutoff = MinimumPoint + point * (MaximumPoint - MinimumPoint);
                Mixer.SetFloat(MixerField, cutoff);
            });
        }
    }
}
